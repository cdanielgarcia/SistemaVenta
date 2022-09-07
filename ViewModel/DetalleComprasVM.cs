using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SistemaVenta.Data;
using SistemaVenta.Model;
using Torbellino;

namespace SistemaVenta.ViewModel
{
    public class DetalleComprasVM : INotifyObject
    {
        public RelayCommand cmd_Consultar { get; set; }

        public DetalleCompra DetalleCompra { get { return detalleCompra; } set { detalleCompra = value; OnPropertyChanged(); } }
        private DetalleCompra detalleCompra;

        public Compra Compra { get { return compra; } set { compra = value; OnPropertyChanged(); } }
        private Compra compra;

        public DetalleComprasVM()
        {
            this.cmd_Consultar = new RelayCommand(p => this.Consultar());
            this.DetalleCompra = new DetalleCompra();
            this.Compra = new Compra();
        }
        public void Consultar()
        {
            try
            {
                var getDocumentId = this.Compra.NumeroDocumento;

                using (var dbc = new ApplicationDbContext())
                {
                    var existDocumentId = (from v in dbc.Compras
                                        where v.NumeroDocumento == getDocumentId
                                        select v).FirstOrDefault();

                    if (existDocumentId != null)
                    {
                        this.Compra.FechaRegistro = existDocumentId.FechaRegistro;
                        this.Compra.TipoDocumento = existDocumentId.TipoDocumento;
                    }
                    else
                    {
                        this.Compra.FechaRegistro = DateTime.Now;
                        this.Compra.TipoDocumento = "";
                        MessageBox.Show("El número documento de la factura no existe en Compras.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error DetalleComprasVM -> No se pudo consultar el Detalle de la Compra | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }
    }
}
