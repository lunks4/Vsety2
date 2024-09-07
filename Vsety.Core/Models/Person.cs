using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Bcpg.Sig;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vsety.Core.Models
{
    public class Person
    { 
        
        public string Name { get; set; } 
        
        public string Surname { get; set; } 
        public string Gender { get; set;} 
        public string City { get; set; } 
        
        public string Birthday { get; set;}
        
        public string Nickname { get; set; }
        
        public string? Description { get; set; }

        public IFormFile? avatar { get; set; }

        public Img? img { get; set; }
    }
}
