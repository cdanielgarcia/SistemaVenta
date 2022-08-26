using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torbellino;

namespace SistemaVenta.Model
{
    public class DetalleVenta : INotifyObject
    {

        [Key]
        private int iddetalleventa { get; set; }

        public int IdDetalleVenta { get { return iddetalleventa; } set { if (iddetalleventa != value) { iddetalleventa = value; OnPropertyChanged(); } } }

        private int idventa { get; set; }

        public int IdVenta { get { return idventa; } set { if (idventa != value) { idventa = value; OnPropertyChanged(); } } }

        public virtual Venta? Venta { get; set; }
        private int idproducto { get; set; }

        public int IdProducto { get { return idproducto; } set { if (idproducto != value) { idproducto = value; OnPropertyChanged(); } } }

        public virtual Producto? Producto { get; set; }

        [Display(Name = ("Precio venta"))]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        private decimal precioventa { get; set; }

        public decimal PrecioVenta { get { return precioventa; } set { if (precioventa != value) { precioventa = value; OnPropertyChanged(); } } }

        private int cantidad { get; set; }

        public int Cantidad { get { return cantidad; } set { if (cantidad != value) { cantidad = value; OnPropertyChanged(); } } }


        [Display(Name = ("Sub total"))]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        private decimal subtotal { get; set; }

        public decimal SubTotal { get { return subtotal; } set { if (subtotal != value) { subtotal = value; OnPropertyChanged(); } } }

        [Display(Name = ("Fecha registro"))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        private DateTime fecharegistro;
        public DateTime FechaRegistro { get { return fecharegistro; } set { if (fecharegistro != value) { fecharegistro = value; OnPropertyChanged(); } } }
    }
}
