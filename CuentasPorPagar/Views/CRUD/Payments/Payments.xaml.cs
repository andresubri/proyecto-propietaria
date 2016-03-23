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

        public async void PopulateWindow()
        {
            try
            {

                var query = await new ParseQuery<Payment>().FindAsync();
                var list = query.Select(p => new
                {
                    Id = p.ObjectId,
                    Concepto = p.Concept,
                    Monto = p.Amount,
                    Proveedor = p.Supplier,
                    Estado = p.State
                });

                PaymentDgv.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
               PopulateWindow();

                if (bool.Parse(Application.Current.Properties["IsAdmin"].ToString()))
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
            var list = result.Select(p => new {  Id = p.ObjectId });
            try
            {
                var element = list.ElementAt(ID).Id;
                var getObject = new ParseQuery<Payment>().Where(a => a.Id.Equals(element));
                await getObject.FirstAsync().Result.DeleteAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

       
    }
}