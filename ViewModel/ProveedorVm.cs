using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SistemaVenta.Data;
using SistemaVenta.Model;
using Torbellino;

namespace SistemaVenta.ViewModel
{
    public class ProveedorVM : INotifyObject
    {
        public RelayCommand cmd_Insertar { get; set; }
        public RelayCommand cmd_Borrar { get; set; }
        public RelayCommand cmd_Modifica { get; set; }

        public Proveedor Proveedor { get { return proveedor; } set { proveedor = value; OnPropertyChanged(); } }
        private Proveedor proveedor;

        public ProveedorVM()
        {
            this.cmd_Insertar = new RelayCommand(p => this.Insertar());
            this.cmd_Borrar = new RelayCommand(p => this.Borrar());
            this.cmd_Modifica = new RelayCommand(p => this.Modificar());
            this.Proveedor = new Proveedor();
        }

        public void Insertar()
        {
            try
            {
                this.Proveedor.IdProveedor = 0;

                if ((this.Proveedor.NumeroDocumento == null || this.Proveedor.NumeroDocumento == "") ||
                    (this.Proveedor.RazonSocial == null || this.Proveedor.RazonSocial == "") ||
                    (this.Proveedor.Correo == null || this.Proveedor.Correo == "") ||
                    (this.Proveedor.Telefono == null || this.Proveedor.Telefono == ""))
                {
                    MessageBox.Show("Llené todos los campos primero.");
                    return;
                }

                this.Proveedor.FechaCreacion = DateTime.Now;

                using (var dbc = new ApplicationDbContext())
                {
                    var existPro = (from p in dbc.Proveedores
                                       where p.NumeroDocumento == this.Proveedor.NumeroDocumento
                                       select p).FirstOrDefault();

                    if (existPro == null)
                    {
                        dbc.Proveedores.Add(this.Proveedor);
                        dbc.SaveChanges();
                        MessageBox.Show("Proveedor registrado exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("El proveedor ya existe.");
                    }
                }

                this.Proveedor.IdProveedor = 0;
                this.Proveedor.NumeroDocumento = "";
                this.Proveedor.RazonSocial = "";
                this.Proveedor.Correo = "";
                this.Proveedor.Telefono = "";
                this.Proveedor.Estado = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ProveedorVM -> No se pudo registrar el proveedor | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }

        public void Borrar()
        {
            try
            {
                if (this.Proveedor.NumeroDocumento == null || this.Proveedor.NumeroDocumento == "")
                {
                    MessageBox.Show("El número de documento no puede ser vacío.");
                    return;
                }

                using (var dbc = new ApplicationDbContext())
                {

                    var borrar = (from p in dbc.Proveedores
                                  where p.NumeroDocumento == this.Proveedor.NumeroDocumento
                                  select p).FirstOrDefault();

                    if (borrar != null)
                    {
                        dbc.Proveedores.Remove(borrar);
                        dbc.SaveChanges();
                        MessageBox.Show("Proveedor eliminado exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("El proveedor no existe.");
                    }
                }

                this.Proveedor.IdProveedor = 0;
                this.Proveedor.NumeroDocumento = "";
                this.Proveedor.RazonSocial = "";
                this.Proveedor.Correo = "";
                this.Proveedor.Telefono = "";
                this.Proveedor.Estado = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ProveedorVM -> No se pudo borrar el proveedor | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }

        public void Modificar()
        {
            try
            {
                if ((this.Proveedor.NumeroDocumento == null || this.Proveedor.NumeroDocumento == "") ||
                    (this.Proveedor.RazonSocial == null || this.Proveedor.RazonSocial == "") ||
                    (this.Proveedor.Correo == null || this.Proveedor.Correo == "") ||
                    (this.Proveedor.Telefono == null || this.Proveedor.Telefono == ""))
                {
                    MessageBox.Show("No pueden ir campos vacíos.");
                    return;
                }

                using (var dbc = new ApplicationDbContext())
                {
                    var proveedor = (from p in dbc.Proveedores
                                   where p.IdProveedor == this.Proveedor.IdProveedor
                                   select p).FirstOrDefault();

                    if (proveedor != null)
                    {
                        proveedor.NumeroDocumento = this.Proveedor.NumeroDocumento;
                        proveedor.RazonSocial = this.Proveedor.RazonSocial;
                        proveedor.Correo = this.Proveedor.Correo;
                        proveedor.Telefono = this.Proveedor.Telefono;
                        proveedor.Estado = this.Proveedor.Estado;
                        dbc.SaveChanges();
                        MessageBox.Show("Proveedor modificado exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("El número de documento no existe.");
                        return;
                    }
                }

                this.Proveedor.IdProveedor = 0;
                this.Proveedor.NumeroDocumento = "";
                this.Proveedor.RazonSocial = "";
                this.Proveedor.Correo = "";
                this.Proveedor.Telefono = "";
                this.Proveedor.Estado = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ProveedorVM -> No se pudo modificar el proveedor | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }
    }
}
