using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Parse;

namespace CuentasPorPagar.Views.CRUD
{
    /// <summary>
    ///     Interaction logic for Supplier.xaml
    /// </summary>
    public partial class Supplier : Window
    {
        public Supplier()
        {
            InitializeComponent();
        }

        private void CreateSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            Crud("save");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                
                PopulateGrid();

                if (bool.Parse(Application.Current.Properties["IsAdmin"].ToString()))
                    DeleteSupplierBtn.IsEnabled = true;
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

        private async void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var id = SupplierDgv.SelectedIndex;
            var query = await new ParseQuery<Models.Supplier>().FindAsync();
            var result = query.Select(o => new {Id = o.ObjectId});
            var element = result.ElementAt(id).Id;
            var editQuery = from p in new ParseQuery<Models.Supplier>()
                where p.ObjectId.Equals(element)
                select p;

            var editElements = await editQuery.FirstAsync();

            IdTxt.Text = editElements.ObjectId;
            NameTxt.Text = editElements.Name;
            IdentificationTxt.Text = editElements.Identification;
            BalanceTxt.Text = editElements.Balance.ToString();
            TypeCbx.SelectedIndex = (editElements.Type.Equals("Juridica")) ? 1 : 2;
            StateCbx.SelectedIndex = (editElements.Type.Equals("Activo")) ? 1 : 2;
        }


        private async void Crud(string option)
        {
            var ID = SupplierDgv.SelectedIndex;
            var query = await new ParseQuery<Models.Supplier>().FindAsync();
            var list = query.Select(p => new
            {
                Id = p.ObjectId,
                Nombre = p.Name,
                Identificacion = p.Identification,
                p.Balance,
                Creado = p.CreatedAt
            });

            switch (option.ToLower())
            {
                //TODO: Identification validation. Returns false
                case "save":
                    if (NameTxt.Text != "" && IdentificationTxt.Text != ""
                        && int.Parse(BalanceTxt.Text) > 0)
                    {
                        try
                        {
                            Models.Supplier supplier;
                            if (!(IdTxt.Text.Length > 2))
                            {
                                supplier = new Models.Supplier
                                {
                                    Name = NameTxt.Text,
                                    Balance = int.Parse(BalanceTxt.Text),
                                    Identification = IdentificationTxt.Text,
                                    State = ((ComboBoxItem) StateCbx.SelectedItem).Content.ToString(),
                                    Type = ((ComboBoxItem) TypeCbx.SelectedItem).Content.ToString()
                                };
                                await supplier.SaveAsync();
                                MessageBox.Show("Suplidor creado");
                            }
                            else
                            {
                                supplier = new Models.Supplier
                                {
                                    ObjectId = IdTxt.Text,
                                    Name = NameTxt.Text,
                                    Balance = int.Parse(BalanceTxt.Text),
                                    Identification = IdentificationTxt.Text,
                                    State = ((ComboBoxItem) StateCbx.SelectedItem).Content.ToString(),
                                    Type = ((ComboBoxItem) StateCbx.SelectedItem).Content.ToString()
                                };
                                await supplier.SaveAsync();
                                MessageBox.Show("Suplidor actualizado");
                            }
                            PopulateGrid();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }

                    break;
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
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error eliminando usuario\n{ex}");
                    }

                    break;
            }
        }

        private async void PopulateGrid()
        {
            try
            {
                var query = await new ParseQuery<Models.Supplier>().FindAsync();
                var result = query.Select(o => new
                {
                    Id = o.ObjectId,
                    Nombre = o.Name,
                    Identificacion = o.Identification,
                    Balance = Utilities.ToDopCurrencyFormat(o.Balance),
                    Creado = o.CreatedAt
                });

                SupplierDgv.ItemsSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al popular \n{0}", ex.ToString());
            }
        }

        private void ClearBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Utilities.Clear(this);
        }
    }
}