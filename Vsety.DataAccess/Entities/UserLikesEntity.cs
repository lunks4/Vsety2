using Org.BouncyCastle.Bcpg.Sig;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsety.DataAccess.Entities
{
    public class UserLikesEntity
    {
        public Guid Id { get; set; }
        
        public UserEntity User { get; set; }

        public List<PostEntity> Posts { get; set; }
    }
}
