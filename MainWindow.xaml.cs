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

namespace SistemaVenta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LogIn_salir(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ingresar_login(object sender, RoutedEventArgs e)
        {
            txtSetPassword.Text = txtPassword.Password;
        }

        private void txtIsOK_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!txtIsOK.Text.Equals("False"))
            {
                SistemaVenta.View.Menu window = new SistemaVenta.View.Menu();
                window.Show();
                this.Close();
            }
        }
    }
}
