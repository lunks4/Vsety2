using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsety.Core.Models.ViewModel
{
    public class AutorisationViewModel
    {

        public string email { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;
    }
}
