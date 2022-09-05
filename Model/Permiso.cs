using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torbellino;

namespace SistemaVenta.Model
{
    public class Permiso : INotifyObject
    {
        private int idpermiso{ get; set; }

        [Key]
        public int IdPermiso{ get { return idpermiso; } set { if (idpermiso != value) { idpermiso = value; OnPropertyChanged(); } } }

        public int IdRol { get; set; }
        [ForeignKey("IdRol")]
        public virtual Rol Rol { get; set; }

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
