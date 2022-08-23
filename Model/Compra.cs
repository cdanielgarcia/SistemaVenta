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
    public class Compra : INotifyObject
    {
        public Compra()
        {
            cDetalleCompra= new ObservableCollection<DetalleCompra>();

        }

        [Key]
        private int idcompra{ get; set; }

        public int IdCompra { get { return idcompra; } set { if (idcompra != value) { idcompra = value; OnPropertyChanged(); } } }

        private int idusuario { get; set; }

        public int IdUsuario { get { return idusuario; } set { if (idusuario != value) { idusuario = value; OnPropertyChanged(); } } }

        private int idproveedor { get; set; }

        public int IdProveedor{ get { return idproveedor; } set { if (idproveedor != value) { idproveedor = value; OnPropertyChanged(); } } }

        [StringLength(60)]
        [Display(Name = ("Tipo documento"))]
        private string? tipodocumento { get; set; }

        public string? TipoDocumento { get { return tipodocumento; } set { if (tipodocumento != value) { tipodocumento = value; OnPropertyChanged(); } } }

        [StringLength(60)]
        [Display(Name = ("número documento"))]
        private string? numerodocumento { get; set; }

        public string? NumeroDocumento { get { return numerodocumento; } set { if (numerodocumento != value) { numerodocumento = value; OnPropertyChanged(); } } }


        [StringLength(60)]
        [Display(Name = ("Nombre completo"))]
        private string? nombrecompleto { get; set; }

        public string? NombreCompleto { get { return nombrecompleto; } set { if (nombrecompleto != value) { nombrecompleto = value; OnPropertyChanged(); } } }

        [Display(Name = ("Monto total"))]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        private decimal montototal { get; set; }

        public decimal MontoTotal { get { return montototal; } set { if (montototal != value) { montototal = value; OnPropertyChanged(); } } }

        [Display(Name = ("Fecha registro"))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        private DateTime fecharegistro;
        public DateTime FechaRegistro { get { return fecharegistro; } set { if (fecharegistro != value) { fecharegistro = value; OnPropertyChanged(); } } }

        private ObservableCollection<DetalleCompra>? cdetallecompra;
        public virtual ObservableCollection<DetalleCompra>? cDetalleCompra { get { return cdetallecompra; } set { cdetallecompra = value; OnPropertyChanged(); } }


    }
}
