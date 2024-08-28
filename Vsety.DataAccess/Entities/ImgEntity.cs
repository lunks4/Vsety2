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
        [Required]
        public string Path { get; set; } = string.Empty;
        //public Guid? PersonId { get; set; }
    }
}
