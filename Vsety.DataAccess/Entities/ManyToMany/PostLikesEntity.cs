using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsety.DataAccess.Entities.ManyToMany
{
    public class PostLikesEntity
    {
        public Guid UserId { get; set; }
        public UserEntity User { get; set; } = null!;


        public Guid PostId { get; set; }
        public PostEntity Post { get; set; } = null!;
    }
}
