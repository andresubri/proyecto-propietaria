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
using Parse;

namespace CuentasPorPagar.Views.Query
{
    /// <summary>
    /// Interaction logic for AdvanceDocument.xaml
    /// </summary>
    public partial class AdvanceDocument : Window
    {
        public AdvanceDocument()
        {
            InitializeComponent();
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            Utilities.ExportToPdf(this.DocumentDataGrid, "Reporte de documentos" + DateTime.Now.Date, "Reporte de documentos ");
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var state = ((ComboBoxItem)StateComboBox.SelectedItem).Content.ToString();

            var query = new ParseQuery<Models.DocumentEntry>();
            if (!string.IsNullOrEmpty(ConceptTextBox.Text)) 
                query = query.WhereMatches("concept", ConceptTextBox.Text);

            if (!string.IsNullOrEmpty(SupplierTextBox.Text))
                query = query.WhereEqualTo("supplier", SupplierTextBox.Text);

            if (!string.IsNullOrEmpty(BalanceTextBox.Text))
                query = query.WhereGreaterThanOrEqualTo("total_payment", int.Parse(BalanceTextBox.Text));

            if (StateComboBox.SelectedIndex > 0)
                query = query.WhereEqualTo("status", state);

           var result = await query.FindAsync();
           

            DocumentDataGrid.ItemsSource = result.Select(p => new
            {
                Id = p.ObjectId,
                Recibo = p.ReceiptNumber,
                Concepto = p.Concept,
                Total = Utilities.ToDopCurrencyFormat(p.TotalAmount),
                Abonado = Utilities.ToDopCurrencyFormat(p.Amount),
                Suplidor = p.Supplier,
                Estatus = p.Status,
                Fecha = p.CreatedAt
            });
        }
    }
}
