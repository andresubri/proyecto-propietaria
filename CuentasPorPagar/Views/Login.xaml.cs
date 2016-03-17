using System;
using System.Windows;
using Parse;

namespace CuentasPorPagar.Views
{
    /// <summary>
    ///     Interaction logic for Login.xaml
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
                var mw = new MainWindow();
                mw.Show();
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Usuario o contraseña son erroneos");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserField.Text = "test";
            PasswordField.Password = "123456";
        }
    }
}