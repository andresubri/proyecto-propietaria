using System;
using System.Linq;
using System.Windows;
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

        private void DeleteSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            supplierOperations("Delete");
        }

        private void ExitSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EditSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            supplierOperations("Edit");
        }
        private async void supplierOperations(String option)
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
                    

            switch (option)
            {
                case "Delete":
                    try
                    {
                    var element = list.ElementAt(ID);
                    var query2 = from a in new ParseQuery<Models.Supplier>()
                                                     where a.Id.Equals(element.Id)
                                                     select a;
                    var aux = query2.FirstAsync().Result;
                        await aux.DeleteAsync();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show($"Error eliminando usuario\n{ex}");
                    }
                    
                    break;
                case "Edit":
                    var window = new EditSupplier();
                    window.Show();
                    break;
            }
        }
    }
}
