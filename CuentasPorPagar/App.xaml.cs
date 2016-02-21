using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Parse;
using CuentasPorPagar.Models;
namespace CuentasPorPagar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ParseObject.RegisterSubclass<Supplier>();
            ParseObject.RegisterSubclass<Payment>();
            ParseClient.Initialize(new ParseClient.Configuration
                {
                ApplicationId = "yMVc5a3J9DSgpGdHDqB2kxKIiO72RVovr4Bxs5Iv",
                WindowsKey = "f1FpJWDQu6tBknQP5uOnr0kA4FMnUdHId1mSP3qM"
                 });
        }
        
    }
}
