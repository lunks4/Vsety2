using Org.BouncyCastle.Crypto.Agreement.Srp;
using Org.BouncyCastle.Utilities.IO;
using System.ComponentModel.DataAnnotations;

namespace Vsety.Core.Models
{
    public class User
    {
        public User(Guid id, string mail, string password)
        {
            Id = id;
            Mail = mail;
            PasswordHash = password;
        }

        [Key]
        [Required]
        public Guid Id { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Mail { get; private set; } 
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PasswordHash { get; private set; }
        public Person? Person { get; set; }
    }
}
