using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mikro.Task.Services.Application.Helpers;
using MovieStoreApi.Core.Models;
using MovieStoreApi.Core.Repositories;
using MovieStoreApi.Core.Services;
using MovieStoreApi.Core.UnitOfWorks;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace MovieStoreApi.Service.Services
{
    public class ServiceGeneric<TEntity,TDto> : IServiceGeneric<TEntity,TDto> where TEntity : BaseEntity where TDto : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public ServiceGeneric(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork,IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TDto> AddAsync(TDto dto)
        {
            var newEntity = _mapper.Map<TEntity>(dto);

            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = _mapper.Map<TDto>(newEntity);
            return newDto;
        }

        public async Task<IEnumerable<TDto>> AddRangeAsync(IEnumerable<TDto> dtos)
        {
            var newEntities = _mapper.Map<IEnumerable<TEntity>>(dtos);

            await _repository.AddRangeAsync(newEntities);
            await _unitOfWork.CommitAsync();

            var newDtos = _mapper.Map<IEnumerable<TDto>>(newEntities);
            return newDtos;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAll().ToListAsync();

            var newDtos = _mapper.Map<IEnumerable<TDto>>(entities);
            return newDtos;
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            var hasProduct = await _repository.GetByIdAsync(id);

            if (hasProduct == null)
            {
                throw new NotFoundException($"{typeof(TDto).Name}({id}) not found");
            }

            var newDto = _mapper.Map<TDto>(hasProduct);
            return newDto;
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<int> ids)
        {
            var entities = await _repository.Where( x => ids.Contains(x.Id)).ToListAsync();
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);

            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<TDto>> Where(Expression<Func<TEntity, bool>> expression)
        {
            var entities = await _repository.Where(expression).ToListAsync() ;

            var newDtos = _mapper.Map<IEnumerable<TDto>>(entities);
            return newDtos;
        }
    }
}
