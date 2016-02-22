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
using CuentasPorPagar.Views.CRUD;
using CuentasPorPagar.Views.CRUD.Suppliers;
using CuentasPorPagar.Views.CRUD.Users;
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

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateUser();
            window.Show();
        }
    }
}
