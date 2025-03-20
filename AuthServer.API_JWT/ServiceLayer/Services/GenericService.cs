using CoreLayer.Repositories;
using CoreLayer.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTO;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace ServiceLayer.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _repository;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }


        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();

            return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(entities), 200);
        }

        public async Task<Response<TDto>> GetByIdAsync(int Id)
        {
            var entity = await _repository.GetByIdAsync(Id);
            if (entity is null)
            {
                return Response<TDto>.Fail("Entity not found", 404, true);
            }
            return Response<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(entity), 200);
        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _repository.Where(predicate);

            return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()), 200);
        }

        public async Task<Response<TDto>> AddAsync(TDto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);

            await _repository.AddAsync(newEntity);
            await _unitOfWork.SaveChangesAsync();

            return Response<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(newEntity), 200);
        }

        public async Task<Response<NoDataDTO>> Update(int Id)
        {
            var anyEntity = await _repository.GetByIdAsync(Id);

            if (anyEntity is null)
            {
                return Response<NoDataDTO>.Fail("Entity not found", 404, true);
            }

            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(anyEntity);
            _repository.Update(updateEntity);
            await _unitOfWork.SaveChangesAsync();

            return Response<NoDataDTO>.Success(204);
        }

        public async Task<Response<NoDataDTO>> Delete(int Id)
        {
            var entity = await _repository.GetByIdAsync(Id);

            if (entity == null)
            {
                return Response<NoDataDTO>.Fail("Entity not found", 404, true);
            }

            _repository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();

            return Response<NoDataDTO>.Success(204);
        }
    }
}