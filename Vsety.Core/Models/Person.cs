using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Bcpg.Sig;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vsety.Core.Models
{
    public class Person
    { 
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; } 
        [DataType(DataType.Text)]
        public string Surname { get; set; } 
        public string Gender { get; set;} 
        public string City { get; set; } 
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set;}
        [DataType(DataType.Text)]
        public string Nickname { get; set; }
        [DataType(DataType.Text)]
        public string? Description { get; set; }

        public IFormFile? avatar { get; set; }

        public Img? img { get; set; }
    }
}
