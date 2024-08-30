using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsety.DataAccess.Entities
{
    public class ImgEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        [Required]
        public string Path { get; set; } = string.Empty;



        public PostEntity? Post { get; set; }




        public Guid? PersonId { get; set; }
        public PersonEntity? Person { get; set; }
    }
}
