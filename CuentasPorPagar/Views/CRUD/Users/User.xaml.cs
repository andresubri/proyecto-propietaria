using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CuentasPorPagar.Models;
using Parse;

namespace CuentasPorPagar.Views.CRUD
{
    /// <summary>
    ///     Interaction logic for User.xaml
    /// </summary>
    public partial class User : Window
    {
        public User()
        {
            InitializeComponent();
        }

        public async void PopulateGrid()
        {
            var query = await new ParseQuery<Users>().FindAsync();
            var result = from o in query
                select new
                {
                    ID = o.ObjectId,
                    Usuario = o.Username,
                    Correo = o.Email,
                    Fecha = o.CreatedAt
                };
            UserDgv.ItemsSource = result;
        }

        private void CreateUserBtn_Click(object sender, RoutedEventArgs e)
        {
            Crud("create");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!bool.Parse(Application.Current.Properties["IsAdmin"].ToString()))
            { 
                DeleteUserBtn.IsEnabled = false;
                CreateUserBtn.IsEnabled = false;

            }

            PopulateGrid();
            var isAdmin = Convert.ToBoolean(Application.Current.Properties["IsAdmin"]);

        }

        public async void Crud(string option)
        {
            var id = UserDgv.SelectedIndex;
            var query = await new ParseQuery<Users>().FindAsync();
            var result = from o in query
                select o.ObjectId;
            var element = "";


            element = result.ElementAt(id);

            switch (option.ToLower())
            {
                case "create":
                    if (Utilities.ValidatePassword(passwordBox.Password) &&
                        Utilities.ValidateEmail(EmailTxt.Text))
                    {
                        try
                        {
                            var user = new Users
                            {
                                Username = NicknameTxt.Text,
                                Password = passwordBox.Password,
                                Email = EmailTxt.Text,
                                Name = UserNameTxt.Text,
                                Permission = ((ComboBoxItem) PermissionsCbx.SelectedItem).Content.ToString()
                            };
                            await user.SaveAsync();
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
                        var deleteQuery = from o in new ParseQuery<Users>()
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

                case "edit":
                    try
                    {
                        var editQuery = from o in new ParseQuery<Users>()
                            where o.ObjectId.Equals(element)
                            select o;

                        var editElements = await editQuery.FirstAsync();
                        NicknameTxt.Text = editElements.Username;
                        UserNameTxt.Text = editElements.Name;
                        passwordBox.Password = editElements.Password;
                        EmailTxt.Text = editElements.Email;


                        var user = new Users
                        {
                            Username = NicknameTxt.Text,
                            Name = UserNameTxt.Text,
                            Email = EmailTxt.Text,
                            Password = passwordBox.Password
                        };
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

        private void EditUserBtn_Click(object sender, RoutedEventArgs e)
        {
            Crud("edit");
        }
    }
}