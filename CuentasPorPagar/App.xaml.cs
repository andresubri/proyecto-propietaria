using System.Windows;
using CuentasPorPagar.Models;
using Parse;

namespace CuentasPorPagar
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ParseObject.RegisterSubclass<DocumentEntry>();
            ParseObject.RegisterSubclass<Supplier>();
            ParseObject.RegisterSubclass<Payment>();
            ParseObject.RegisterSubclass<Users>();
            ParseClient.Initialize(new ParseClient.Configuration
            {
                ApplicationId = "yMVc5a3J9DSgpGdHDqB2kxKIiO72RVovr4Bxs5Iv",
                WindowsKey = "f1FpJWDQu6tBknQP5uOnr0kA4FMnUdHId1mSP3qM"
            });
        }
    }
}