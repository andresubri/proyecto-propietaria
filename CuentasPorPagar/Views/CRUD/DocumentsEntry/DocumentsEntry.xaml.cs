using Parse;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CuentasPorPagar.Views.CRUD.DocumentsEntry
{
    /// <summary>
    /// Interaction logic for DocumentsEntry.xaml
    /// </summary>
    public partial class DocumentsEntry : Window
    {

        public DocumentsEntry()
        {
            InitializeComponent();
        }


        private void CreateDocumentBtn_Click(object sender, RoutedEventArgs e)
        {
            Crud("guardar");
        }

        private  void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateGrid();
            bool isAdmin = Convert.ToBoolean(Application.Current.Properties["IsAdmin"]);
            if (isAdmin)
            {
                DeleteDocumentBtn.IsEnabled = true;
            }
        }

        private async void PopulateGrid()
        {
            try
            {
                var query = new ParseQuery<Models.DocumentEntry>();
                var result = await query.FindAsync();
                var list = from p in result
                           select new
                           {
                               Id = p.ObjectId,
                               Recibo = p.ReceiptNumber,
                               Concepto = p.Concept,
                               Monto = string.Format(new CultureInfo("en-US"), $"{p.Amount:c}"),
                               Suplidor = p.Supplier,
                               Estatus = p.Status,
                               Fecha = p.CreatedAt
                           };

                DocumentDgv.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Clear()
        {
            conceptTxt.Text = "";
            amountTxt.Text = "";
            supplierTxt.Text = "";
            numberTxt.Text = "";
            objectIdTxt.Text = "";
        }

        private async void Crud(string option)
        {
            var id = DocumentDgv.SelectedIndex;
            var query = new ParseQuery<Models.DocumentEntry>();
            var result = await query.FindAsync();
            var list = from p in result
                       select new
                       {
                           Id = p.ObjectId
                       };
            var element = "";
            if (id > 0) {
                 element = list.ElementAt(id).Id;
            }
                 
            
            
            switch (option.ToLower())
            {
                case "guardar":
                    if (conceptTxt.Text != "" && (int.Parse(amountTxt.Text)) > 0 ||
                        supplierTxt.Text != "" && (int.Parse(numberTxt.Text)) > 0)
                    {
                        try
                        {
                            var document = new Models.DocumentEntry();
                            if (!(objectIdTxt.Text.Length > 2))
                            {
                                document = new Models.DocumentEntry()
                                {
                                    //Crea nuevo registro
                                    Concept = conceptTxt.Text,
                                    Amount = int.Parse(amountTxt.Text),
                                    Supplier = supplierTxt.Text,
                                    ReceiptNumber = int.Parse(numberTxt.Text),
                                    Status = "pendiente"

                                };
                                await document.SaveAsync();
                                MessageBox.Show("Documento creado");
                            }
                            else {
                                document = new Models.DocumentEntry()
                                {
                                    //Actualiza registro
                                    ObjectId = objectIdTxt.Text,
                                    Concept = conceptTxt.Text,
                                    Amount = int.Parse(amountTxt.Text),
                                    Supplier = supplierTxt.Text,
                                    ReceiptNumber = int.Parse(numberTxt.Text),
                                };
                                await document.SaveAsync();
                                MessageBox.Show("Documento actualizado");
                            }
                            PopulateGrid();
                            Clear();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }

                 break;
                case "delete":
                    if (!(id < 0))
                    {

                        try
                        {
                            var deleteQuery = from o in new ParseQuery<Models.DocumentEntry>()
                                              where o.ObjectId.Equals(element)
                                              select o;
                            await deleteQuery.FirstAsync().Result.DeleteAsync();
                            PopulateGrid();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error eliminando documento\n{ex}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Favor seleccionar un elemento de la lista.");
                    }

                    break;
                
            }
        }

        private async void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var id = DocumentDgv.SelectedIndex;
            var query = new ParseQuery<Models.DocumentEntry>();
            var result = await query.FindAsync();
            var list = from p in result
                       select new
                       {
                           Id = p.ObjectId
                       };

            var element = list.ElementAt(id).Id;
            var editQuery = from aux in new ParseQuery<Models.DocumentEntry>()
                            where aux.ObjectId.Equals(element)
                            select aux;
            var editElements = await editQuery.FirstAsync();
            conceptTxt.Text = editElements.Concept;
            amountTxt.Text = editElements.Amount.ToString();
            numberTxt.Text = editElements.ReceiptNumber.ToString();
            supplierTxt.Text = editElements.Supplier.ToString();
            objectIdTxt.Text = editElements.ObjectId;
        }

        private void DeleteDocumentBtn_Click(object sender, RoutedEventArgs e)
        {
            Crud("Delete");
        }

        private void EditDocumentBtn_Click(object sender, RoutedEventArgs e)
        {
        }

        private void typeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void loadBtn_Click(object sender, RoutedEventArgs e)
        {
            save();
        }
        public async void save()
        {
            try
            {
                var id = DocumentDgv.SelectedIndex;
                var query = new ParseQuery<Models.DocumentEntry>();
                var result = await query.FindAsync();
                var list = from p in result
                           select new
                           {
                               Id = p.ObjectId
                           };

                var element = list.ElementAt(id).Id;
                var editQuery = from aux in new ParseQuery<Models.DocumentEntry>()
                                where aux.ObjectId.Equals(element)
                                select aux;
                var editElements = await editQuery.FirstAsync();
                var document = new Models.DocumentEntry()
        {
                    DocumentNumber = element,
                    Concept = conceptTxt.Text,
                    Amount = int.Parse(amountTxt.Text),
                    Supplier = supplierTxt.Text,
                    ReceiptNumber = int.Parse(numberTxt.Text)
                };

                await document.SaveAsync();
                MessageBox.Show("Documento actualizado");
                PopulateGrid();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bool isAdmin = Convert.ToBoolean(Application.Current.Properties["IsAdmin"]);
            if(isAdmin)
            {
                DeleteDocumentBtn.IsEnabled = true;
            }
        }

        private void ExitDocumentBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void loadSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            var findSupplier = new FindSupplier();
            findSupplier.ShowDialog();

            if (findSupplier.DialogResult == true)
            { 
                 supplierTxt.Text = findSupplier.supplier;
            }
        }

        private async void getNumberBtn_Click(object sender, RoutedEventArgs e)
        {
            var query = new ParseQuery<Models.DocumentEntry>().OrderByDescending("receiptNum");
            var result = await query.FindAsync();
            var list = from p in result
                       select new
                       {
                           Recibo = p.ReceiptNumber
                       };
            numberTxt.Text = (list.ElementAt(0).Recibo + 1).ToString();
        }
    }
}
