using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CuentasPorPagar.Views.Query
{
    /// <summary>
    /// Interaction logic for Supplier.xaml
    /// </summary>
    public partial class Supplier : Window
    {
        private ParseQuery<Models.Supplier> Query = new ParseQuery<Models.Supplier>();
        public Supplier()
        {
            InitializeComponent();
        }

        private async void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime? dateFrom = date1.SelectedDate;
            DateTime? dateTo = date2.SelectedDate;

            try
            {
                
                if (!string.IsNullOrEmpty(nameTxt.Text))
                    Query.WhereMatches("name", nameTxt.Text);
                
                if (!string.IsNullOrEmpty(documentTxt.Text))
                    Query.WhereMatches("identification", documentTxt.Text);
                
                if (((ComboBoxItem)stateCmb.SelectedItem).Content.ToString() == "Activo" 
                    || ((ComboBoxItem)stateCmb.SelectedItem).Content.ToString() == "Inactivo")
                    Query.WhereContains("state", ((ComboBoxItem)stateCmb.SelectedItem).Content.ToString());
                
                if (((ComboBoxItem)typeCmb.SelectedItem).Content.ToString() == "Fisica" 
                    || ((ComboBoxItem)typeCmb.SelectedItem).Content.ToString() == "Juridica")
                    Query.WhereContains("type", ((ComboBoxItem)typeCmb.SelectedItem).Content.ToString());
                
                if (!string.IsNullOrEmpty(amountTxt1.Text))
                    Query.WhereGreaterThanOrEqualTo("balance", int.Parse(amountTxt1.Text));
                
                if (string.IsNullOrEmpty(amountTxt2.Text))
                    Query.WhereLessThanOrEqualTo("balance", int.Parse(amountTxt2.Text));
                

                /*if (date1 != null)
                {
                    Query.WhereGreaterThanOrEqualTo("createdAt", date1);
                }
                if (date2 != null)
                {
                    Query.WhereLessThanOrEqualTo("createdAt", date2);
                }*/

                var result = await Query.FindAsync();
                var list = from p in result
                           select new
                           {
                               Id = p.ObjectId,
                               Nombre = p.Name,
                               Identificacion = p.Identification,
                               Estado = p.State,
                               Tipo = p.Type,
                               Balance = Utilities.ToDopCurrencyFormat(p.Balance),
                               Creado = p.CreatedAt
                           };
                dataGrid.ItemsSource = list;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            stateCmb.SelectedIndex = 0;
            typeCmb.SelectedIndex = 0;
            

            var query = await new ParseQuery<Models.Supplier>().FindAsync();
            var list = query.Select(p => new
            {
                Id = p.ObjectId,
                Nombre = p.Name,
                Identificacion = p.Identification,
                Estado = p.State,
                Tipo = p.Type,
                Balance = Utilities.ToDopCurrencyFormat(p.Balance),
                Creado = p.CreatedAt
            });
                      
            dataGrid.ItemsSource = list;
           
        }
    }
}

