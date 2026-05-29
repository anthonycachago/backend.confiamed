

using BackEnd.Core.Models;

namespace BackEnd.Core.Repository;

public interface IModelBaseRepository<T> where T : ModelBase
{
    Task CreateAsync(T model);
    Task<List<T>> GetAllAsync();
    Task UpdateAsync(T model);
    Task DeleteAsync(T model);
    Task<T?> GetAsync(long Id);
    IQueryable<T> List();
}

