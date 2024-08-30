using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsety.Core.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public DateTime Time {  get; set; }
        public IFormFile? file { get; set; }

        public Img? Img { get; set; }
        public string Description { get; set; } = String.Empty;

        public List<User>? UsersLikes { get; set; }

        public List<Comment>? UsersComments { get; set; }

        public List<User>? UsersReposts { get; set; }

        public User? User { get; set; }


    }
}
