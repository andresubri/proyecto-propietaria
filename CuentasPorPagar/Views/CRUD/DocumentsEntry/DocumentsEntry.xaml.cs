using Parse;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

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
            Crud("save");
        }
        
        private async void PopulateGrid()
        {
            try
            {
                var query = await new ParseQuery<Models.DocumentEntry>().FindAsync();
                var list = query.Select(p => new
                {
                    Id = p.ObjectId,
                    Recibo = p.ReceiptNumber,
                    Concepto = p.Concept,
                    Monto = Utilities.ToDOPCurrencyFormat(p.Amount),
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
            var query = new ParseQuery<Models.DocumentEntry>();
            var result = await query.FindAsync();
            var list = from p in result
                       select new
                       {
                           Id = p.ObjectId
                       };
            var element = "";
            if (id > 0) 
                 element = list.ElementAt(id).Id;
            
            switch (option.ToLower())
            {
                case "save":
                    if (conceptTxt.Text != "" && (int.Parse(amountTxt.Text)) > 0 ||
                        supplierTxt.Text != "" && (int.Parse(numberTxt.Text)) > 0)
                    {
                        try
                        {
                            Models.DocumentEntry document;
                            if (!(objectIdTxt.Text.Length > 2))
                            {
                                document = new Models.DocumentEntry()
                                {
                                    //CREATE
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
                                document = new Models.DocumentEntry()
                                {
                                    //UPDATE 
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
                            Utilities.Clear(this);
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
                            var deleteQuery =  new ParseQuery<Models.DocumentEntry>()
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
            var query = await new ParseQuery<Models.DocumentEntry>().FindAsync(); 
            var list = query.Select(p => new { Id = p.ObjectId });

            var element = list.ElementAt(id).Id;
            var editQuery = from aux in new ParseQuery<Models.DocumentEntry>()
                            where aux.ObjectId.Equals(element)
                            select aux;

            //Fill the controls
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
            var isAdmin = Convert.ToBoolean(Application.Current.Properties["IsAdmin"]);
            if(isAdmin)
                DeleteDocumentBtn.IsEnabled = true;

            PopulateGrid();

        }

        private void ExitDocumentBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            Utilities.Clear(this);
        }

        private void loadSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            var findSupplier = new FindSupplier();
            findSupplier.ShowDialog();

            if (findSupplier.DialogResult == true)
                 supplierTxt.Text = findSupplier.supplier;
            
        }

        private async void getNumberBtn_Click(object sender, RoutedEventArgs e)
        {
            var query = await new ParseQuery<Models.DocumentEntry>().OrderByDescending("receiptNum").FindAsync(); 
            var list = query.Select(p => new {  p.ReceiptNumber });
            numberTxt.Text = (list.ElementAt(0).ReceiptNumber + 1).ToString();
        }
    }
}
