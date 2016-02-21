using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
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


namespace CuentasPorPagar.Views.CRUD
{
    /// <summary>
    /// Interaction logic for PaymentConcept.xaml
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
                var query = new ParseQuery<Models.Payment>();
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async void DeletePaymentConceptBtn_Click(object sender, RoutedEventArgs e)
        {
            var ID = PaymentDgv.SelectedIndex;
            var query = new ParseQuery<Models.Payment>();
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
                var getObject = from a in new ParseQuery<Models.Payment>()
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
