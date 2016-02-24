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
        [ParseFieldName("name")]
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
        [ParseFieldName("permission")]
        public string Permission
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }
    }
}
