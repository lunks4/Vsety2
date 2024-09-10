using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsety.Core.Models;
using Vsety.DataAccess.Entities.ManyToMany;

namespace Vsety.DataAccess.Entities
{
    public class PostEntity
    {

        public Guid Id { get; set; }

        public DateTime Time { get; set; }

        public string Description { get; set; } = String.Empty;


        public List<UserEntity> UserLikes { get; private set; } = [];

        public int countLikes { get; set; } = 0;

        public List<CommentEntity> UsersComments { get; private set; } = [];

        public int countComments { get; set; } = 0;

        public List<UserEntity> UserReposts { get; private set; } = [];

        public int countReposts { get; set; } = 0;

        public Guid? ImgId { get; set; }
        public ImgEntity? Img { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

    }
}
