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

namespace CuentasPorPagar.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var userName = UserField.Text;
            var password = PasswordField.Password;
            try
            {
                var user = await ParseUser.LogInAsync(userName, password);
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
    
            }
            catch (Exception)
            {
                MessageBox.Show("Usuario o contraseña son erroneos");
            }

        }
    }
}
