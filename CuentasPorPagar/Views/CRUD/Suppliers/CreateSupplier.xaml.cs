using System;
using System.Windows;
using System.Windows.Controls;

namespace CuentasPorPagar.Views.CRUD.Suppliers
{
    /// <summary>
    ///     Interaction logic for CreateSupplier.xaml
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
                var supplier = new Models.Supplier
                {
                    Name = SupplierName.Text,
                    Type = ((ComboBoxItem) PersonType.SelectedItem).Content.ToString(),
                    Identification = Identification.Text,
                    Balance = int.Parse(SupplierBalance.Text),
                    State = ((ComboBoxItem) StateCbx.SelectedItem).Content.ToString()
                };

                await supplier.SaveAsync();
                MessageBox.Show("Agregado satisfactoriamente");

                Close();
                var back = new Supplier();
                back.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar suplidor \n{ex}");
            }
        }
    }
}