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
                var result = await query.FindAsync();
                var list = from p in result
                    select new
                    {
                        Id = p.ObjectId,
                        Nombre = p.Name,
                        Identificacion = p.Identification,
                        p.Balance,
                        Creado = p.CreatedAt
                        
                    };
                
                SupplierDgv.ItemsSource = list;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private async void DeleteSupplierBtn_Click(object sender, RoutedEventArgs e)
        {

            var ID = SupplierDgv.SelectedIndex;
            var query = new ParseQuery<Models.Supplier>();
            var result = await query.FindAsync();
            var list = from p in result
                       select new
                       {
                           Id = p.ObjectId,
                           Nombre = p.Name,
                           Identificacion = p.Identification,
                           p.Balance,
                           Creado = p.CreatedAt

                       };

            try
            {

                var element = list.ElementAt(ID);
                var query2 = from a in new ParseQuery<Models.Supplier>()
                    where a.Id.Equals(element.Id)
                    select a;

               var aux = query2.FirstAsync().Result;
               await aux.DeleteAsync();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }





        }

        private void ExitSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
