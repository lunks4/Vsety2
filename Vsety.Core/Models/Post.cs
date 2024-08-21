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
        public int Id { get; set; }
        public Person? Person { get; set; }
        public TimeOnly Time {  get; set; }
        public Img? Img { get; set; }
        public string Description { get; set; } = String.Empty;

        public int likes { get; set; }

        public List<User> UsersLikes { get; set; }

        public List<Comment> UsersComments { get; set; }

        public List<User> UsersReposts { get; set; }


    }
}
