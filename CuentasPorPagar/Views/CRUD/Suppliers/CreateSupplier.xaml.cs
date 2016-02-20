using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace CuentasPorPagar.Views.CRUD.Suppliers
{
    /// <summary>
    /// Interaction logic for CreateSupplier.xaml
    /// </summary>
    public partial class CreateSupplier : Window
    {
        public CreateSupplier()
        {
            InitializeComponent();
        }

        private void CleanValuesBtn_Click(object sender, RoutedEventArgs e)
        {
            SupplierName.Text = "";
            SupplierBalance.Text = "";
            Identification.Text = "";
        }

        private async void CreateSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               

                var supplier = new ParseObject("Supplier");
                supplier["name"] = SupplierName.Text;
                supplier["type"] = ((ComboBoxItem)PersonType.SelectedItem).Content.ToString();
                supplier["identification"] = Identification.Text;
                supplier["balance"] = SupplierBalance.Text;
                supplier["state"] = ((ComboBoxItem)StateCbx.SelectedItem).Content.ToString();

                await supplier.SaveAsync();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
