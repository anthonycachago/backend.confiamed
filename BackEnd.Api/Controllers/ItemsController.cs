using BackEnd.Bussines.ItemsTrabajo;
using BackEnd.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers;

[ApiController]
[Route("api/user/items")]
public class ItemsController:ControllerBase
{
    private readonly IItemTrabajoService _service;

    public ItemsController(
        IItemTrabajoService itemTrabajoService)
    {
        _service= itemTrabajoService;
    }
    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] ItemsTrabajoEntity item)
    {
        var result = await _service.CrearAsync(item);
        return Ok(result);
    }
    // Consultar todos los items
    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var items = await _service.ObtenerTodosAsync();

        return Ok(items);
    }

    // Consultar item por Id
    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var item = await _service.ObtenerPorIdAsync(id);

        if (item == null)
            return NotFound();

        return Ok(item);
    }

    // Consultar pendientes de un usuario
    [HttpGet("usuario/{usuarioId}/pendientes")]
    public async Task<IActionResult> ObtenerPendientesUsuario(int usuarioId)
    {
        var items = await _service.ObtenerPendientesUsuarioAsync(usuarioId);

        return Ok(items);
    }
}
