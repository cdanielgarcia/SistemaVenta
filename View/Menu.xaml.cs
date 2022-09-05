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

        private void Btncategoria_Click(object sender, RoutedEventArgs e)
        {
            Categoria categoria = new Categoria();
            categoria.Show();
            this.Close();
        }

        private void Btncliente_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente();
            cliente.Show();
            this.Close();
        }

        private void Btnventa_Click(object sender, RoutedEventArgs e)
        {
            Ventas venta = new Ventas();
            venta.Show();
            this.Close();
        }

        private void Btndetalleventa_Click(object sender, RoutedEventArgs e)
        {
            DetalleVentas detalleVentas = new DetalleVentas();
            detalleVentas.Show();
            this.Close();
        }

        private void Btnproveedores_Click(object sender, RoutedEventArgs e)
        {
            Proveedor proveedor = new Proveedor();
            proveedor.Show();
            this.Close();
        }

        private void Btnroles_Click(object sender, RoutedEventArgs e)
        {
            Rol rol = new Rol();
            rol.Show();
            this.Close();
        }

        private void Btnpermisos_Click(object sender, RoutedEventArgs e)
        {
            Permisos permisos = new Permisos();
            permisos.Show();
            this.Close();
        }
    }
}
