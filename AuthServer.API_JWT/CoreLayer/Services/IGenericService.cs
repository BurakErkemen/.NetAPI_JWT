using SharedLibrary.DTO;
using System.Linq.Expressions;

namespace CoreLayer.UnitOfWork
{
    public interface IGenericService <TEntity,TDto> where TEntity : class where TDto : class
    {
        Task<Response<TDto>> GetByIdAsync(int Id);

        Task<Response<IEnumerable<TDto>>> GetAllAsync();

        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);

        Task<Response<TDto>> AddAsync(TDto entity);

        // State durumu modified olarak geçen entityleri veri tabanında aradığı için asenkron olarak çalışmamaktadır.
        Task<Response<NoDataDTO>> Update(int Id);

        Task<Response<NoDataDTO>> Delete(int Id);
    }
}
