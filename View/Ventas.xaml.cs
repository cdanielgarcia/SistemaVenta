using SistemaVenta.Data;
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
    /// Lógica de interacción para Categoria.xaml
    /// </summary>
    public partial class Ventas : Window
    {
        ApplicationDbContext dataEntities = new ApplicationDbContext();

        public Ventas()
        {
            InitializeComponent();
            getDataGrid();
        }

        private void Ventas_salir(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void getDataGrid()
        {
            var query =
            from venta in dataEntities.Ventas
            orderby venta.FechaRegistro descending
            select new { 
                venta.IdVenta, venta.IdUsuario, venta.TipoDocumento, venta.NumeroDocumento,
                venta.DocumentoCliente, venta.NombreCompleto, venta.MontoPago, venta.MontoCambio,
                venta.MontoTotal, venta.FechaRegistro
            };

            getData.ItemsSource = query.ToList();
        }

        private void Actualizar_grid(object sender, RoutedEventArgs e)
        {
            getDataGrid();
        }

        private void Buscar_cliente(object sender, RoutedEventArgs e)
        {
            try
            {
                string numeroDocumento = txtNumeroDocumento.Text;

                var existClient = (from c in dataEntities.Clientes
                                   where c.NumeroDocumento == numeroDocumento
                                   select c).FirstOrDefault();

                if (existClient != null)
                {
                    txtNombreCompleto.Text = existClient.NombreCompleto;
                }
                else
                {
                    MessageBox.Show("No se encontró el cliente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Ventas -> Ocurrió un error al obtener el cliente | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }

        private void Buscar_producto(object sender, RoutedEventArgs e)
        {
            try
            {
                int codigoProducto = Convert.ToInt32(txtIdProducto.Text);

                var existProduct = (from p in dataEntities.Productos
                                   where p.IdProducto == codigoProducto
                                   select p.Nombre).FirstOrDefault();

                if (existProduct != null)
                {
                    txtNombreProducto.Text = existProduct;
                }
                else
                {
                    MessageBox.Show("No se encontró el producto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Ventas -> Ocurrió un error al obtener el producto | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }
    }
}
