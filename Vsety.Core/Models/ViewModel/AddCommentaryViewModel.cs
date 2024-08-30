using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsety.Core.Models.ViewModel
{
    public class AddCommentaryViewModel
    {
        public Guid PostId { get; set; }

        public Guid UserId { get; set; }

        public string Comment { get; set; } = string.Empty;
    }
}
