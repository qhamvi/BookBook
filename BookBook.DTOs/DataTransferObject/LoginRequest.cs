using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookBook.DTOs;

public record LoginRequest
{
    [Required(ErrorMessage = "Username is required")]
    public string? UserName { get; init; }
    
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; init; }
}
