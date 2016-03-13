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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CuentasPorPagar.Models;
using CuentasPorPagar.Views;
using Parse;
using SupplierReport = CuentasPorPagar.Views.Report.Supplier;
using Supplier = CuentasPorPagar.Views.CRUD.Supplier;
using Payments = CuentasPorPagar.Views.CRUD.Payments;

namespace CuentasPorPagar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var login = new Login();
            login.Show();
        }

        private void SupplierCrudItem_Click(object sender, RoutedEventArgs e)
        {
           var window = new Supplier();
            window.Show();
        }

        private void PaymentCrudItemm_Click(object sender, RoutedEventArgs e)
        {
            var window = new Payments();
            window.Show();

        }

        private void PaymentTypeCrudItem_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
        

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var window = new Views.CRUD.DocumentsEntry.DocumentsEntry();
            window.Show();
        }

        private void UserCrudItem_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new Views.CRUD.User();
            window.Show();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            SupplierReport sr = new SupplierReport();
            sr.Show();
        }

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var LoggedUser = ParseUser.CurrentUser.Username;                            
                var query = from a in new ParseQuery<Models.Users>()
                            where a.Username.Equals(LoggedUser)
                            select a;
                var TableResult = query.FirstAsync().Result;
                var UserPermission = TableResult.Permission;
                txtUserPermission.Content = "Permisos: "+UserPermission;
                 
                if (UserPermission == "Administrador")
                {
                    App.Current.Properties["IsAdmin"] = true; 
                }
                else
                {
                    App.Current.Properties["IsAdmin"] = false;
                }
                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
