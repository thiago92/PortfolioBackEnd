﻿using Infrastructure.FiltersModel;
using System.Linq.Expressions;

namespace Infrastructure.FunctionsDatabase
{
    public static class QueryableExtensions
    {
        public static FilterReturn<T> ApplyDynamicFilters<T>(this IQueryable<T> query, Dictionary<string, string> filters, int pageSize, int pageNumber) where T : class
        {
            IQueryable<T> paginatedQuery;
            int totalItems;

            if (filters is null || !filters.Any())
            {
                totalItems = query.Count();
                paginatedQuery = ApplyPagination(query, pageSize, pageNumber);
            }
            else
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                Expression combined = null;

                foreach (var filter in filters)
                {
                    if (string.IsNullOrEmpty(filter.Value))
                        continue;

                    var propertyName = filter.Key.EndsWith("Contains") ? filter.Key.Replace("Contains", "") :
                        filter.Key.EndsWith("Equal") ? filter.Key.Replace("Equal", "") : filter.Key;
                    var member = Expression.Property(parameter, propertyName);
                    var constant = Expression.Constant(filter.Value);

                    Expression body = member.Type switch
                    {
                        Type t when t == typeof(string) => BuildStringContainsExpression(member, constant),
                        Type t when t == typeof(int) || t == typeof(double) || t == typeof(decimal) => BuildNumericEqualsExpression(member, constant),
                        Type t when t == typeof(Guid) => BuildGuidEqualsExpression(member, constant),
                        Type t when t == typeof(DateTime) => BuildDateTimeEqualsExpression(member, constant),
                        Type t when t.IsEnum => BuildEnumEqualsExpression(member, constant),
                        _ => throw new NotSupportedException($"The type '{member.Type}' is not supported for dynamic filtering.")
                    };

                    combined = combined == null ? body : Expression.AndAlso(combined, body);
                }

                if (combined is not null)
                {
                    var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);
                    query = query.Where(lambda);
                }

                totalItems = query.Count();
                paginatedQuery = ApplyPagination(query, pageSize, pageNumber);
            }

            return new FilterReturn<T>
            {
                TotalRegister = totalItems,
                TotalRegisterFilter = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                ItensList = paginatedQuery.ToList()
            };
        }
        private static Expression BuildDateTimeEqualsExpression(MemberExpression member, ConstantExpression constant)
        {
            DateTime dateTimeValue = DateTime.Parse((string)constant.Value);
            var utcDateTimeValue = dateTimeValue.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(dateTimeValue, DateTimeKind.Utc)
                : dateTimeValue.ToUniversalTime();
            var dateOnlyConstant = Expression.Constant(utcDateTimeValue.Date, typeof(DateTime));

            var memberDate = Expression.Property(member, "Date");
            return Expression.Equal(memberDate, dateOnlyConstant);
        }

        private static Expression BuildEnumEqualsExpression(MemberExpression member, ConstantExpression constant)
        {
            object enumValue = Enum.Parse(member.Type, (string)constant.Value);
            var enumConstant = Expression.Constant(enumValue);
            return Expression.Equal(member, enumConstant);
        }

        private static Expression BuildGuidEqualsExpression(MemberExpression member, ConstantExpression constant)
        {
            Guid guidValue = Guid.Parse((string)constant.Value);
            var guidConstant = Expression.Constant(guidValue);
            return Expression.Equal(member, guidConstant);
        }

        private static Expression BuildStringContainsExpression(MemberExpression member, ConstantExpression constant)
        {
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            return Expression.Call(member, containsMethod, constant);
        }

        private static Expression BuildNumericEqualsExpression(MemberExpression member, ConstantExpression constant)
        {
            var numericValue = Convert.ChangeType(constant.Value, member.Type);
            var numericConstant = Expression.Constant(numericValue);
            return Expression.Equal(member, numericConstant);
        }

        private static IQueryable<T> ApplyPagination<T>(IQueryable<T> query, int pageSize, int pageNumber)
        {
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }  
    }
}
