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
    public class Venta : INotifyObject
    {
        public Venta()
        {
            cDetalleVenta= new ObservableCollection<DetalleVenta>();
            
        }
        
        private int idventa{ get; set; }

        [Key]
        public int IdVenta { get { return idventa; } set { if (idventa != value) { idventa = value; OnPropertyChanged(); } } }

        private int idusuario { get; set; }

        public int IdUsuario { get { return idusuario; } set { if (idusuario != value) { idusuario = value; OnPropertyChanged(); } } }

        public virtual Usuario? Usuario { get; set; }

        [StringLength(60)]
        [Display(Name = ("Tipo documento"))]
        private string? tipodocumento { get; set; }

        public string? TipoDocumento { get { return tipodocumento; } set { if (tipodocumento != value) { tipodocumento = value; OnPropertyChanged(); } } }

        [StringLength(60)]
        [Display(Name = ("número documento"))]
        private string? numerodocumento { get; set; }

        public string? NumeroDocumento { get { return numerodocumento; } set { if (numerodocumento != value) { numerodocumento = value; OnPropertyChanged(); } } }

        [StringLength(60)]
        [Display(Name = ("Documento cliente"))]
        private string? documentocliente { get; set; }

        public string? DocumentoCliente { get { return documentocliente; } set { if (documentocliente != value) { documentocliente = value; OnPropertyChanged(); } } }

        [StringLength(60)]
        [Display(Name = ("Nombre completo"))]
        private string? nombrecompleto { get; set; }

        public string? NombreCompleto { get { return nombrecompleto; } set { if (nombrecompleto != value) { nombrecompleto = value; OnPropertyChanged(); } } }


        [Display(Name = ("Monto pago"))]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        private decimal montopago { get; set; }

        public decimal MontoPago{ get { return montopago; } set { if (montopago != value) { montopago = value; OnPropertyChanged(); } } }


        [Display(Name = ("Monto cambio"))]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        private decimal montocambio { get; set; }

        public decimal MontoCambio { get { return montocambio; } set { if (montocambio != value) { montocambio = value; OnPropertyChanged(); } } }

        [Display(Name = ("Monto total"))]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        private decimal montoTotal { get; set; }

        public decimal MontoTotal { get { return montoTotal; } set { if (montoTotal != value) { montoTotal = value; OnPropertyChanged(); } } }

        [Display(Name = ("Fecha registro"))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        private DateTime fecharegistro;
        public DateTime FechaRegistro { get { return fecharegistro; } set { if (fecharegistro != value) { fecharegistro = value; OnPropertyChanged(); } } }

        private ObservableCollection<DetalleVenta>? cdetalleventa;
        public virtual ObservableCollection<DetalleVenta>? cDetalleVenta { get { return cdetalleventa; } set { cdetalleventa = value; OnPropertyChanged(); } }
    }
}
