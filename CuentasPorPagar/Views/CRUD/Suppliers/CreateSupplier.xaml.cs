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

        private  void CreateSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                Models.Supplier supplier = new Models.Supplier();
                supplier.Name = SupplierName.Text;
                supplier.Type = ((ComboBoxItem)PersonType.SelectedItem).Content.ToString();
                supplier.Identification = int.Parse(Identification.Text);
                supplier.Balance = int.Parse(SupplierBalance.Text);
                supplier.State = ((ComboBoxItem)StateCbx.SelectedItem).Content.ToString();                
                supplier.SaveAsync().Wait();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
