using BackEnd.Core.Models;
using BackEnd.Core.Repository;
using BackEnd.Infrastructure.DataBase;

namespace BackEnd.Infrastructure.Repository;

public class UsuarioRepository:BaseRepository<UsuarioEntity>, IUsuarioRepository
{
    public UsuarioRepository(BackEndContext backEnd) : base(backEnd)
    {
        
    }
}
