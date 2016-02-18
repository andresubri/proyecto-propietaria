using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasPorPagar
{
    class PaymentConcept : Parse.ParseObject
    {
        public PaymentConcept(string id, string description, bool state)
        {
            Id = id;
            Description = description;
            State = state;
        }
        public string Id { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
    }
}
