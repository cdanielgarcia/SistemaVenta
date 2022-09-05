using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SistemaVenta.Data;
using SistemaVenta.Model;
using Torbellino;

namespace SistemaVenta.ViewModel
{
    public class VentasVM : INotifyObject
    {
        public RelayCommand cmd_Insertar { get; set; }

        public Venta Venta { get { return venta; } set { venta = value; OnPropertyChanged(); } }
        private Venta venta;
        public DetalleVenta DetalleVenta { get { return detalleVenta; } set { detalleVenta = value; OnPropertyChanged(); } }
        private DetalleVenta detalleVenta;

        public Producto Producto { get { return producto; } set { producto = value; OnPropertyChanged(); } }
        private Producto producto;

        public VentasVM()
        {
            this.cmd_Insertar = new RelayCommand(p => this.Insertar());
            this.Venta = new Venta();
            this.DetalleVenta = new DetalleVenta();
            this.Producto = new Producto();
            this.Venta.FechaRegistro = DateTime.Now;
            this.DetalleVenta.FechaRegistro = this.Venta.FechaRegistro;
            this.Producto.Stock = 0;
        }

        public void Insertar()
        {
            try
            {
                this.Venta.IdVenta = 0;
                this.DetalleVenta.IdDetalleVenta = 0;
                this.DetalleVenta.SubTotal = this.Venta.MontoTotal;
                this.Venta.NumeroDocumento = DateTime.Now.ToString("MMddyyyyHHmmss");
                this.Venta.IdUsuario = 1; // Toca cambiarlo por el usuario que esta registrando la factura.
                this.DetalleVenta.FechaRegistro = this.Venta.FechaRegistro;

                if ((this.Venta.TipoDocumento == null || this.Venta.TipoDocumento == "") ||
                    (this.Venta.NumeroDocumento == null || this.Venta.NumeroDocumento == "") ||
                    (this.Venta.DocumentoCliente == null || this.Venta.DocumentoCliente == "") ||
                    (this.Venta.NombreCompleto == null || this.Venta.NombreCompleto == "") ||
                    (this.Venta.MontoPago == 0) || (this.DetalleVenta.SubTotal == 0) ||
                    (this.Venta.MontoTotal == 0) || (this.DetalleVenta.IdProducto == 0) ||
                    (this.DetalleVenta.PrecioVenta == 0) || (this.DetalleVenta.Cantidad == 0) ||
                    (this.Producto.Stock == 0))
                {
                    MessageBox.Show("Revisé que haya llenado o cambiado los valores de los campos.");
                    return;
                }

                if (this.Venta.MontoPago - this.Venta.MontoCambio > this.Venta.MontoTotal)
                {
                    MessageBox.Show("La suma del 'Pago con' y 'Cambio' dado es mayor al 'Total a Pagar'.");
                    return;
                }

                using (var dbc = new ApplicationDbContext())
                {
                    dbc.Ventas?.Add(this.Venta);
                    dbc.SaveChanges();

                    var getVentaId = (from v in dbc.Ventas
                                      orderby v.FechaRegistro descending
                                      select v.IdVenta).FirstOrDefault();

                    this.DetalleVenta.IdVenta = getVentaId;
                    dbc.DetalleVentas?.Add(this.DetalleVenta);
                    dbc.SaveChanges();

                    var setStockProduct = (from p in dbc.Productos
                                   where p.IdProducto == this.DetalleVenta.IdProducto
                                   select p).FirstOrDefault();

                    setStockProduct.Stock -= this.DetalleVenta.Cantidad;
                    dbc.Productos.Update(setStockProduct);
                    dbc.SaveChanges();

                    MessageBox.Show("Venta registrada exitosamente.");
                }

                this.Venta.IdVenta = 0;
                this.DetalleVenta.IdDetalleVenta = 0;
                this.DetalleVenta.SubTotal = 0;
                this.Venta.NumeroDocumento = "";
                //this.Venta.IdUsuario = 0;
                this.Venta.TipoDocumento = "";
                this.Venta.NumeroDocumento = "";
                this.Venta.DocumentoCliente = "";
                this.Venta.NombreCompleto = "";
                this.Venta.MontoPago = 0;
                this.Venta.MontoCambio = 0;
                this.Venta.MontoTotal = 0;
                this.Venta.FechaRegistro = DateTime.Now;
                this.DetalleVenta.IdProducto = 0;
                this.DetalleVenta.PrecioVenta = 0;
                this.DetalleVenta.Cantidad = 0;
                this.DetalleVenta.FechaRegistro = this.Venta.FechaRegistro;
                this.Producto.Stock = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error VentasVM -> No se pudo registrar la venta | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }
    }
}
