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

namespace SistemaVenta.View
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>    
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btn_Minimizar(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btn_Cerrar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
