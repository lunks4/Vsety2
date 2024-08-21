using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vsety.DataAccess.Entities
{
    public class UserEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; } 
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }

        [ForeignKey("PersonEntity")]
        public Guid? PersonId { get; set; }
        public PersonEntity? Person { get; set; }
    }
}
