

using BackEnd.Core.Models;

namespace BackEnd.Bussines.ItemsTrabajo;

public interface IItemTrabajoService
{

    Task<ItemsTrabajoEntity> CrearAsync(ItemsTrabajoEntity item);

    Task<List<ItemsTrabajoEntity>> ObtenerTodosAsync();

    Task<ItemsTrabajoEntity?> ObtenerPorIdAsync(int id);

    Task<List<ItemsTrabajoEntity>> ObtenerPendientesUsuarioAsync(int usuarioId);
}
