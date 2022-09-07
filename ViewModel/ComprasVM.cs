using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SistemaVenta.Data;
using SistemaVenta.Model;
using Torbellino;

namespace SistemaVenta.ViewModel
{
    public class ComprasVM : INotifyObject
    {
        public RelayCommand cmd_Insertar { get; set; }

        public Compra Compra { get { return compra; } set { compra = value; OnPropertyChanged(); } }
        private Compra compra;
        public DetalleCompra DetalleCompra { get { return detalleCompra; } set { detalleCompra = value; OnPropertyChanged(); } }
        private DetalleCompra detalleCompra;

        public Producto Producto { get { return producto; } set { producto = value; OnPropertyChanged(); } }
        private Producto producto;

        public ComprasVM()
        {
            this.cmd_Insertar = new RelayCommand(p => this.Insertar());
            this.Compra = new Compra();
            this.DetalleCompra = new DetalleCompra();
            this.Producto = new Producto();
            this.Compra.FechaRegistro = DateTime.Now;
            this.DetalleCompra.FechaRegistro = this.Compra.FechaRegistro;
            this.Producto.Stock = 0;
        }

        public void Insertar()
        {
            try
            {
                this.Compra.IdCompra = 0;
                this.DetalleCompra.IdDetalleCompra = 0;
                this.DetalleCompra.FechaRegistro = this.Compra.FechaRegistro;
                this.Compra.NumeroDocumento = DateTime.Now.ToString("MMddyyyyHHmmss");
                this.DetalleCompra.MontoTotal = this.Compra.MontoTotal;
                this.Compra.IdUsuario = 1;

                if ((this.Compra.TipoDocumento == null || this.Compra.TipoDocumento == "") ||
                    (this.Compra.NumeroDocumento == null || this.Compra.NumeroDocumento == "") ||
                    (this.DetalleCompra.MontoTotal == 0) || (this.Compra.MontoTotal == 0) ||
                    (this.DetalleCompra.IdProducto == 0) || (this.DetalleCompra.PrecioCompra == 0) ||
                    (this.DetalleCompra.PrecioVenta == 0) || (this.DetalleCompra.Cantidad == 0))
                {
                    MessageBox.Show("Revisé que haya llenado o cambiado los valores de los campos.");
                    return;
                }

                using (var dbc = new ApplicationDbContext())
                {
                    dbc.Compras?.Add(this.Compra);
                    dbc.SaveChanges();

                    var getCompraId = (from c in dbc.Compras
                                      orderby c.FechaRegistro descending
                                      select c.IdCompra).FirstOrDefault();

                    this.DetalleCompra.IdCompra = getCompraId;
                    dbc.DetalleCompras?.Add(this.DetalleCompra);
                    dbc.SaveChanges();

                    var setProduct = (from p in dbc.Productos
                                    where p.IdProducto == this.DetalleCompra.IdProducto
                                    select p).FirstOrDefault();

                    setProduct.Stock += this.DetalleCompra.Cantidad;
                    setProduct.PrecioCompra = this.DetalleCompra.PrecioCompra;
                    setProduct.PrecioVenta = this.DetalleCompra.PrecioVenta;
                    dbc.Productos.Update(setProduct);
                    dbc.SaveChanges();

                    MessageBox.Show("Compra registrada exitosamente.");
                }

                this.Compra.IdCompra = 0;
                this.DetalleCompra.IdDetalleCompra = 0;
                this.DetalleCompra.MontoTotal = 0;
                this.Compra.NumeroDocumento = "";
                //this.Venta.IdUsuario = 0;
                this.Compra.TipoDocumento = "";
                this.Compra.NumeroDocumento = "";
                this.DetalleCompra.PrecioVenta = 0;
                this.DetalleCompra.PrecioCompra = 0;
                this.Compra.MontoTotal = 0;
                this.Compra.FechaRegistro = DateTime.Now;
                this.DetalleCompra.IdProducto = 0;
                this.DetalleCompra.Cantidad = 0;
                this.DetalleCompra.FechaRegistro = this.Compra.FechaRegistro;
                this.Producto.Stock = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ComprasVM -> No se pudo registrar la compra | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }
    }
}
