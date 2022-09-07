using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SistemaVenta.Data;
using SistemaVenta.Model;
using Torbellino;

namespace SistemaVenta.ViewModel
{
    public class ProductoVm : INotifyObject
    {
        public RelayCommand cmd_Insertar { get; set; }
        public RelayCommand cmd_Borrar { get; set; }
        public RelayCommand cmd_Modifica { get; set; }
        public Producto Producto { get { return producto; } set { producto = value; OnPropertyChanged(); } }

        private Producto producto;

        public ProductoVm()
        {
            this.cmd_Insertar = new RelayCommand(p => this.Insertar());
            this.cmd_Borrar = new RelayCommand(p => this.Borrar());
            this.cmd_Modifica = new RelayCommand(p => this.Modifica());
            this.Producto = new Producto();
        }

        public void Insertar()
        {
            try
            {
                this.Producto.IdProducto = 0;

                if ((this.Producto.Codigo == null || this.Producto.Codigo == "") ||
                    (this.Producto.Nombre == null || this.Producto.Nombre == "") ||
                    (this.Producto.Descripcion == null || this.Producto.Descripcion == "") ||
                    (this.Producto.IdCategoria == 0))
                {
                    MessageBox.Show("No digitó algunos datos, intente nuevamente");
                    return;
                }

                this.Producto.FechaRegistro = DateTime.Now;

                using (var dbc = new ApplicationDbContext())
                {
                    var existProducto = (from p in dbc.Productos
                                         where p.Codigo == this.Producto.Codigo
                                         select p).FirstOrDefault();

                    if (existProducto == null)
                    {
                        dbc.Productos.Add(this.Producto);
                        dbc.SaveChanges();
                        MessageBox.Show("Producto registrado exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("Ya existe un Producto con ese código.");
                    }
                }

                this.Producto.IdProducto = 0;
                this.Producto.Codigo = "";
                this.Producto.Nombre = "";
                this.Producto.Descripcion = "";
                this.Producto.Estado = false;
                this.Producto.IdProducto = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ProductoVM -> No se pudo registrar el producto | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }

        public void Borrar()
        {
            try
            {
                if ((this.Producto.Codigo == null || this.Producto.Codigo == "") ||
                    (this.Producto.Nombre == null || this.Producto.Nombre == "") ||
                    (this.Producto.Descripcion == null || this.Producto.Descripcion == "") ||
                    (this.Producto.IdCategoria == 0))
                {
                    MessageBox.Show("No digitó algunos datos, intente nuevamente");
                    return;
                }

                using (var dbc = new ApplicationDbContext())
                {
                    var borrar = (from p in dbc.Productos
                                         where p.Codigo == this.Producto.Codigo
                                         select p).FirstOrDefault();

                    if (borrar != null)
                    {
                        dbc.Productos.Remove(borrar);
                        dbc.SaveChanges();
                        MessageBox.Show("Producto borrado exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("El código no existe en Productos.");
                    }
                }

                this.Producto.IdProducto = 0;
                this.Producto.Codigo = "";
                this.Producto.Nombre = "";
                this.Producto.Descripcion = "";
                this.Producto.Estado = false;
                this.Producto.IdProducto = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ProductoVM -> No se pudo borrar el Producto | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }

        public void Modifica()
        {
            try
            {
                if ((this.Producto.Codigo == null || this.Producto.Codigo == "") ||
                    (this.Producto.Nombre == null || this.Producto.Nombre == "") ||
                    (this.Producto.Descripcion == null || this.Producto.Descripcion == "") ||
                    (this.Producto.IdCategoria == 0))
                {
                    MessageBox.Show("No digitó algunos datos, intente nuevamente");
                    return;
                }

                using (var dbc = new ApplicationDbContext())
                {
                    var producto = (from p in dbc.Productos
                                  where p.IdProducto == this.Producto.IdProducto
                                  select p).FirstOrDefault();

                    if (producto != null)
                    {
                        producto.IdProducto = this.Producto.IdProducto;
                        producto.IdCategoria = this.Producto.IdCategoria;
                        producto.Codigo = this.Producto.Codigo;
                        producto.Nombre = this.Producto.Nombre;
                        producto.Descripcion = this.Producto.Descripcion;
                        producto.Stock = 0;
                        producto.PrecioCompra = 0;
                        producto.PrecioVenta = 0;
                        producto.Estado = this.Producto.Estado;
                        dbc.SaveChanges();
                        MessageBox.Show("Producto modificado exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("El producto no existe.");
                    }
                }

                this.Producto.IdProducto = 0;
                this.Producto.Codigo = "";
                this.Producto.Nombre = "";
                this.Producto.Descripcion = "";
                this.Producto.Estado = false;
                this.Producto.IdProducto = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ProductoVM -> No se pudo modificar el producto | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }
    }
}
