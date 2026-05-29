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
}
