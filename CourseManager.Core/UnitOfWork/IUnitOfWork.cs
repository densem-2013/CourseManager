using System;
using System.Data;
using CourseManager.Core.Repositories;

namespace CourseManager.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        CourseManagerContext GetContext();
        int SaveChanges();
        void Dispose(bool disposing);
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        bool Commit();
        void Rollback();
    }
}
