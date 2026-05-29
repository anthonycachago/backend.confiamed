

using BackEnd.Core.Models;
using BackEnd.Core.Repository;
using BackEnd.Infrastructure.DataBase;

namespace BackEnd.Infrastructure.Repository;

public class ItemTrabajoRepository: BaseRepository<ItemsTrabajoEntity>, IItemTrabajoRepository
{
    public ItemTrabajoRepository(BackEndContext backEnd) : base(backEnd)
    {
        
    }
}
