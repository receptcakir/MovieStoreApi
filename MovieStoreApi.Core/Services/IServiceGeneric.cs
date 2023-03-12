using MovieStoreApi.Core.Models;
using System.Linq.Expressions;

namespace MovieStoreApi.Core.Services
{
    public interface IServiceGeneric<TEntity,TDto> where TEntity : BaseEntity where TDto : class
    {
        Task<TDto> GetByIdAsync(int id);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<IEnumerable<TDto>> Where(Expression<Func<TEntity, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
        Task<TDto> AddAsync(TDto dto);
        Task<IEnumerable<TDto>> AddRangeAsync(IEnumerable<TDto> dtos);
        Task UpdateAsync(TDto dto);
        Task RemoveAsync(int id);
        Task RemoveRangeAsync(IEnumerable<int> ids);
    }
}
