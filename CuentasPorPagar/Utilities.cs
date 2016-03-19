using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;

namespace CuentasPorPagar
{
    internal static class Utilities
    {
        public static bool ValidateRnc(string arg)

        {
            var vnTotal = 0;
            var vcCedula = arg.Replace("-", "");
            var pLongCed = vcCedula.Trim().Length;
            var multiplier = new int[11] {1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1};

            if (pLongCed < 11 || pLongCed > 11)
                return false;

            for (var vDig = 1; vDig <= pLongCed; vDig++)
            {
                var calculate = int.Parse(vcCedula.Substring(vDig - 1, 1))*multiplier[vDig - 1];
                vnTotal += calculate < 10
                    ? calculate
                    : int.Parse(calculate.ToString().Substring(0, 1)) + int.Parse(calculate.ToString().Substring(1, 1));
            }
            return vnTotal%10 == 0;
        }

        public static bool ValidatePassword(string password) => password.Length >= 6;

        public static bool ValidateEmail(string email)
        {
            var pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            return Regex.IsMatch(email, pattern);
        }

        public static void Clear(Visual window)
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(window);
            dynamic found;
            for (var i = 0; i < childrenCount; i++)
            {
                var child = (Visual) VisualTreeHelper.GetChild(window, i);
                if (child is TextBox)
                {
                    found = (TextBox) child;
                    found.Clear();
                }
                else if (child is ComboBox)
                {
                    found = (ComboBox) child;
                    found.SelectedIndex = 0;
                }

                Clear(child);
            }
        }

        public static string ToDopCurrencyFormat(int value) => $"{value:RD$#,##0.00;($#,##0.00);''}";
    }
}