using System.ComponentModel.DataAnnotations;

namespace Vsety.API.Contracts.Users
{
    public record RegisterUserRequest (
        [Required] string Mail,
        [Required] string Password);
    
}
