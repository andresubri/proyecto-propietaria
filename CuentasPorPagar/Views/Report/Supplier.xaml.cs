using System;
using System.Linq;
using System.Windows;
using Microsoft.Reporting.WinForms;
using Parse;

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

        private async void WindowsFormsHost_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var query = new ParseQuery<Models.Supplier>();
                var result = await query.FindAsync();
                var list = from p in result
                    select new
                    {
                        Id = p.ObjectId,
                        Nombre = p.Name,
                        Identificacion = p.Identification,
                        p.Balance,
                        Creado = p.CreatedAt
                    };


                var rds = new ReportDataSource();
                rds.Value = list.ToList();
                rds.Name = "DataSet1";
                _reportViewer.LocalReport.DataSources.Clear();
                _reportViewer.LocalReport.DataSources.Add(rds);
                _reportViewer.LocalReport.ReportEmbeddedResource = "Report1.rdlc";
                _reportViewer.LocalReport.ReportPath =
                    @"C:\Users\DELL\Desktop\proyecto-propietaria\CuentasPorPagar\Views\Report\Report1.rdlc";
                _reportViewer.LocalReport.Refresh();
                _reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}