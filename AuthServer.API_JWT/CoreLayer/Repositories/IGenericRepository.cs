using System.Linq.Expressions;

namespace CoreLayer.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int Id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        IQueryable<TEntity> Where(Expression<Func<TEntity,bool>> predicate);

        Task AddAsync(TEntity entity);

        // State durumu modified olarak geçen entityleri veri tabanında aradığı için asenkron olarak çalışmamaktadır.
        TEntity Update(TEntity entity);  

        void Delete(TEntity entity);
    }
}