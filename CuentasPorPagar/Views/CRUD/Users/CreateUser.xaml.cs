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

namespace CuentasPorPagar.Views.CRUD.Users
{
    /// <summary>
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Window
    {
        public CreateUser()
        {
            InitializeComponent();
        }


        

        private void Closebtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void CreateUserBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var user = new Models.User
                {
                    Username = NicknameTxt.Text,
                    Password = passwordBox.Password,
                    Name = UserNameTxt.Text,
                    Permission = ((ComboBoxItem) PermissionsCbx.SelectedItem).Content.ToString(),
                };
                ;
                MessageBox.Show("Usuario creado satisfactoriamente");
                this.Close();


                await user.SignUpAsync();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }
    }
}
