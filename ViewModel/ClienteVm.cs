using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SistemaVenta.Data;
using SistemaVenta.Model;
using Torbellino;

namespace SistemaVenta.ViewModel
{
    public class ClienteVM : INotifyObject
    {
        public RelayCommand cmd_Insertar { get; set; }
        public RelayCommand cmd_Borrar { get; set; }
        public RelayCommand cmd_Modifica { get; set; }

        public Cliente Cliente { get { return cliente; } set { cliente = value; OnPropertyChanged(); } }
        private Cliente cliente;

        public ClienteVM()
        {
            this.cmd_Insertar = new RelayCommand(p => this.Insertar());
            this.cmd_Borrar = new RelayCommand(p => this.Borrar());
            this.cmd_Modifica = new RelayCommand(p => this.Modificar());
            this.Cliente = new Cliente();
        }

        public void Insertar()
        {
            try
            {
                this.Cliente.IdCliente = 0;

                if ((this.Cliente.NumeroDocumento == null || this.Cliente.NumeroDocumento == "") ||
                    (this.Cliente.NombreCompleto == null || this.Cliente.NombreCompleto == "") ||
                    (this.Cliente.Correo == null || this.Cliente.Correo == "") ||
                    (this.Cliente.Telefono == null || this.Cliente.Telefono == ""))
                {
                    MessageBox.Show("Llené todos los campos primero.");
                    return;
                }

                this.Cliente.FechaCreacion = DateTime.Now;

                using (var dbc = new ApplicationDbContext())
                {
                    var existClient = (from c in dbc.Clientes
                                         where c.NumeroDocumento == this.Cliente.NumeroDocumento
                                         select c).FirstOrDefault();

                    if (existClient == null)
                    {
                        dbc.Clientes.Add(this.Cliente);
                        dbc.SaveChanges();
                        MessageBox.Show("Cliente registrado exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("El cliente ya existe.");
                    }
                }

                this.Cliente.IdCliente = 0;
                this.Cliente.NumeroDocumento = "";
                this.Cliente.NombreCompleto = "";
                this.Cliente.Correo = "";
                this.Cliente.Telefono = "";
                this.Cliente.Estado = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ClienteVM -> No se pudo registrar el cliente | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }

        public void Borrar()
        {
            try
            {
                if (this.Cliente.NumeroDocumento == null || this.Cliente.NumeroDocumento == "")
                {
                    MessageBox.Show("El número de documento no puede ser vacío.");
                    return;
                }

                using (var dbc = new ApplicationDbContext())
                {

                    var borrar = (from c in dbc.Clientes
                                  where c.NumeroDocumento == this.Cliente.NumeroDocumento
                                  select c).FirstOrDefault();

                    if (borrar != null)
                    {
                        dbc.Clientes.Remove(borrar);
                        dbc.SaveChanges();
                        MessageBox.Show("Cliente eliminado exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("El cliente no existe.");
                    }
                }

                this.Cliente.IdCliente = 0;
                this.Cliente.NumeroDocumento = "";
                this.Cliente.NombreCompleto = "";
                this.Cliente.Correo = "";
                this.Cliente.Telefono = "";
                this.Cliente.Estado = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ClienteVM -> No se pudo borrar el cliente | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }

        public void Modificar()
        {
            try
            {
                if ((this.Cliente.NumeroDocumento == null || this.Cliente.NumeroDocumento == "")  ||
                    (this.Cliente.NombreCompleto == null || this.Cliente.NombreCompleto == "") ||
                    (this.Cliente.Correo == null || this.Cliente.Correo == "") ||
                    (this.Cliente.Telefono == null || this.Cliente.Telefono == ""))
                {
                    MessageBox.Show("No pueden ir campos vacíos.");
                    return;
                }

                using (var dbc = new ApplicationDbContext())
                {
                    var cliente = (from e in dbc.Clientes
                                         where e.IdCliente == this.Cliente.IdCliente
                                         select e).FirstOrDefault();

                    if (cliente != null)
                    {
                        cliente.NumeroDocumento = this.Cliente.NumeroDocumento;
                        cliente.NombreCompleto = this.Cliente.NombreCompleto;
                        cliente.Correo = this.Cliente.Correo;
                        cliente.Telefono = this.Cliente.Telefono;
                        cliente.Estado = this.Cliente.Estado;
                        dbc.SaveChanges();
                        MessageBox.Show("Cliente modificado exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("El número de documento no existe.");
                        return;
                    }
                }

                this.Cliente.IdCliente = 0;
                this.Cliente.NumeroDocumento = "";
                this.Cliente.NombreCompleto = "";
                this.Cliente.Correo = "";
                this.Cliente.Telefono = "";
                this.Cliente.Estado = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ClienteVM -> No se pudo modificar el cliente | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }
    }
}
