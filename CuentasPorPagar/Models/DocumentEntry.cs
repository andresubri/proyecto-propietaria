using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasPorPagar.Models
{
    class DocumentEntry : Parse.ParseObject
    {
        public DocumentEntry(int documentNumber, int receiptNumber, string documentDate, int amount, string registerDate, string supplier, bool state)
        {
            DocumentNumber = documentNumber;
            ReceiptNumber = receiptNumber;
            DocumentDate = documentDate;
            Amount = amount;
            RegisterDate = registerDate;
            Supplier = supplier;
            this.state = state;
        }

        public int DocumentNumber { get; set; }
        public int ReceiptNumber { get; set; }
        public string DocumentDate { get; set; }
        public int Amount { get; set; }
        public string RegisterDate { get; set; }
        public string Supplier { get; set; }
        public bool state { get; set; }
    }
}
