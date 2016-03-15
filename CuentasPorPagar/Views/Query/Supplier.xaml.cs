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

namespace CuentasPorPagar.Views.Query
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

        public async void query(string name, 
                                string identification, 
                                string state, 
                                string type 
                                //string balance1, 
                                //string balance2, 
                                //string date1, 
                                //string date2
                                )
        {
            var query = new ParseQuery<Models.Supplier>()
                .WhereContains("name",name)
                .WhereContains("identification",identification)
                .WhereContains("state",state)
                .WhereContains("type",type);
            var result = await query.FindAsync();
            var list = from p in result
                       select new
                       {
                           Id = p.ObjectId,
                           Nombre = p.Name,
                           Identificacion = p.Identification,
                           Estado = p.State,
                           Tipo = p.Type,
                           p.Balance,
                           Creado = p.CreatedAt
                       };
            dataGrid.ItemsSource = list;
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            //Ejecuta la busqueda al presionar el boton
            query(nameTxt.Text,
                  documentTxt.Text,
                  ((ComboBoxItem)stateCmb.SelectedItem).Content.ToString(),
                  ((ComboBoxItem)typeCmb.SelectedItem).Content.ToString()
                  );
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            stateCmb.SelectedIndex = 0;
            typeCmb.SelectedItem = 0;
            //Popula el Datagrid al lanzarse
            var query = new ParseQuery<Models.Supplier>();
            var result = await query.FindAsync();
            var list = from p in result
                       select new
                       {
                           Id = p.ObjectId,
                           Nombre = p.Name,
                           Identificacion = p.Identification,
                           Estado = p.State,
                           Tipo = p.Type,
                           p.Balance,
                           Creado = p.CreatedAt
                       };
            dataGrid.ItemsSource = list;
           
        }
    }
}

