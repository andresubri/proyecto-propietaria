using Parse;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            Crud("Create");
        }

        private  void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateGrid();
            bool isAdmin = Convert.ToBoolean(Application.Current.Properties["IsAdmin"]);
            if (isAdmin)
            {
                EditDocumentBtn.IsEnabled = true;
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
                               Fecha = p.DocumentDate,
                               Monto= p.Amount,
                               Suplidor = p.Supplier
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
        }

        private async void Crud(string option)
        {
            var id = DocumentDgv.SelectedIndex;
            var query = new ParseQuery<Models.DocumentEntry>();
            var result = await query.FindAsync();
            var list = from p in result
                     select p.ObjectId;
            var element = "";
            if (id < 0)
                element = list.ElementAt(id);
            
            
            switch (option.ToLower())
            {
                case "create":
                    if (conceptTxt.Text != "" && (int.Parse(amountTxt.Text)) > 0 ||
                        supplierTxt.Text != "" && (int.Parse(numberTxt.Text)) > 0)
                    {
                        try
                        {
                            var document = new Models.DocumentEntry()
                            {
                                Concept = conceptTxt.Text,
                                Amount = int.Parse(amountTxt.Text),
                                Supplier = supplierTxt.Text,
                                ReceiptNumber = int.Parse(numberTxt.Text)
                            };
                            await document.SaveAsync();
                            MessageBox.Show("Documento creado");
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

                    break;
                case "edit":
                    try
                    {

                        var editQuery = from aux in new ParseQuery<Models.DocumentEntry>()
                                     where aux.ObjectId.Equals(element)
                                     select aux;

                        var editElements = await editQuery.FirstAsync();
                        conceptTxt.Text = editElements.Concept;
                        amountTxt.Text = editElements.Amount.ToString();
                        numberTxt.Text = editElements.ReceiptNumber.ToString();


                        PopulateGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;
            }
            
        }

        private void DeleteDocumentBtn_Click(object sender, RoutedEventArgs e)
        {
            Crud("Delete");
        }

        private void EditDocumentBtn_Click(object sender, RoutedEventArgs e)
        {
            Crud("Edit");
        }

        private void typeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
