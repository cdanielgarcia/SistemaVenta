using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torbellino;

namespace SistemaVenta.Model
{
    public class Producto : INotifyObject
    {
        public Producto()
        {
            cDetalleCompra = new ObservableCollection<DetalleCompra>();
            cDetalleVenta = new ObservableCollection<DetalleVenta>();
        }
        
        private int idproducto { get; set; }

        [Key]
        public int IdProducto { get { return idproducto; } set { if (idproducto != value) { idproducto = value; OnPropertyChanged(); } } }

        private int idcategoria { get; set; }

        public int IdCategoria { get { return idcategoria; } set { if (idcategoria != value) { idcategoria = value; OnPropertyChanged(); } } }

        public virtual Categoria? Categoria { get; set; }

        [StringLength(60)]
        [Display(Name = ("Código"))]
        private string? codigo { get; set; }

        public string? Codigo { get { return codigo; } set { if (codigo != value) { codigo = value; OnPropertyChanged(); } } }

        [StringLength(60)]
        private string? nombre { get; set; }

        public string? Nombre { get { return nombre; } set { if (nombre != value) { nombre = value; OnPropertyChanged(); } } }

        [StringLength(60)]
        [Display(Name = ("Descripción"))]
        private string? descripcion { get; set; }

        public string? Descripcion { get { return descripcion; } set { if (descripcion != value) { descripcion = value; OnPropertyChanged(); } } }

        
        private int stock { get; set; }

        public int Stock { get { return stock; } set { if (stock != value) { stock = value; OnPropertyChanged(); } } }

        [Display(Name = ("Precio compra"))]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        private decimal preciocompra { get; set; }

        public decimal PrecioCompra { get { return preciocompra; } set { if (preciocompra != value) { preciocompra = value; OnPropertyChanged(); } } }


        [Display(Name = ("Precio venta"))]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        private decimal precioventa { get; set; }

        public decimal PrecioVenta { get { return precioventa; } set { if (precioventa != value) { precioventa = value; OnPropertyChanged(); } } }

        /*private bool estado { get; set; }

        public bool Estado { get { return estado; } set { if (estado != value) { estado = value; OnPropertyChanged(); } } }*/

        [Display(Name = ("Fecha registro"))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        private DateTime fecharegistro;
        public DateTime FechaRegistro { get { return fecharegistro; } set { if (fecharegistro != value) { fecharegistro = value; OnPropertyChanged(); } } }

        private ObservableCollection<DetalleCompra>? cdetallecompra;
        public virtual ObservableCollection<DetalleCompra>? cDetalleCompra { get { return cdetallecompra; } set { cdetallecompra = value; OnPropertyChanged(); } }

        private ObservableCollection<DetalleVenta>? cdetalleventa;
        public virtual ObservableCollection<DetalleVenta>? cDetalleVenta { get { return cdetalleventa; } set { cdetalleventa = value; OnPropertyChanged(); } }
    }

}
