

using System.ComponentModel.DataAnnotations;

namespace BackEnd.Core.Models;

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; } = null!;
}
