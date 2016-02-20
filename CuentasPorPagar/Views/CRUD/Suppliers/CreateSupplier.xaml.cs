using System;
using System.Windows;
using System.Windows.Controls;


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
                Models.Supplier supplier = new Models.Supplier();
                supplier.Name = SupplierName.Text;
                supplier.Type = ((ComboBoxItem)PersonType.SelectedItem).Content.ToString();
                supplier.Identification = Identification.Text;
                supplier.Balance = int.Parse(SupplierBalance.Text);
                supplier.State = ((ComboBoxItem)StateCbx.SelectedItem).Content.ToString();                
                await supplier.SaveAsync();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar suplidor");
                throw;
            }

        }
    }
}
