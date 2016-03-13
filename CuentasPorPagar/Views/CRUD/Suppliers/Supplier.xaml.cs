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
                bool isAdmin = Convert.ToBoolean(Application.Current.Properties["IsAdmin"]);
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

                if (isAdmin)
                {
                    EditSupplierBtn.IsEnabled = true;
                    DeleteSupplierBtn.IsEnabled = true;
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void DeleteSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            Crud("Delete");
        }

        private void ExitSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EditSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            Crud("Edit");
            
        }
        private async void Crud(string option)
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
                   
            switch (option.ToLower())
            {
                case "delete":
                    try
                    {
                    var element = list.ElementAt(ID);
                    var deleteSupplier = from a in new ParseQuery<Models.Supplier>()
                                                     where a.Id.Equals(element.Id)
                                                     select a;

                        await deleteSupplier.FirstAsync().Result.DeleteAsync();
                        MessageBox.Show("Eliminado satisfactoriamente");
                        PopulateGrid();

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show($"Error eliminando usuario\n{ex}");
                    }
                    
                    break;
                case "edit":
                    var window = new EditSupplier();
                    window.Show();
                    break;
            }
        }
        private async void PopulateGrid()
        {
            try
            {
                var query = await new ParseQuery<Models.Supplier>().FindAsync();
                var result = from o in query
                    select new
                    {
                        Id = o.ObjectId,
                        Nombre = o.Name,
                        Identificacion = o.Identification,
                        o.Balance,
                        Creado = o.CreatedAt
                    };
                SupplierDgv.ItemsSource = result;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al popular \n{0}", ex.ToString());
            }
        }
    }
}
