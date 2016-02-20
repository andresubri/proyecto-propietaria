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
using CuentasPorPagar.Models;
using CuentasPorPagar.Views.CRUD.Suppliers;
using Parse;

namespace CuentasPorPagar.Views.CRUD
{
    /// <summary>
    /// Interaction logic for Supplier.xaml
    /// </summary>
    public partial class Supplier : Window
    {
        public Supplier()
        {
            InitializeComponent();
            //var query = from Models.Supplier in Parse.ParseQuery<Models.Supplier>;
        }

        private void CreateSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateSupplier();
            window.Show();
            this.Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var query = new ParseQuery<Models.Supplier>();
                IEnumerable<Models.Supplier> result = await query.FindAsync();
                List<Models.Supplier> list = result.ToList();

                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
