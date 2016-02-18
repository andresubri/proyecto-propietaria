using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasPorPagar.Models
{
    class Customer : Parse.ParseObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Balance { get; set; }


    }
}
