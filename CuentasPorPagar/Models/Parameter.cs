using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasPorPagar.Models
{
    class Parameter : Parse.ParseObject
    {
        public Parameter(string processedYear, string processedMonth, bool executedClosing)
        {
            ProcessedYear = processedYear;
            ProcessedMonth = processedMonth;
            ExecutedClosing = executedClosing;
        }

        public string ProcessedYear { get; set; }
        public string ProcessedMonth { get; set; }
        public bool ExecutedClosing { get; set; }
    }
}
