using System;
using System.Linq;
using System.Windows;
using CuentasPorPagar.Models;
using CuentasPorPagar.Views;
using CuentasPorPagar.Views.CRUD;
using CuentasPorPagar.Views.CRUD.DocumentsEntry;
using Parse;
using Supplier = CuentasPorPagar.Views.CRUD.Supplier;
using SupplierReport = CuentasPorPagar.Views.Report.Supplier;

namespace CuentasPorPagar
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var window = new Login();
            window.Show();
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


        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var window = new DocumentsEntry();
            window.Show();
        }

        private void UserCrudItem_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new User();
            window.Show();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            var window = new SupplierReport();
            window.Show();
        }

        private async void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var query = new ParseQuery<DocumentEntry>().WhereContains("status", "pendiente").OrderBy("createdAt");
                var result = await query.FindAsync();
                var list = from p in result
                    select new
                    {
                        Id = p.ObjectId,
                        Suplidor = p.Supplier,
                        Recibo = p.ReceiptNumber,
                        Monto = Utilities.ToDOPCurrencyFormat(p.Amount),
                        Fecha = p.CreatedAt
                    };
                var sum = result.Sum(o => o.Amount);
                PaymentLeft.Content += sum.ToString();

                dataGrid.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var loggedUser = ParseUser.CurrentUser.Username;
                var query = new ParseQuery<Users>().Where(a => a.Username.Equals(loggedUser));
                var tableResult = query.FirstAsync().Result.Permission;

                txtUserPermission.Content = $"Tipo: {tableResult}";
                Application.Current.Properties["IsAdmin"] = tableResult == "Administrador";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            var supplierQuery = new Views.Query.Supplier();
            supplierQuery.Show();
        }
    }
}