

using BackEnd.Core.Models;

namespace BackEnd.Core.Repository;

public interface IItemTrabajoRepository : IModelBaseRepository<ItemsTrabajoEntity>
{
    Task<List<ItemsTrabajoEntity>> GetAllAsync();
    Task<ItemsTrabajoEntity?> GetByIdAsync(int id);
    Task<List<ItemsTrabajoEntity>> GetPendientesUsuarioAsync(int usuarioId);
}
