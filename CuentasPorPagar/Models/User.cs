using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasPorPagar.Models
{
    class User : Parse.ParseObject
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Permission { get; set; }
    }
}
