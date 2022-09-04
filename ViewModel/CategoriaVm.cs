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
        public RelayCommand cmd_Borrar { get; set; }
        public RelayCommand cmd_Modifica { get; set; }

        public Categoria Categoria { get { return categoria; } set { categoria = value; OnPropertyChanged(); } }
        private Categoria categoria;

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
                    var existCategory = (from c in dbc.Categorias
                                          where c.Descripcion == this.Categoria.Descripcion
                                          select c).FirstOrDefault();

                    if (existCategory == null)
                    {
                        dbc.Categorias.Add(this.Categoria);
                        dbc.SaveChanges();
                        MessageBox.Show("Categoría registrada exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("La categoría ya existe.");
                    }
                }

                this.Categoria.IdCategoria = 0;
                this.Categoria.Descripcion = "";
                this.Categoria.Estado = false;
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
                if (this.Categoria.Descripcion == null || this.Categoria.Descripcion == "")
                {
                    MessageBox.Show("No digito la descripción de la categoría.");
                    return;
                }

                using (var dbc = new ApplicationDbContext())
                {
                    var borrar = (from c in dbc.Categorias
                                where c.Descripcion == this.Categoria.Descripcion
                                select c).FirstOrDefault();

                    if (borrar != null)
                    {
                        dbc.Categorias.Remove(borrar);
                        dbc.SaveChanges();
                        MessageBox.Show("Categoría borrada exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("La categoría no existe.");
                    }
                }

                this.Categoria.IdCategoria = 0;
                this.Categoria.Descripcion = "";
                this.Categoria.Estado = false;
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
                    var categoria = (from c in dbc.Categorias
                                  where c.Descripcion == this.Categoria.Descripcion
                                  select c).FirstOrDefault();

                    if (categoria != null)
                    {
                        categoria.Descripcion = this.Categoria.Descripcion;
                        categoria.Estado = this.Categoria.Estado;
                        dbc.SaveChanges();
                        MessageBox.Show("Categoría modificada exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("La categoría no existe.");
                    }
                }

                this.Categoria.IdCategoria = 0;
                this.Categoria.Descripcion = "";
                this.Categoria.Estado = false;
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
