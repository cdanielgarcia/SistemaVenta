using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torbellino;

namespace SistemaVenta.Model
{
    public class Permiso : INotifyObject
    {
      

        [Key]
        private int idpermiso{ get; set; }

        public int IdPermiso{ get { return idpermiso; } set { if (idpermiso != value) { idpermiso = value; OnPropertyChanged(); } } }

        private int idrol { get; set; }

        public int IdRol { get { return idrol; } set { if (idrol != value) { idrol = value; OnPropertyChanged(); } } }

        public virtual Rol? Rol { get; set; }

        [StringLength(60)]
        [Display(Name = ("Nombre menú"))]
        private string? nombremenu { get; set; }

        public string? NombreMenu { get { return nombremenu; } set { if (nombremenu != value) { nombremenu = value; OnPropertyChanged(); } } }

        [Display(Name = ("Fecha creación"))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]

        private DateTime fechacreacion;
        public DateTime FechaCreacion { get { return fechacreacion; } set { if (fechacreacion != value) { fechacreacion = value; OnPropertyChanged(); } } }
    }
}
