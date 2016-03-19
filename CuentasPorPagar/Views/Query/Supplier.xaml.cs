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
        public Supplier()
        {
            InitializeComponent();
        }

        private async void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime? dateFrom = date1.SelectedDate;
            DateTime? dateTo = date2.SelectedDate;

            //Ejecuta la busqueda al presionar el boton

            try
            {

                var query = new ParseQuery<Models.Supplier>();

                if (nameTxt.Text != "")
                {
                    query.WhereMatches("name", nameTxt.Text);
                }

                if (documentTxt.Text != "")
                {
                    query.WhereMatches("identification", documentTxt.Text);
                }

                if (((ComboBoxItem)stateCmb.SelectedItem).Content.ToString() == "Activo" || ((ComboBoxItem)stateCmb.SelectedItem).Content.ToString() == "Inactivo")
                {
                    query.WhereContains("state", ((ComboBoxItem)stateCmb.SelectedItem).Content.ToString());
                }
                if (((ComboBoxItem)typeCmb.SelectedItem).Content.ToString() == "Fisica" || ((ComboBoxItem)typeCmb.SelectedItem).Content.ToString() == "Juridica")
                {
                    query.WhereContains("type", ((ComboBoxItem)typeCmb.SelectedItem).Content.ToString());
                }
                if (amountTxt1.Text != "")
                {
                    query.WhereGreaterThanOrEqualTo("balance", Int32.Parse(amountTxt1.Text));
                }
                if (amountTxt2.Text != "")
                {
                    query.WhereLessThanOrEqualTo("balance", Int32.Parse(amountTxt2.Text));
                }

                /*if (date1 != null)
                {
                    query.WhereGreaterThanOrEqualTo("createdAt", date1);
                }
                if (date2 != null)
                {
                    query.WhereLessThanOrEqualTo("createdAt", date2);
                }*/

                var result = await query.FindAsync();
                var list = from p in result
                           select new
                           {
                               Id = p.ObjectId,
                               Nombre = p.Name,
                               Identificacion = p.Identification,
                               Estado = p.State,
                               Tipo = p.Type,
                               p.Balance,
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
            //Popula el Datagrid al lanzarse
            var query = new ParseQuery<Models.Supplier>();
            var result = await query.FindAsync();
            var list = from p in result
                       select new
                       {
                           Id = p.ObjectId,
                           Nombre = p.Name,
                           Identificacion = p.Identification,
                           Estado = p.State,
                           Tipo = p.Type,
                           p.Balance,
                           Creado = p.CreatedAt
                       };
            dataGrid.ItemsSource = list;
           
        }
    }
}

