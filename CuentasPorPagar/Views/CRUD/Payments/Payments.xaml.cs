using System;
using System.Linq;
using System.Windows;
using CuentasPorPagar.Models;
using Parse;

namespace CuentasPorPagar.Views.CRUD
{
    /// <summary>
    ///     Interaction logic for PaymentConcept.xaml
    /// </summary>
    public partial class Payments : Window
    {
        public Payments()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var isAdmin = Convert.ToBoolean(Application.Current.Properties["IsAdmin"]);
                var query = new ParseQuery<Payment>();
                var result = await query.FindAsync();
                var list = from p in result
                    select new
                    {
                        Id = p.ObjectId,
                        Concepto = p.Concept,
                        Monto = p.Amount,
                        Proveedor = p.Supplier,
                        Estado = p.State
                    };

                PaymentDgv.ItemsSource = list;

                if (isAdmin)
                {
                    EditPaymentConceptBtn.IsEnabled = true;
                    DeletePaymentConceptBtn.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async void DeletePaymentConceptBtn_Click(object sender, RoutedEventArgs e)
        {
            var ID = PaymentDgv.SelectedIndex;
            var query = new ParseQuery<Payment>();
            var result = await query.FindAsync();
            var list = from p in result
                select new
                {
                    Id = p.ObjectId,
                    Concepto = p.Concept,
                    Monto = p.Amount,
                    Proveedor = p.Supplier,
                    Estado = p.State
                };
            try
            {
                var element = list.ElementAt(ID);
                var getObject = from a in new ParseQuery<Payment>()
                    where a.Id.Equals(element.Id)
                    select a;

                await getObject.FirstAsync().Result.DeleteAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}