using System.ComponentModel.DataAnnotations;

namespace Vsety.Core.Models.ViewModel
{
    public class RegisterViewModel
    {
        
        public string email { get; set; }
        
        public string password { get; set; }
        
        public string confirmPassword { get; set; }
    }
}
