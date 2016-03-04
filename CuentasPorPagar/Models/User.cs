using Parse;
namespace CuentasPorPagar.Models
{
    [ParseClassName("User")]
    class User : ParseUser
    {
        [ParseFieldName("username")]
        public new string Username
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }
        [ParseFieldName("Name")]
        public string Name
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }
        [ParseFieldName("password")]
        public new string Password
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }
        [ParseFieldName("Permission")]
        public string Permission
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        [ParseFieldName("email")]
        public new string Email
        {
            get { return GetProperty<string>();  }
            set { SetProperty(value);}
        }
    }
}
