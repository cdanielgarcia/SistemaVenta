using SistemaVenta.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class Compras : Window
    {
        ApplicationDbContext dataEntities = new ApplicationDbContext();

        public Compras()
        {
            InitializeComponent();
            getDataGrid();
        }

        private void Compras_salir(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void getDataGrid()
        {
            var query =
            from compras in dataEntities.Compras
            orderby compras.FechaRegistro descending
            select new {
                compras.IdCompra,
                compras.IdUsuario,
                compras.IdProveedor,
                compras.TipoDocumento,
                compras.NumeroDocumento,
                compras.MontoTotal,
                compras.FechaRegistro
            };

            getData.ItemsSource = query.ToList();
        }

        private void Actualizar_grid(object sender, RoutedEventArgs e)
        {
            getDataGrid();
        }

        private void Buscar_proveedor(object sender, RoutedEventArgs e)
        {
            try
            {
                string numeroDocumento = txtNumeroDocumentoPro.Text;

                var existPro = (from c in dataEntities.Proveedores
                                   where c.NumeroDocumento == numeroDocumento
                                   select c).FirstOrDefault();

                if (existPro != null)
                {
                    txtNombrePro.Text = existPro.RazonSocial;
                    txtIdProveedor.Text = existPro.IdProveedor.ToString();
                }
                else
                {
                    MessageBox.Show("No se encontró el proveedor.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Compras -> Ocurrió un error al obtener el proveedor | " + ex.Message);
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
                                   select new { p.Nombre, p.Stock, p.PrecioVenta }).FirstOrDefault();

                if (existProduct != null)
                {
                    txtNombreProducto.Text = existProduct.Nombre;
                    txtStock.Text = existProduct.Stock.ToString();
                    decimal precioVenta = existProduct.PrecioVenta;
                    txtPrecioVenta.Text = precioVenta.ToString("F", CultureInfo.InvariantCulture);
                }
                else
                {
                    MessageBox.Show("No se encontró el producto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Compras -> Ocurrió un error al obtener el producto | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }

        private void Obtener_total(object sender, RoutedEventArgs e)
        {
            decimal precio = decimal.Parse(txtPrecioCompra.Text, CultureInfo.InvariantCulture);
            int cantidad = Convert.ToInt32(txtCantidad.Text);

            if (precio == 0 || cantidad == 0)
            {
                MessageBox.Show("Revisa que el precio o la cantidad no este en 0.");
                return;
            }

            decimal total = precio * cantidad;
            txtTotalPagar.Text = total.ToString("F", CultureInfo.InvariantCulture);
        }

        private void volver_Menu_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }
    }
}
