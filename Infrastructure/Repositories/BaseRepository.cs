using Infrastructure.Context;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Notifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly IUnitOfWork _unitOfWork;
        private readonly NotificationContext _notificationContext;

        public BaseRepository(AppDbContext context, IUnitOfWork unitOfWork, NotificationContext notificationContext)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _unitOfWork = unitOfWork;
            _notificationContext = notificationContext;
        }

        public TEntity Add(TEntity obj)
        {
            _unitOfWork.BeginTransaction();
            _dbSet.Add(obj);
            _unitOfWork.Commit();
            return obj;
        }

        public void Update(TEntity obj)
        {
            _unitOfWork.BeginTransaction();
            _dbSet.Update(obj);
            _unitOfWork.Commit();
        }

        public void Delete(TEntity obj)
        {
            _unitOfWork.BeginTransaction();
            _dbSet.Remove(obj);
            _unitOfWork.Commit();
        }

        public IQueryable<TEntity> GetByExpression(Expression<Func<TEntity, bool>> expression, params string[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return query.Where(expression);
        }

        public TEntity? GetElementByExpression(Expression<Func<TEntity, bool>> expression, params string[] includes)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return query.FirstOrDefault(expression);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
