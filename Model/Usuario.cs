using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torbellino;

namespace SistemaVenta.Model
{
    public class Usuario : INotifyObject
    {
        public Usuario()
        {
            cCompra = new ObservableCollection<Compra>();
        }

        
        private int idusuario { get; set; }

        [Key]
        public int IdUsuario { get { return idusuario; } set { if (idusuario != value) { idusuario = value; OnPropertyChanged(); } } }

        public int IdRol { get; set; }
        [ForeignKey("IdRol")]
        public virtual Rol Rol { get; set; }

        [StringLength(60)]
        [Display(Name = ("Número documento"))]
        private string? numerodocumento { get; set; }

        public string? NumeroDocumento { get { return numerodocumento; } set { if (numerodocumento != value) { numerodocumento = value; OnPropertyChanged(); } } }


        [StringLength(60)]
        [Display(Name = ("Nombre completo"))]

        private string? nombrecompleto { get; set; }

        public string? NombreCompleto { get { return nombrecompleto; } set { if (nombrecompleto != value) { nombrecompleto = value; OnPropertyChanged(); } } }

        [StringLength(60)]
        private string? correo { get; set; }

        public string? Correo { get { return correo; } set { if (correo != value) { correo = value; OnPropertyChanged(); } } }


        [StringLength(60)]
        private string? clave { get; set; }
        public string? Clave { get { return clave; } set { if (clave != value) { clave = value; OnPropertyChanged(); } } }

        private bool estado { get; set; }

        public bool Estado { get { return estado; } set { if (estado != value) { estado = value; OnPropertyChanged(); } } }


        [Display(Name = ("Fecha registro"))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        private DateTime fecharegistro;
        public DateTime FechaRegistro { get { return fecharegistro; } set { if (fecharegistro != value) { fecharegistro = value; OnPropertyChanged(); } } }

        private ObservableCollection<Compra>? ccompra;
        public virtual ObservableCollection<Compra>? cCompra { get { return ccompra; } set { ccompra = value; OnPropertyChanged(); } }

        private ObservableCollection<Venta>? vventa;
        public virtual ObservableCollection<Venta>? vVenta { get { return vventa; } set { vventa = value; OnPropertyChanged(); } }
    }
}
