using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SistemaVenta.Data;
using SistemaVenta.Model;
using Torbellino;

namespace SistemaVenta.ViewModel
{
    public class DetalleVentasVM : INotifyObject
    {
        public RelayCommand cmd_Insertar { get; set; }
        public RelayCommand cmd_Consultar { get; set; }
        public RelayCommand cmd_Borrar { get; set; }
        public RelayCommand cmd_Modifica { get; set; }

        public DetalleVenta DetalleVenta { get { return detalleVenta; } set { detalleVenta = value; OnPropertyChanged(); } }
        private DetalleVenta detalleVenta;

        public ObservableCollection<DetalleVenta> Lista { get { return lista; } set { lista = value; OnPropertyChanged(); } }

        private ObservableCollection<DetalleVenta> lista = new ObservableCollection<DetalleVenta>();

        public DetalleVentasVM()
        {
            //this.cmd_Insertar = new RelayCommand(p => this.Insertar());
            //this.cmd_Consultar = new RelayCommand(p => this.Consultar());
            //this.cmd_Borrar = new RelayCommand(p => this.Borrar());
            //this.cmd_Modifica = new RelayCommand(p => this.Modificar());
            this.DetalleVenta = new DetalleVenta();
        }

        //public void Insertar()
        //{
        //    try
        //    {
        //        if ((this.Cliente.NumeroDocumento == null || this.Cliente.NumeroDocumento == "") ||
        //            (this.Cliente.NombreCompleto == null || this.Cliente.NombreCompleto == "") ||
        //            (this.Cliente.Correo == null || this.Cliente.Correo == "") ||
        //            (this.Cliente.Telefono == null || this.Cliente.Telefono == ""))
        //        {
        //            MessageBox.Show("Llené todos los campos primero.");
        //            return;
        //        }

        //        this.Cliente.FechaCreacion = DateTime.Now;

        //        using (var dbc = new ApplicationDbContext())
        //        {
        //            dbc.Clientes.Add(this.Cliente);
        //            dbc.SaveChanges();
        //        }

        //        MessageBox.Show("Cliente registrado exitosamente.");

        //        this.Consultar();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error ClienteVM -> No se pudo registrar el cliente | " + ex.Message);
        //        if (ex.InnerException != null)
        //            MessageBox.Show("Error " + ex.InnerException.Message);
        //    }
        //}

        //public void Consultar()
        //{
        //    try
        //    {
        //        using (var dbc = new ApplicationDbContext())
        //        {
        //            this.Lista = new ObservableCollection<Cliente>(dbc.Clientes);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error ClienteVM -> No se pudo consultar el cliente | " + ex.Message);
        //        if (ex.InnerException != null)
        //            MessageBox.Show("Error " + ex.InnerException.Message);
        //    }
        //}

        //public void Borrar()
        //{
        //    try
        //    {
        //        using (var dbc = new ApplicationDbContext())
        //        {

        //            var borrar = (from c in dbc.Clientes
        //                        where c.NumeroDocumento == this.Cliente.NumeroDocumento
        //                        select c).Single();

        //            dbc.Clientes.Remove(borrar);
        //            dbc.SaveChanges();

        //            this.Lista = new ObservableCollection<Cliente>(dbc.Clientes);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error ClienteVM -> No se pudo borrar el cliente | " + ex.Message);
        //        if (ex.InnerException != null)
        //            MessageBox.Show("Error " + ex.InnerException.Message);
        //    }
        //}

        //public void Modificar()
        //{
        //    try
        //    {
        //        if ((this.Cliente.NumeroDocumento == null || this.Cliente.NumeroDocumento == "")  ||
        //            (this.Cliente.NombreCompleto == null || this.Cliente.NombreCompleto == "") ||
        //            (this.Cliente.Correo == null || this.Cliente.Correo == "") ||
        //            (this.Cliente.Telefono == null || this.Cliente.Telefono == ""))
        //        {
        //            MessageBox.Show("No pueden ir campos vacíos.");
        //            return;
        //        }

        //        using (var dbc = new ApplicationDbContext())
        //        {
        //            var cliente = dbc.Clientes.Find(this.Cliente.IdCliente);

        //            var existDocument = (from e in dbc.Clientes
        //                                 where e.NumeroDocumento == this.Cliente.NumeroDocumento
        //                                 select e).FirstOrDefault();

        //            if (existDocument == null)
        //            {
        //                cliente.NumeroDocumento = this.Cliente.NumeroDocumento;
        //                cliente.NombreCompleto = this.Cliente.NombreCompleto;
        //                cliente.Correo = this.Cliente.Correo;
        //                cliente.Telefono = this.Cliente.Telefono;
        //                dbc.SaveChanges();
        //                this.Consultar();
        //            }
        //            else
        //            {
        //                MessageBox.Show("El número de documento ya existe.");
        //                return;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error ClienteVM -> No se pudo modificar el cliente | " + ex.Message);
        //        if (ex.InnerException != null)
        //            MessageBox.Show("Error " + ex.InnerException.Message);
        //    }
        //}
    }
}
