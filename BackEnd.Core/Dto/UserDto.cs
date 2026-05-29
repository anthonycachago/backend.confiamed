
using BackEnd.Core.Models;

namespace BackEnd.Core.Dto;

public class UserDto
{
    public string Username { get; set; }
    public List<ItemsTrabajoEntity> ItemsTrabajo { get; set; } = new();
}
