

using BackEnd.Core.Enums;

namespace BackEnd.Core.Models;

public class ItemsTrabajoEntity:ModelBase
{
   
    public string Titulo { get; set; }

    public string? Descripcion { get; set; }

    public DateTime FechaEntrega { get; set; }

    public Relevancia Relevancia { get; set; }

    public EstadoItem Estado { get; set; }

    public int UsuarioId { get; set; }  

    public UsuarioEntity? Usuario { get; set; } 
}
