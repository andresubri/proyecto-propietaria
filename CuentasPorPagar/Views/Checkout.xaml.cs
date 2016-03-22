using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Parse;

namespace CuentasPorPagar.Views
{
    /// <summary>
    /// Interaction logic for Checkout.xaml
    /// </summary>
    public partial class Checkout : Window
    {

        public Checkout()
        {
            InitializeComponent();
            
        }

        public int CurrentAmount { get; set; }

        private void AmounTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

            var amount = (TextBox) sender;
            try
            {
                if (Regex.IsMatch(amount.Text, "\\D"))
                {
                    var index = amount.SelectionStart - 1;
                    amount.Text = amount.Text.Remove(index, 1);

                    amount.SelectionStart = index;
                    amount.SelectionLength = 0;
                }
            }
            catch (Exception)
            {
            }

        }

        private  async void PaymentButton_OnClick(object sender, RoutedEventArgs e)
        {
            var amount = CurrentAmount - int.Parse(AmounTxt.Text);
             var payment = new Models.Payment
             {
                 Supplier = SupplierNameLbl.Content.ToString(),
                 Concept = ConceptLabel.Content.ToString(),
                 Amount = amount,
                 State = amount.Equals(0) ? "Completado" : "Abonado"
             };

             await payment.SaveAsync();

            var query = await new ParseQuery<Models.DocumentEntry>()
                .Where(p => p.ObjectId.Equals(CheckoutNumberLbl.Content.ToString())).FirstAsync();

            query.Amount = amount;
            query.Status = (amount.Equals(0)) ? "Pagado" : "Pendiente";
            await query.SaveAsync();

            this.Close();
        }
    }
}
