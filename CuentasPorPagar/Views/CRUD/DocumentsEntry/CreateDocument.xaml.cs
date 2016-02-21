using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CuentasPorPagar.Views.CRUD.DocumentsEntry
{
    /// <summary>
    /// Interaction logic for CreateDocument.xaml
    /// </summary>
    public partial class CreateDocument : Window
    {
        public CreateDocument()
        {
            InitializeComponent();
        }

        private void CreateDocumentBtn_Copy_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var querySupplier = new ParseQuery<Models.Supplier>();
                var result = await querySupplier.FindAsync();
                var list = from p in result
                           select p.Name;
                          
                var dc = new Models.DocumentEntry();
                supplierLst.ItemsSource = list;
                var queryNum = new ParseQuery<Models.DocumentEntry>().OrderByDescending("createdAt");
                var resultNum = await queryNum.FirstAsync();
                int num = resultNum.ReceiptNumber + 1;
                numberTxt.Text = num.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async void createDocumentBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var document = new Models.DocumentEntry
                {
                    Concept = conceptTxt.Text,
                    Amount = int.Parse(amountTxt.Text),
                    Supplier = supplierLst.SelectedItem.ToString(),
                    ReceiptNumber = int.Parse(numberTxt.ToString())
                };
                await document.SaveAsync();
                MessageBox.Show("Documento creado");
                this.Close();
                var dw = new Views.CRUD.DocumentsEntry.DocumentsEntry();
                dw.Show();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void supplierLst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
