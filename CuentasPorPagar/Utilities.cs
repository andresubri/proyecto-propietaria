using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CuentasPorPagar
{
   static class Utilities
    {
        public static bool ValidateRnc(string arg)

        {
            var vnTotal = 0;
            var vcCedula = arg.Replace("-", "");
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

       public static bool ValidatePassword(string password) => password.Length >= 6;
       public static bool ValidateEmail(string email)
       {
           var pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
           return Regex.IsMatch(email, pattern);
       }


    }
}
