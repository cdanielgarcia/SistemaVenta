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
    public class Rol : INotifyObject
    {
        public Rol()
        {
            cPermiso = new ObservableCollection<Permiso>();
            cUsuario = new ObservableCollection<Usuario>();
        }

        

        
        private int idrol { get; set; }

        [Key]
        public int IdRol { get { return idrol; } set { if (idrol != value) { idrol = value; OnPropertyChanged(); } } }

        [StringLength(60)]
        [Display(Name = ("Descripción"))]
        private string? descripcion { get; set; }

        public string? Descripcion { get { return descripcion; } set { if (descripcion != value) { descripcion = value; OnPropertyChanged(); } } }

        [Display(Name = ("Fecha creación"))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]

        private DateTime fechacreacion;
        public DateTime FechaCreacion { get { return fechacreacion; } set { if (fechacreacion != value) { fechacreacion = value; OnPropertyChanged(); } } }

        private ObservableCollection<Permiso>? cpermiso;
        public virtual ObservableCollection<Permiso>? cPermiso { get { return cpermiso; } set { cpermiso = value; OnPropertyChanged(); } }

        private ObservableCollection<Usuario>? cusuario;
        public virtual ObservableCollection<Usuario>? cUsuario { get { return cusuario; } set { cusuario = value; OnPropertyChanged(); } }






    }

   
}
