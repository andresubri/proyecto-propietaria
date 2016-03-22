using System;
using System.Collections.Generic;
using System.Data;
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
using Microsoft.Reporting.WinForms;
using Parse;

namespace CuentasPorPagar.Views.Report
{
    /// <summary>
    /// Interaction logic for Supplier.xaml
    /// </summary>
    public partial class Supplier : Window
    {
        public Supplier()
        {
            InitializeComponent();
        }

    

        private async void WindowsFormsHost_Loaded_1(object sender, RoutedEventArgs e)
        {
            var query = await new ParseQuery<Models.Supplier>().FindAsync();
            var result = query.Select(o => new
            {
                ID = o.Id,
                Nombre = o.Name,
                Balance = Utilities.ToDopCurrencyFormat(o.Balance),
                Tipo = o.Type,
                Estado = o.State

            }).ToDataTable();
            ReportDataSource dataSource = new ReportDataSource { Value = result };

            _reportViewer.LocalReport.DataSources.Add(dataSource);
            _reportViewer.LocalReport.ReportPath = "Reporte.rdlc";
            _reportViewer.RefreshReport();
        }
    }
}
