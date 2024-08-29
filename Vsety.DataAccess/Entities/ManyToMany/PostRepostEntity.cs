using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsety.DataAccess.Entities.ManyToMany
{
    public class PostRepostEntity
    {
        public Guid Id { get; set; }

        public UserRepostsEntity User { get; set; }

        public PostEntity Post { get; set; }
    }
}
