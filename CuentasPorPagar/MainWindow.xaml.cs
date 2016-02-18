using Parse;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CuentasPorPagar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ParseClient.Initialize(new ParseClient.Configuration
            {
                ApplicationId= "yMVc5a3J9DSgpGdHDqB2kxKIiO72RVovr4Bxs5Iv",
                WindowsKey= "f1FpJWDQu6tBknQP5uOnr0kA4FMnUdHId1mSP3qM"
            });
            InitializeComponent();
        }
    }
}
