using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsety.Core.Models.ViewModel
{
    public class AddPostViewModel
    {
        public User User { get; set; }
        public string Description { get; set; } = string.Empty;

        public IFormFile? file { get; set; }
    }
}
