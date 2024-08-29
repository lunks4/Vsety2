using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsety.Core.Models.ViewModel
{
    public class IndexViewModel
    {
        public User? User { get; set; }

        //public AddPostViewModel? AddPost { get; set; }

        public List<Post>? Posts { get; set; }
    }
}
