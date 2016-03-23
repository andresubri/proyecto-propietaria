using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CuentasPorPagar.Models;
using Parse;

namespace CuentasPorPagar.Views.CRUD.DocumentsEntry
{
    /// <summary>
    ///     Interaction logic for DocumentsEntry.xaml
    /// </summary>
    public partial class DocumentsEntry : Window
    {
        public DocumentsEntry()
        {
            InitializeComponent();
        }

        private void CreateDocumentBtn_Click(object sender, RoutedEventArgs e)
        {
            Crud("save");
        }

        private async void PopulateGrid()
        {
            try
            {
                var query = await new ParseQuery<DocumentEntry>().FindAsync();
                var list = query.Select(p => new
                {
                    Id = p.ObjectId,
                    Recibo = p.ReceiptNumber,
                    Concepto = p.Concept,
                    Monto = Utilities.ToDopCurrencyFormat(p.Amount),
                    Suplidor = p.Supplier,
                    Estatus = p.Status,
                    Fecha = p.CreatedAt
                });

                DocumentDgv.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async void Crud(string option)
        {
            var id = DocumentDgv.SelectedIndex;
            var query = await new ParseQuery<DocumentEntry>().FindAsync();
            var list = query.Select( p => new { Id = p.ObjectId });
            var element = "";

            
                

            switch (option.ToLower())
            {
                case "save":
                    if (conceptTxt.Text != "" && (int.Parse(amountTxt.Text)) > 0 ||
                        supplierTxt.Text != "" && (int.Parse(numberTxt.Text)) > 0)
                    {
                        try
                        {
                            DocumentEntry document;
                            if (!(objectIdTxt.Text.Length > 2))
                            {
                                document = new DocumentEntry
                                {                                   
                                    Concept = conceptTxt.Text,
                                    Amount = int.Parse(amountTxt.Text),
                                    Supplier = supplierTxt.Text,
                                    ReceiptNumber = int.Parse(numberTxt.Text),
                                    Status = "pendiente"
                                };
                                await document.SaveAsync();
                                MessageBox.Show("Documento creado");
                            }
                            else
                            {
                                document = new DocumentEntry
                                { 
                                    ObjectId = objectIdTxt.Text,
                                    Concept = conceptTxt.Text,
                                    Amount = int.Parse(amountTxt.Text),
                                    Supplier = supplierTxt.Text,
                                    ReceiptNumber = int.Parse(numberTxt.Text)
                                };
                                await document.SaveAsync();
                                MessageBox.Show("Documento actualizado");
                            }
                            PopulateGrid();
                            Utilities.Clear(this);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }

                    break;

                case "delete":
                    element = list.ElementAt(id).Id;
                    if (!(id < 0))
                    {
                        try
                        {
                            var deleteQuery = new ParseQuery<DocumentEntry>()
                                .Where(o => o.ObjectId.Equals(element));
                            await deleteQuery.FirstAsync().Result.DeleteAsync();
                            PopulateGrid();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error eliminando documento\n{ex}");
                        }
                    }
                    else
                        MessageBox.Show("Favor seleccionar un elemento de la lista.");
                    
                    break;

                default:
                    MessageBox.Show("No parameters passed");
                    break;
            }
        }

        private async void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var id = DocumentDgv.SelectedIndex;
            var query = await new ParseQuery<DocumentEntry>().FindAsync();
            var list = query.Select(p => new {Id = p.ObjectId});

            var element = list.ElementAt(id).Id;
            var editQuery = from aux in new ParseQuery<DocumentEntry>()
                where aux.ObjectId.Equals(element)
                select aux;

           
            var editElements = await editQuery.FirstAsync();
            conceptTxt.Text = editElements.Concept;
            amountTxt.Text = editElements.Amount.ToString();
            numberTxt.Text = editElements.ReceiptNumber.ToString();
            supplierTxt.Text = editElements.Supplier;
            objectIdTxt.Text = editElements.ObjectId;
        }

        private void DeleteDocumentBtn_Click(object sender, RoutedEventArgs e)
        {
            Crud("delete");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (bool.Parse(Application.Current.Properties["IsAdmin"].ToString()))
                DeleteDocumentBtn.IsEnabled = true;

            PopulateGrid();
        }

        private void ExitDocumentBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            Utilities.Clear(this);
        }

        private void loadSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            var findSupplier = new FindSupplier();
            findSupplier.ShowDialog();
            this.supplierTxt.Text = findSupplier.supplier;
        }

        private async void getNumberBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var query = await new ParseQuery<DocumentEntry>().OrderByDescending("receiptNum").FindAsync();
                var list = query.Select(p => new { p.ReceiptNumber });
                numberTxt.Text = (list.ElementAt(0).ReceiptNumber + 1).ToString();
            }
            catch (Exception ex)
            {

               numberTxt.Text = "1";
            }
            
        }
    }
}