using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parse;

namespace CuentasPorPagar
{
   static class Utilities
    {
        public static bool Validate(string args )

        {
            var vnTotal = 0;
            var vcCedula = args.Replace("-", "");
            var pLongCed = vcCedula.Trim().Length;
            var multiplier = new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };

            if (pLongCed < 11 || pLongCed > 11)
                return false;

            for (var vDig = 1; vDig <= pLongCed; vDig++)
            {
                var calculate = int.Parse(vcCedula.Substring(vDig - 1, 1)) * multiplier[vDig - 1];
                vnTotal += calculate < 10 ? calculate : int.Parse(calculate.ToString().Substring(0, 1)) + int.Parse(calculate.ToString().Substring(1, 1));
            }

            return vnTotal % 10 == 0;
        }

    }
}
