using BackEnd.Core.Models;
using BackEnd.Core.Repository;
using BackEnd.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace BackEnd.Infrastructure.Repository
{
    public class BaseRepository<T> : IModelBaseRepository<T> where T : ModelBase
    {
        private readonly BackEndContext _db;
        private readonly ILogger<BaseRepository<T>> _logger;

        public BaseRepository(BackEndContext db, ILogger<BaseRepository<T>>? logger = null)
        {
            _db = db;
            _logger = logger ?? NullLogger<BaseRepository<T>>.Instance;
        }

        public Task<List<T>> GetAllAsync()
        {
            return _db.Set<T>().ToListAsync();
        }

                public async Task CreateAsync(T entity)
                {
                    try
                    {
                        _logger.LogInformation("Iniciando creación de entidad {EntityType}", typeof(T).Name);

                        await _db.Set<T>().AddAsync(entity);
                        await _db.SaveChangesAsync();

                        _logger.LogInformation("Entidad {EntityType} creada con Id {Id}", typeof(T).Name, entity.Id);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error al crear entidad {EntityType}", typeof(T).Name);
                        throw;
                    }
                }

        public async Task UpdateAsync(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        public Task<T?> GetAsync(long id)
        {
            return _db.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<T> List()
        {
            return _db.Set<T>().AsNoTracking();
        }
    }
}
