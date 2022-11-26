using Blog.Domain.CrossCutting;

namespace Blog.Infrastructure.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<T> GetById<T>(Guid id) where T : IEntity;
        Task<T> InsertAsync<T>(T item) where T : IEntity;
        Task InsertRangeAsync<T>(IList<T> items) where T : IEntity;
        Task DeleteAsync<T>(Guid id) where T : IEntity;
        Task UpdateAsync<T>(T item) where T : IEntity;
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
