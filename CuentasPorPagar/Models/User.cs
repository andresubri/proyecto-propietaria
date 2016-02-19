using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parse;
namespace CuentasPorPagar.Models
{
    [ParseClassName("User")]
    class User : ParseObject
    {
        [ParseFieldName("username")]
        public string Username { get; set; }
        [ParseFieldName("name")]
        public string Name { get; set; }
        [ParseFieldName("password")]
        public string Password { get; set; }
        [ParseFieldName("permission")]
        public string Permission { get; set; }
    }
}
