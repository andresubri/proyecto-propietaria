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


                ReportDataSource rds = new ReportDataSource();
                rds.Value = list.ToList();
                rds.Name = "DataSet1";
                _reportViewer.LocalReport.DataSources.Clear();
                _reportViewer.LocalReport.DataSources.Add(rds);
                _reportViewer.LocalReport.ReportEmbeddedResource = "Report1.rdlc";
                _reportViewer.LocalReport.ReportPath = @"C:\Users\DELL\Desktop\proyecto-propietaria\CuentasPorPagar\Views\Report\Report1.rdlc";
                _reportViewer.LocalReport.Refresh();
                this._reportViewer.RefreshReport();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

           
        }
    }
}
