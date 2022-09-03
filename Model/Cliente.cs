using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torbellino;

namespace SistemaVenta.Model
{
    public class Cliente : INotifyObject
    {
        
        private int idcliente { get; set; }

        [Key]
        public int IdCliente { get { return idcliente; } set { if (idcliente != value) { idcliente = value; OnPropertyChanged(); } } }

        [StringLength(60)]
        [Display(Name = ("número documento"))]
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
        [Display(Name = ("Teléfono"))]
        private string? telefono { get; set; }
        public string? Telefono { get { return telefono; } set { if (telefono != value) { telefono = value; OnPropertyChanged(); } } }

        private bool estado { get; set; }

        public bool Estado { get { return estado; } set { if (estado != value) { estado = value; OnPropertyChanged(); } } }


        [Display(Name = ("Fecha creación"))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        private DateTime fechacreacion;
        public DateTime FechaCreacion { get { return fechacreacion; } set { if (fechacreacion != value) { fechacreacion = value; OnPropertyChanged(); } } }

    }
}
