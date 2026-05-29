

using BackEnd.Core.Models;

namespace BackEnd.Bussines.ItemsTrabajo;

public interface IItemTrabajoService
{
   
    Task<ItemsTrabajoEntity> CrearAsync(ItemsTrabajoEntity item);
}
