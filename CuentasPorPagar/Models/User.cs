using Parse;
namespace CuentasPorPagar.Models
{
    [ParseClassName("User")]
    class User : ParseObject
    {
        [ParseFieldName("username")]
        public ParseUser Username
        {
            get { return GetProperty<ParseUser>(); }
            set { SetProperty(value); }
        }
        [ParseFieldName("name")]
        public string Name
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
