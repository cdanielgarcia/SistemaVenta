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
        public RelayCommand cmd_Consultar { get; set; }

        public DetalleVenta DetalleVenta { get { return detalleVenta; } set { detalleVenta = value; OnPropertyChanged(); } }
        private DetalleVenta detalleVenta;

        public Venta Venta { get { return venta; } set { venta = value; OnPropertyChanged(); } }
        private Venta venta;

        public DetalleVentasVM()
        {
            this.cmd_Consultar = new RelayCommand(p => this.Consultar());
            this.DetalleVenta = new DetalleVenta();
            this.Venta = new Venta();
        }
        public void Consultar()
        {
            try
            {
                var getDocumentId = this.Venta.NumeroDocumento;

                using (var dbc = new ApplicationDbContext())
                {
                    var existDocumentId = (from v in dbc.Ventas
                                        where v.NumeroDocumento == getDocumentId
                                        select new
                                        {
                                            v.FechaRegistro,
                                            v.TipoDocumento,
                                            v.DocumentoCliente,
                                            v.NombreCompleto
                                        }).FirstOrDefault();

                    if (existDocumentId != null)
                    {
                        this.Venta.FechaRegistro = existDocumentId.FechaRegistro;
                        this.Venta.TipoDocumento = existDocumentId.TipoDocumento;
                        this.Venta.DocumentoCliente = existDocumentId.DocumentoCliente;
                        this.Venta.NombreCompleto = existDocumentId.NombreCompleto;
                    }
                    else
                    {
                        MessageBox.Show("El número documento de la factura no existe en Ventas.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error DetalleVentasVM -> No se pudo consultar el Detalle de la Venta | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }
    }
}
