using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasPorPagar.Models
{
    class Supplier : Parse.ParseObject
    {
        public Supplier(int identification, int balance, string id, string name, string type, bool state)
        {
            this.identification = identification;
            this.balance = balance;
            this.id = id;
            this.name = name;
            this.type = type;
            this.state = state;
        }

        public int identification { get; set; }
        public int balance { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public bool state{ get; set; }
    }
}
