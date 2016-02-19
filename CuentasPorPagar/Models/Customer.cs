using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parse;
namespace CuentasPorPagar.Models
{
    class Customer : ParseObject
    {
        [ParseFieldName("objectId")]
        public string Id { get; set; }
        [ParseFieldName("name")]
        public string Name { get; set; }
        [ParseFieldName("balance")]
        public int Balance { get; set; }
    }
}
