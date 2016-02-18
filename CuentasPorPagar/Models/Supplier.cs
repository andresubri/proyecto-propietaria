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
            Identification = identification;
            Balance = balance;
            Id = id;
            Name = name;
            Type = type;
            State = state;
        }

        public int Identification { get; set; }
        public int Balance { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool State{ get; set; }
    }
}
