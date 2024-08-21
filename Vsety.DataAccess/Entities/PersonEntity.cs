using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vsety.DataAccess.Entities
{
    public class PersonEntity
    {
        [Required]
        public Guid Id { get; set; }
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
        //public Guid? UserId { get; set; }
        [ForeignKey("ImgEntity")]
        public Guid ImgId { get; set; } = Guid.Empty;
        public ImgEntity? img { get; set; }
    }
}
