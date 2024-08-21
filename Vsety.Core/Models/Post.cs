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


    }
}
