using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vsety.DataAccess.Entities.ManyToMany;

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




       //public Guid? PersonId { get; set; }
        public PersonEntity? Person { get; set; }



        public ICollection<PostEntity> Posts { get; set; } = new List<PostEntity>();


        public List<PostEntity> PostLikes { get; set; } = [];

        public List<PostEntity> PostReposts { get; set; } = [];

        public List<CommentEntity> Comments { get; set; } = [];
    }
}
