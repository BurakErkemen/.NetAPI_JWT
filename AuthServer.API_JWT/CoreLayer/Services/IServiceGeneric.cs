using SharedLibrary.DTO;
using System.Linq.Expressions;

namespace CoreLayer.UnitOfWork
{
    public interface IServiceGeneric <TEntity,TDto> where TEntity : class where TDto : class
    {
        Task<Response<TDto>> GetByIdAsync(int Id);

        Task<Response<IEnumerable<TDto>>> GetAllAsync();

        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TDto, bool>> predicate);

        Task<Response<TDto>> AddAsync(TEntity entity);

        // State durumu modified olarak geçen entityleri veri tabanında aradığı için asenkron olarak çalışmamaktadır.
        Task<Response<NoDataDTO>> Update(TEntity entity);

        Task<Response<NoDataDTO>> Delete(TEntity entity);
    }
}
