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
using CuentasPorPagar.Models;
using Parse;

namespace CuentasPorPagar.Views.CRUD
{
    /// <summary>
    /// Interaction logic for Supplier.xaml
    /// </summary>
    public partial class Supplier : Window
    {
        public Supplier()
        {
            
            InitializeComponent();
            var supplier = new Models.Supplier();
            supplier = ParseObject.CreateWithoutData<Models.Supplier>(supplier.Id);
            var query = new ParseQuery<Models.Supplier>();
            IEnumerable<Models.Supplier> result = await query.FindAsync();

        }
    }
}
