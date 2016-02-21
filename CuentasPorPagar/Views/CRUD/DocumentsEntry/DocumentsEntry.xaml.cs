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
            var cd = new CRUD.DocumentsEntry.CreateDocument();
            cd.Show();
            this.Close();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var query = new ParseQuery<Models.DocumentEntry>();
                var result = await query.FindAsync();
                var list = from p in result
                           select new
                           {
                               Id = p.ObjectId,
                               Nombre = p.ReceiptNumber,
                               DocumentDate = p.DocumentDate,
                               Amount = p.Amount,
                               Supplier = p.Supplier,
                               State = p.State

                           };

                DocumentDgv.ItemsSource = list;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
