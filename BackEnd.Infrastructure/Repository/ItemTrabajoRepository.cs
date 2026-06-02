

using BackEnd.Core.Enums;
using BackEnd.Core.Models;
using BackEnd.Core.Repository;
using BackEnd.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Infrastructure.Repository;

public class ItemTrabajoRepository: BaseRepository<ItemsTrabajoEntity>, IItemTrabajoRepository
{
    private readonly BackEndContext _context;

    public ItemTrabajoRepository(BackEndContext backEnd) : base(backEnd)
    {
        _context=backEnd;
    }
    public async Task<List<ItemsTrabajoEntity>> GetAllAsync()
    {
        return await _context.Set<ItemsTrabajoEntity>()
            .Include(i => i.Usuario)
            .OrderByDescending(i => i.Relevancia)
            .ThenBy(i => i.FechaEntrega)
            .ToListAsync();
    }

    public async Task<ItemsTrabajoEntity?> GetByIdAsync(int id)
    {
        return await _context.Set<ItemsTrabajoEntity>()
            .Include(i => i.Usuario)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<List<ItemsTrabajoEntity>> GetPendientesUsuarioAsync(int usuarioId)
    {
        return await _context.Set<ItemsTrabajoEntity>()
            .Where(i =>
                i.UsuarioId == usuarioId &&
                i.Estado == EstadoItem.Pendiente)
            .Include(i => i.Usuario)
            .OrderByDescending(i => i.Relevancia)
            .ThenBy(i => i.FechaEntrega)
            .ToListAsync();
    }
}
