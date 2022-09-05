using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SistemaVenta.Data;
using SistemaVenta.Model;
using Torbellino;

namespace SistemaVenta.ViewModel
{
    public class PermisosVM : INotifyObject
    {
        public RelayCommand cmd_Insertar { get; set; }
        public RelayCommand cmd_Borrar { get; set; }
        public RelayCommand cmd_Modifica { get; set; }

        public Permiso Permiso { get { return permiso; } set { permiso = value; OnPropertyChanged(); } }
        private Permiso permiso;

        public PermisosVM()
        {
            this.cmd_Insertar = new RelayCommand(p => this.Insertar());
            this.cmd_Borrar = new RelayCommand(p => this.Borrar());
            this.cmd_Modifica = new RelayCommand(p => this.Modificar());
            this.Permiso = new Permiso();
        }

        public void Insertar()
        {
            try
            {
                this.Permiso.IdPermiso = 0;

                if ((this.Permiso.NombreMenu == null || this.Permiso.NombreMenu == "") ||
                    (this.Permiso.IdRol == 0))
                {
                    MessageBox.Show("Llené todos los campos primero.");
                    return;
                }

                this.Permiso.FechaCreacion = DateTime.Now;

                using (var dbc = new ApplicationDbContext())
                {
                    var existPermiso = (from p in dbc.Permisos
                                         where p.NombreMenu == this.Permiso.NombreMenu &&
                                         p.IdRol == this.Permiso.IdRol
                                         select p).FirstOrDefault();

                    if (existPermiso == null)
                    {
                        dbc.Permisos.Add(this.Permiso);
                        dbc.SaveChanges();
                        MessageBox.Show("Permiso registrado exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("El permiso ya lo tiene el rol.");
                    }
                }

                this.Permiso.IdPermiso = 0;
                this.Permiso.IdRol = 0;
                this.Permiso.NombreMenu = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error PermisosVM -> No se pudo registrar el permiso | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }

        public void Borrar()
        {
            try
            {
                if ((this.Permiso.NombreMenu == null || this.Permiso.NombreMenu == "") ||
                    (this.Permiso.IdRol == 0))
                {
                    MessageBox.Show("Llené todos los campos primero.");
                    return;
                }

                using (var dbc = new ApplicationDbContext())
                {
                    var borrar = (from p in dbc.Permisos
                                        where p.NombreMenu == this.Permiso.NombreMenu &&
                                        p.IdRol == this.Permiso.IdRol
                                        select p).FirstOrDefault();

                    if (borrar != null)
                    {
                        dbc.Permisos.Remove(borrar);
                        dbc.SaveChanges();
                        MessageBox.Show("Permiso eliminado exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("El permiso no existe.");
                    }
                }

                this.Permiso.IdPermiso = 0;
                this.Permiso.IdRol = 0;
                this.Permiso.NombreMenu = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error PermisosVM -> No se pudo borrar el permiso | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }

        public void Modificar()
        {
            try
            {
                if ((this.Permiso.NombreMenu == null || this.Permiso.NombreMenu == "") ||
                    (this.Permiso.IdRol == 0))
                {
                    MessageBox.Show("Llené todos los campos primero.");
                    return;
                }

                using (var dbc = new ApplicationDbContext())
                {
                    var cliente = (from p in dbc.Permisos
                                  where p.NombreMenu == this.Permiso.NombreMenu &&
                                  p.IdRol == this.Permiso.IdRol
                                  select p).FirstOrDefault();

                    if (cliente != null)
                    {
                        cliente.NombreMenu = this.Permiso.NombreMenu;
                        cliente.IdRol = this.Permiso.IdRol;
                        dbc.SaveChanges();
                        MessageBox.Show("Permiso modificado exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("El permiso no esta relacionado con el rol.");
                        return;
                    }
                }

                this.Permiso.IdPermiso = 0;
                this.Permiso.IdRol = 0;
                this.Permiso.NombreMenu = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error PermisosVM -> No se pudo modificar el permiso | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }
    }
}
