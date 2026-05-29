

using BackEnd.Core.Dto;
using BackEnd.Core.Enums;
using BackEnd.Core.Models;
using BackEnd.Core.Repository;


namespace BackEnd.Bussines.ItemsTrabajo;


public class ItemTrabajoService: IItemTrabajoService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IItemTrabajoRepository _itemRepository;

    public ItemTrabajoService(
         IUsuarioRepository usuarioRepository,
        IItemTrabajoRepository itemTrabajoRepository)
    {
        _usuarioRepository = usuarioRepository;
        _itemRepository = itemTrabajoRepository;
    }
    private async Task<UsuarioEntity> AsignarUsuarioAsync(ItemsTrabajoEntity item)
    {
        var usuarios = await _usuarioRepository.GetAllAsync();

        if (usuarios == null || !usuarios.Any())
            throw new Exception("No hay usuarios registrados");

        var disponibles = usuarios
            .Where(u =>
            {
                var pendientes = u.ItemsTrabajo?
                    .Count(t => t.Estado == EstadoItem.Pendiente) ?? 0;

                var altasPendientes = u.ItemsTrabajo?
                    .Count(t =>
                        t.Relevancia == Relevancia.Alta &&
                        t.Estado == EstadoItem.Pendiente) ?? 0;

                return altasPendientes < 3; // regla de saturación
            })
            .ToList();

        if (!disponibles.Any())
            throw new Exception("No hay usuarios disponibles para asignación");

        bool urgente = item.FechaEntrega <= DateTime.Now.AddDays(3);

        var usuarioAsignado = disponibles
            .OrderBy(u =>
                u.ItemsTrabajo?.Count(x =>
                    x.Estado == EstadoItem.Pendiente) ?? 0)
            .First();

        return usuarioAsignado;
    }
    public async Task<ItemsTrabajoEntity> CrearAsync(ItemsTrabajoEntity item)
    {
        var usuario = await AsignarUsuarioAsync(item);

       
        item.UsuarioId = (int)usuario.Id;
        item.Usuario = null;

        item.Estado = EstadoItem.Pendiente;

        await _itemRepository.CreateAsync(item);

        return item;
    }
}
