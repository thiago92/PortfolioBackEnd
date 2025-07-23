namespace Infrastructure.Interfaces.IRepositories
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        void Rollback();
        void BeginTransaction();
        void Commit();
    }
}
