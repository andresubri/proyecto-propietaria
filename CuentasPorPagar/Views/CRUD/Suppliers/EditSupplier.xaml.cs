using Parse;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CuentasPorPagar.Views.CRUD.Suppliers
{
    /// <summary>
    /// Interaction logic for EditSupplier.xaml
    /// </summary>
    public partial class EditSupplier : Window
    {
        public string objectId;

        public EditSupplier()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var query = from a in new ParseQuery<Models.Supplier>()
                            where a.Name.Equals(SupplierName.Text)
                            select a;

                var aux = query.FirstAsync().Result;
                SupplierName.Text = aux.Name;
                Identification.Text = aux.Identification.ToString();
                SupplierBalance.Text = aux.Balance.ToString();
                PersonType.SelectedIndex = aux.Type == "Fisica" ? 0 : 1;
                StateCbx.SelectedIndex = aux.State == "Pendiente" ? 0 : 1;
                objectId = aux.ObjectId;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando datos: Editar Suplidor\n{0}", ex.ToString());
            }
        }

        private async void EditSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var supplier = new Models.Supplier
                {
                    Name = SupplierName.Text,
                    Type = ((ComboBoxItem) PersonType.SelectedItem).Content.ToString(),
                    Identification = Identification.Text,
                    Balance = int.Parse(SupplierBalance.Text),
                    State = ((ComboBoxItem) StateCbx.SelectedItem).Content.ToString(),
                    ObjectId = objectId
                };

                await supplier.SaveAsync();
                MessageBox.Show("Editado satisfactoriamente");

                this.Close();
                var back = new Supplier();
                back.Show();
            }
            catch
            {
                MessageBox.Show("Error actualizando datos");
            }
            }
    }
}
