

namespace BackEnd.Core.Models;

public class UsuarioEntity:ModelBase
{
    public string Username { get; set; }
    public List<ItemsTrabajoEntity> ItemsTrabajo { get; set; } = new();
}
