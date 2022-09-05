using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SistemaVenta.Data;
using SistemaVenta.Model;
using Torbellino;

namespace SistemaVenta.ViewModel
{
    public class RolVM : INotifyObject
    {
        public RelayCommand cmd_Insertar { get; set; }
        public RelayCommand cmd_Borrar { get; set; }
        public RelayCommand cmd_Modifica { get; set; }

        public Rol Rol { get { return rol; } set { rol = value; OnPropertyChanged(); } }
        private Rol rol;

        public RolVM()
        {
            this.cmd_Insertar = new RelayCommand(p => this.Insertar());
            this.cmd_Borrar = new RelayCommand(p => this.Borrar());
            this.cmd_Modifica = new RelayCommand(p => this.Modificar());
            this.Rol = new Rol();
        }

        public void Insertar()
        {
            try
            {
                this.Rol.IdRol = 0;

                if (this.Rol.Descripcion == null || this.Rol.Descripcion == "")
                {
                    MessageBox.Show("No digito la descripción del Rol.");
                    return;
                }

                this.Rol.FechaCreacion = DateTime.Now;

                using (var dbc = new ApplicationDbContext())
                {
                    var existRol = (from r in dbc.Roles
                                          where r.Descripcion == this.Rol.Descripcion
                                          select r).FirstOrDefault();

                    if (existRol == null)
                    {
                        dbc.Roles.Add(this.Rol);
                        dbc.SaveChanges();
                        MessageBox.Show("Rol registrado exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("El rol ya existe.");
                    }
                }

                this.Rol.IdRol = 0;
                this.Rol.Descripcion = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error RolVM -> No se pudo registrar el rol | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }

        public void Borrar()
        {
            try
            {
                if (this.Rol.Descripcion == null || this.Rol.Descripcion == "")
                {
                    MessageBox.Show("No digito la descripción del Rol.");
                    return;
                }

                using (var dbc = new ApplicationDbContext())
                {
                    var borrar = (from r in dbc.Roles
                                    where r.Descripcion == this.Rol.Descripcion
                                    select r).FirstOrDefault();

                    if (borrar != null)
                    {
                        dbc.Roles.Remove(borrar);
                        dbc.SaveChanges();
                        MessageBox.Show("Rol borrado exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("El rol no existe.");
                    }
                }

                this.Rol.IdRol = 0;
                this.Rol.Descripcion = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error RolVM -> No se pudo borrar el rol | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }

        public void Modificar()
        {
            try
            {
                if (this.Rol.Descripcion == null || this.Rol.Descripcion == "")
                {
                    MessageBox.Show("No digito la descripción del Rol.");
                    return;
                }

                using (var dbc = new ApplicationDbContext())
                {
                    var rol = (from r in dbc.Roles
                                  where r.IdRol == this.Rol.IdRol
                                  select r).FirstOrDefault();

                    if (rol != null)
                    {
                        rol.Descripcion = this.Rol.Descripcion;
                        dbc.SaveChanges();
                        MessageBox.Show("Rol modificado exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("El rol no existe.");
                    }
                }

                this.Rol.IdRol = 0;
                this.Rol.Descripcion = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error RolVM -> No se pudo modificar el rol | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }
    }
}
