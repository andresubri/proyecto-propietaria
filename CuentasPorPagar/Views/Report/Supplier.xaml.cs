using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Microsoft.Reporting.WinForms;
using Parse;
using System.Threading.Tasks;

namespace CuentasPorPagar.Views.Report
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

        private void WindowsFormsHost_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                var suppliers =  GetSuppliers();
                
                var reportData = new ReportDataSource("Reporte", suppliers);
                _reportViewer.LocalReport.DataSources.Add(reportData);
                _reportViewer.RefreshReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }       
        //Eso es un force muy grande
        public async Task<IEnumerable<dynamic>> GetSuppliers()
        {
            var query = await new ParseQuery<Models.Supplier>().FindAsync();
            var result = query.Select(o => new
            {
                ID = o.Id,
                Nombre = o.Name,
                Balance = Utilities.ToDopCurrencyFormat(o.Balance),
                Tipo = o.Type,
                Estado = o.State

            });

            return result.AsEnumerable();
        }
        


    }
}