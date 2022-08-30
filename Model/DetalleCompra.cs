using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torbellino;

namespace SistemaVenta.Model
{
    public class DetalleCompra : INotifyObject
    {
        
        private int iddetallecompra { get; set; }

        [Key]
        public int IdDetalleCompra { get { return iddetallecompra; } set { if (iddetallecompra != value) { iddetallecompra = value; OnPropertyChanged(); } } }

        private int idcompra { get; set; }

        [ForeignKey("IdCompra")]
        public int IdCompra { get { return idcompra; } set { if (idcompra != value) { idcompra = value; OnPropertyChanged(); } } }

        public virtual Compra? Compra { get; set; }
        private int idproducto { get; set; }

        [ForeignKey("IdProducto")]
        public int IdProducto { get { return idproducto; } set { if (idproducto != value) { idproducto = value; OnPropertyChanged(); } } }

        public virtual Producto? Producto { get; set; }

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

        private int cantidad { get; set; }

        public int Cantidad { get { return cantidad; } set { if (cantidad != value) { cantidad = value; OnPropertyChanged(); } } }


        [Display(Name = ("Monto total"))]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        private decimal montototal { get; set; }

        public decimal MontoTotal { get { return montototal; } set { if (montototal != value) { montototal = value; OnPropertyChanged(); } } }

        [Display(Name = ("Fecha registro"))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        private DateTime fecharegistro;
        public DateTime FechaRegistro { get { return fecharegistro; } set { if (fecharegistro != value) { fecharegistro = value; OnPropertyChanged(); } } }
    }
}
