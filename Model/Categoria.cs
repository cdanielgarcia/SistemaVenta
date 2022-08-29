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
    public class Categoria : INotifyObject
    {

        public Categoria()
        {
            cProducto = new ObservableCollection<Producto>();
        }
        
        
        private int idcategoria { get; set; }

        [Key]
        public int IdCategoria { get { return idcategoria; } set { if (idcategoria != value) { idcategoria = value; OnPropertyChanged(); } } }

        [StringLength(60)]
        [Display(Name = ("Descripción"))]
        private string? descripcion { get; set; }

        public string? Descripcion { get { return descripcion; } set { if (descripcion != value) { descripcion = value; OnPropertyChanged(); } } }

        /*private bool estado { get; set; }

        public bool Estado { get { return estado; } set { if (estado != value) { estado = value; OnPropertyChanged(); } } }*/


        [Display(Name = ("Fecha creación"))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]

        private DateTime fechacreacion;
        public DateTime FechaCreacion { get { return fechacreacion; } set { if (fechacreacion != value) { fechacreacion = value; OnPropertyChanged(); } } }

        private ObservableCollection<Producto>? cproducto;
        public virtual ObservableCollection<Producto>? cProducto { get { return cproducto; } set { cproducto = value; OnPropertyChanged(); } }
    }
}
