using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SistemaVenta.Data;
using SistemaVenta.Model;
using Torbellino;

namespace SistemaVenta.ViewModel
{
    public class CategoriaVM : INotifyObject
    {
        public RelayCommand cmd_Insertar { get; set; }
        public RelayCommand cmd_Consultar { get; set; }
        public RelayCommand cmd_Borrar { get; set; }
        public RelayCommand cmd_Modifica { get; set; }

        public Categoria Categoria { get { return categoria; } set { categoria = value; OnPropertyChanged(); } }
        private Categoria categoria;

        public ObservableCollection<Categoria> Lista { get { return lista; } set { lista = value; OnPropertyChanged(); } }

        private ObservableCollection<Categoria> lista = new ObservableCollection<Categoria>();

        public CategoriaVM()
        {
            this.cmd_Insertar = new RelayCommand(p => this.Insertar());
            this.cmd_Borrar = new RelayCommand(p => this.Borrar());
            this.cmd_Modifica = new RelayCommand(p => this.Modificar());
            this.Categoria = new Categoria();
        }

        public void Insertar()
        {
            try
            {
                this.Categoria.IdCategoria = 0;

                if (this.Categoria.Descripcion == null || this.Categoria.Descripcion == "")
                {
                    MessageBox.Show("No digito la descripción de la categoría.");
                    return;
                }

                this.Categoria.FechaCreacion = DateTime.Now;

                using (var dbc = new ApplicationDbContext())
                {
                    dbc.Categorias.Add(this.Categoria);
                    dbc.SaveChanges();
                }

                MessageBox.Show("Categoría registrada exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error CategoriaVM -> No se pudo registrar la categoría | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }

        public void Borrar()
        {
            try
            {
                using (var dbc = new ApplicationDbContext())
                {

                    var borrar = (from c in dbc.Categorias
                                where c.Descripcion == this.Categoria.Descripcion
                                select c).Single();

                    dbc.Categorias.Remove(borrar);
                    dbc.SaveChanges();
                }

                MessageBox.Show("Categoría borrada exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error CategoriaVM -> No se pudo borrar la categoría | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }

        public void Modificar()
        {
            try
            {
                if (this.Categoria.Descripcion == null || this.Categoria.Descripcion == "")
                {
                    MessageBox.Show("No digito la descripción de la categoría.");
                    return;
                }

                using (var dbc = new ApplicationDbContext())
                {
                    var categoria = dbc.Categorias?.Find(this.Categoria.IdCategoria);
                    categoria.Descripcion = this.Categoria.Descripcion;
                    categoria.Estado = this.Categoria.Estado;
                    dbc.SaveChanges();
                }

                MessageBox.Show("Categoría modificada exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error CategoriaVM -> No se pudo modificar la categoría | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }
    }
}
