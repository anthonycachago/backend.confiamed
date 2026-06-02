using BackEnd.Core.Enums;
using BackEnd.Core.Models;
using BackEnd.Core.Repository;

namespace BackEnd.Bussines.ItemsTrabajo;

public class ItemTrabajoService : IItemTrabajoService
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

    private bool EstaSaturado(UsuarioEntity usuario)
    {
        return usuario.ItemsTrabajo.Count(t =>
            t.Estado == EstadoItem.Pendiente &&
            t.Relevancia == Relevancia.Alta) >= 3;
    }

    private async Task<UsuarioEntity> AsignarUsuarioAsync(
        ItemsTrabajoEntity item)
    {
        var usuarios = await _usuarioRepository.GetAllAsync();

        if (usuarios == null || !usuarios.Any())
            throw new Exception("No hay usuarios registrados");

        var disponibles = usuarios
            .Where(u => !EstaSaturado(u))
            .ToList();

        if (!disponibles.Any())
            throw new Exception(
                "No hay usuarios disponibles para asignación");

        bool urgente =
            item.FechaEntrega <= DateTime.Now.AddDays(3);

        UsuarioEntity usuarioAsignado;

        if (urgente)
        {
            usuarioAsignado = disponibles
                .OrderBy(u => u.ItemsTrabajo.Count(t =>
                    t.Estado == EstadoItem.Pendiente &&
                    t.Relevancia == Relevancia.Alta))
                .ThenBy(u => u.ItemsTrabajo.Count(t =>
                    t.Estado == EstadoItem.Pendiente))
                .First();
        }
        else
        {
            usuarioAsignado = disponibles
                .OrderBy(u => u.ItemsTrabajo.Count(t =>
                    t.Estado == EstadoItem.Pendiente))
                .ThenBy(u => u.ItemsTrabajo.Count(t =>
                    t.Estado == EstadoItem.Pendiente &&
                    t.Relevancia == Relevancia.Alta))
                .First();
        }

        return usuarioAsignado;
    }

    public async Task<ItemsTrabajoEntity> CrearAsync(
        ItemsTrabajoEntity item)
    {
        var usuario = await AsignarUsuarioAsync(item);

        item.UsuarioId = (int)usuario.Id;
        item.Usuario = null;
        item.Estado = EstadoItem.Pendiente;

        await _itemRepository.CreateAsync(item);

        return item;
    }

    public async Task<List<ItemsTrabajoEntity>> ObtenerTodosAsync()
    {
        return await _itemRepository.GetAllAsync();
    }

    public async Task<ItemsTrabajoEntity?> ObtenerPorIdAsync(int id)
    {
        return await _itemRepository.GetByIdAsync(id);
    }

    public async Task<List<ItemsTrabajoEntity>> ObtenerPendientesUsuarioAsync(
        int usuarioId)
    {
        return await _itemRepository
            .GetPendientesUsuarioAsync(usuarioId);
    }
}