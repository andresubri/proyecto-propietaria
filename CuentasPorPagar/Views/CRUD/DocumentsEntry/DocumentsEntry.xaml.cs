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
            documentOperations("Create");
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            populateGrid();
        }

        private async void populateGrid()
        {
            try
            {
                var query = new ParseQuery<Models.DocumentEntry>();
                var result = await query.FindAsync();
                var list = from p in result
                           select new
                           {
                               Id = p.ObjectId,
                               Recibo = p.ReceiptNumber.ToString(),
                               Fecha = p.DocumentDate.ToString(),
                               Monto= p.Amount.ToString(),
                               Suplidor = p.Supplier
                           };

                DocumentDgv.ItemsSource = list;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void clear()
        {
            conceptTxt.Text = "";
            amountTxt.Text = "";
            supplierTxt.Text = "";
            numberTxt.Text = "";
        }

        private async void documentOperations(String option)
        {
            var ID = DocumentDgv.SelectedIndex;
            var query = new ParseQuery<Models.DocumentEntry>();
            var result = await query.FindAsync();
            var list = from p in result
                     select p.ObjectId;
            var element = "";
            if (ID < 0)
            {
            }
            else
            {
                 element = list.ElementAt(ID);
            }


            switch (option)
            {
                case "Create":
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
                        populateGrid();
                        clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;
                case "Delete":
                    try
                    {
                        var query2 = from a in new ParseQuery<Models.DocumentEntry>()
                                     where a.ObjectId.Equals(element)
                                     select a;
                        var aux = query2.FirstAsync().Result;
                        await aux.DeleteAsync();
                        this.Close();
                        var de = new DocumentsEntry();
                        de.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error eliminando documento\n{ex}");
                    }

                    break;
                case "Edit":
                    try
                    {

                        var query2 = from aux in new ParseQuery<Models.DocumentEntry>()
                                     where aux.ObjectId.Equals(element)
                                     select aux;
                        var element1 = await query2.FirstAsync();
                        conceptTxt.Text = element1.Concept;
                        amountTxt.Text = element1.Amount.ToString();
                        numberTxt.Text = element1.ReceiptNumber.ToString();

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
            documentOperations("Delete");
        }

        private void EditDocumentBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void typeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
