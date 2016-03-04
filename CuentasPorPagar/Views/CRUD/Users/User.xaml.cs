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

namespace CuentasPorPagar.Views.CRUD
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Window
    {
        public User()
        {
            InitializeComponent();
        }

        public async void PopulateGrid()
        {
            var query = await new ParseQuery<ParseUser>().FindAsync();
            var result = from o in query
                select new
                {
                    ID = o.ObjectId,
                    Usuario = o.Username,
                    Correo = o.Email,
                    Fecha = o.CreatedAt,
                };
            UserDgv.ItemsSource = result;
        }
        private void CreateUserBtn_Click(object sender, RoutedEventArgs e)
        {
            Crud("create");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateGrid();

        }

        public async void Crud(string option)
        {
            var id = UserDgv.SelectedIndex;
            var query = await new ParseQuery<ParseUser>().FindAsync();
            var result = from o in query
                select o.ObjectId;
            var element = "";
            
            if (id > 0)
                element = result.ElementAt(id);

            switch (option.ToLower())
            {
                case "create":
                    if (Utilities.ValidatePassword(passwordBox.Password) &&
                        Utilities.ValidateEmail(EmailTxt.Text))
                    {
                        try
                        {
                            var user = new Models.User
                            {
                                Username = NicknameTxt.Text,
                                Password = passwordBox.Password,
                                Name = UserNameTxt.Text,
                                Email = EmailTxt.Text,
                                Permission = ((ComboBoxItem) PermissionsCbx.SelectedItem).Content.ToString()
                            };
                            await user.SignUpAsync();
                            MessageBox.Show("Usuario creado satisfactoriamente");
                            PopulateGrid();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    else
                        MessageBox.Show("El campo Email o contraseña no son validos");

                    break;

                case "delete":
                    try
                    {
                        var deleteQuery = from o in new ParseQuery<Models.User>()
                            where o.ObjectId.Equals(element)
                            select o;

                        await deleteQuery.FirstAsync().Result.DeleteAsync();
                        PopulateGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;

                default:
                    MessageBox.Show("No cases were assigned");
                    break;
            }


        }

        private void DeleteUserBtn_Click(object sender, RoutedEventArgs e)
        {
            Crud("delete");
        }
    }
}
