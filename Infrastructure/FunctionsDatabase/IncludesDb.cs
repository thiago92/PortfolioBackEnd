using Microsoft.EntityFrameworkCore;

namespace Infrastructure.FunctionsDatabase
{
    public static class IncludesDb
    {
        public static IQueryable<T> Includes<T>(this IQueryable<T> query, params string[] includes) where T : class
        {
            var aux = query;

            if (includes == null) return aux;
            
            foreach (var include in includes)
            {
                aux = aux.Include(include);
            }

            return aux;
        }
    }
}
