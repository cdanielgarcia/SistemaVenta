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
        public RelayCommand cmd_Consultar { get; set; }
        public RelayCommand cmd_Borrar { get; set; }
        public RelayCommand cmd_Modifica { get; set; }
        public Producto Producto { get { return producto; } set { producto = value; OnPropertyChanged(); } }

        private Producto producto;


        public ObservableCollection<Producto> ListaProducto { get { return listaproducto; } set { listaproducto = value; OnPropertyChanged(); } }
        private ObservableCollection<Producto> listaproducto = new ObservableCollection<Producto>();

        public ObservableCollection<Categoria> ListaCategoria { get { return listacategoria; } set { listacategoria = value; OnPropertyChanged(); } }
        private ObservableCollection<Categoria> listacategoria = new ObservableCollection<Categoria>();


        public ProductoVm()
        {
            this.cmd_Insertar = new RelayCommand(p => this.Insertar());
            this.cmd_Consultar = new RelayCommand(p => this.Consultar());
            this.cmd_Borrar = new RelayCommand(p => this.Borrar());
            this.cmd_Modifica = new RelayCommand(p => this.Modifica());
            this.Producto = new Producto();
        }

        public void Insertar()
        {
            using (var dbc = new ApplicationDbContext())
            {
                if (this.Producto.Codigo== null || this.Producto.Nombre == null || this.Producto.Descripcion == null)
                {
                    MessageBox.Show("No digitó algunos datos, intente nuevamente");
                    return;
                }
                
                dbc.Productos?.Add(this.Producto);
                try
                {
                    dbc.SaveChanges();
                    this.Consultar();
                }
                catch (Exception er)
                {
                    MessageBox.Show("Error " + er.Message);
                    if (er.InnerException != null)
                        MessageBox.Show("Error " + er.InnerException.Message);
                }
            }
        }

        public void Consultar()
        {
            using (var dbc = new ApplicationDbContext())
            {
                this.ListaProducto = new ObservableCollection<Producto>(dbc.Productos);
                this.ListaCategoria = new ObservableCollection<Categoria>(dbc.Categorias);
                
            }
        }


        public void Borrar()
        {
            if (this.Producto.Nombre == null)
            {
                MessageBox.Show("No digitó el producto a borrar");
                return;
            }

            using (var dbc = new ApplicationDbContext())
            {
                try
                {
                    var borr = (from p in dbc.Productos
                                where p.Nombre == this.Producto.Nombre
                                select p).Single();
                    dbc.Productos?.Remove(borr);
                    dbc.SaveChanges();
                    this.ListaProducto = new ObservableCollection<Producto>(dbc.Productos);
                }
                catch (Exception er)
                {
                    MessageBox.Show("Error " + er.Message);
                    if (er.InnerException != null)
                        MessageBox.Show("Error " + er.InnerException.Message);
                }
            }
        }

        public void Modifica()
        {
            if (this.Producto.Nombre == null)
            {
                MessageBox.Show("No digitó el nombre del producto a modificar");
                return;
            }
            using (var dbc = new ApplicationDbContext())
            {
                var producto = dbc.Productos?.Find(this.Producto.IdProducto);
                producto.IdCategoria = this.Producto.IdCategoria;            
                producto.Codigo = this.Producto.Codigo;
                producto.Nombre = this.Producto.Nombre;
                producto.Descripcion = this.Producto.Descripcion;
                producto.Stock = this.Producto.Stock;
                producto.PrecioCompra = this.Producto.PrecioCompra;
                producto.PrecioVenta = this.Producto.PrecioVenta;
                producto.Estado = this.Producto.Estado;
                producto.FechaRegistro = this.Producto.FechaRegistro;
                try
                {
                    dbc.SaveChanges();
                    this.Consultar();
                }
                catch (Exception er)
                {
                    MessageBox.Show("Error " + er.Message);
                    if (er.InnerException != null)
                        MessageBox.Show("Error " + er.InnerException.Message);
                }
            }
        }

    }
}
