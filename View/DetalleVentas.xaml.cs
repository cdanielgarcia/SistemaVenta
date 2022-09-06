using SistemaVenta.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.Win32;
using System.Globalization;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace SistemaVenta.View
{
    /// <summary>
    /// Lógica de interacción para Categoria.xaml
    /// </summary>
    public partial class DetalleVentas : System.Windows.Window
    {
        ApplicationDbContext dataEntities = new ApplicationDbContext();

        public DetalleVentas()
        {
            InitializeComponent();
        }

        private void getDataGrid(string Id)
        {
            var existVentaId = (from e in dataEntities.Ventas
                                where e.NumeroDocumento == Id
                                select e).FirstOrDefault();

            if (existVentaId != null)
            {
                var venta =
                from v in dataEntities.Ventas
                where v.NumeroDocumento == Id
                select new
                {
                    v.IdVenta,
                    v.IdUsuario,
                    v.TipoDocumento,
                    v.NumeroDocumento,
                    v.DocumentoCliente,
                    v.NombreCompleto,
                    v.MontoPago,
                    v.MontoCambio,
                    v.MontoTotal,
                    v.FechaRegistro
                };

                var detalleVenta =
                from d in dataEntities.DetalleVentas
                where d.IdVenta == existVentaId.IdVenta
                select new
                {
                    d.IdDetalleVenta,
                    d.IdVenta,
                    d.IdProducto,
                    d.PrecioVenta,
                    d.Cantidad,
                    d.SubTotal,
                    d.FechaRegistro
                };

                getDataVenta.ItemsSource = venta.ToList();
                getDataDetalleVenta.ItemsSource = detalleVenta.ToList();
            }
            else
            {
                getDataVenta.ItemsSource = null;
                getDataVenta.Items.Refresh();

                getDataDetalleVenta.ItemsSource = null;
                getDataDetalleVenta.Items.Refresh();

                MessageBox.Show("No existe el numero de documento de la Venta.");
                return;
            }
        }

        private void DetalleVentas_salir(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Consultar_venta(object sender, RoutedEventArgs e)
        {
            this.getDataGrid(txtDocumentoId.Text);
        }

        private void Descargar_pdf(object sender, RoutedEventArgs e)
        {
            if ((txtDocumentoId.Text == null || txtDocumentoId.Text == "") ||
                (txtFechaRegistro.Text == null || txtFechaRegistro.Text == "") ||
                (txtNombreCompleto.Text == null || txtNombreCompleto.Text == "") ||
                (txtNumeroDocumento.Text == null || txtNumeroDocumento.Text == "") ||
                (txtTipoDocumento.Text == null || txtTipoDocumento.Text == ""))
            {
                MessageBox.Show("Consulte primero si existe la Venta.");
                return;
            }

            var venta =
                (from v in dataEntities.Ventas
                 where v.NumeroDocumento == txtDocumentoId.Text
                 select v).FirstOrDefault();

            if (venta != null)
            {
                var detalleVenta =
                   (from d in dataEntities.DetalleVentas
                    where d.IdVenta == venta.IdVenta
                    select d).FirstOrDefault();

                if (detalleVenta != null)
                {
                    var dateTime = DateTime.Now.ToString("MMddyyyyHHmmss");

                    string download = Environment.GetEnvironmentVariable("USERPROFILE") + @"\" + "Downloads" + @"\" + "Factura_";

                    FileStream fs = new FileStream(download + txtDocumentoId.Text + ".pdf", FileMode.Create);
                    Document doc = new Document(PageSize.LETTER, 5, 5, 7, 7);
                    PdfWriter pw = PdfWriter.GetInstance(doc, fs);
                    doc.Open();
                    doc.AddAuthor("SistemaVentas");
                    doc.AddTitle("Detalle Venta: " + dateTime);
                    iTextSharp.text.Font standarFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA,
                        8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                    doc.Add(new iTextSharp.text.Paragraph("SISTEMA DE VENTAS"));
                    doc.Add(new iTextSharp.text.Paragraph("DETALLE DE LA VENTA | " + txtDocumentoId.Text));
                    doc.Add(new iTextSharp.text.Paragraph("Fecha: " + txtFechaRegistro.Text));
                    doc.Add(Chunk.NEWLINE);

                    PdfPTable tblDetalle = new PdfPTable(9);
                    tblDetalle.WidthPercentage = 100;

                    PdfPCell clTipoDoc = new PdfPCell(new Phrase("Tipo Documento", standarFont));
                    clTipoDoc.BorderWidth = 0;
                    clTipoDoc.BorderWidthBottom = 0.75f;

                    PdfPCell clNroDoc = new PdfPCell(new Phrase("Numero Documento", standarFont));
                    clNroDoc.BorderWidth = 0;
                    clNroDoc.BorderWidthBottom = 0.75f;

                    PdfPCell clDocCli = new PdfPCell(new Phrase("Documento Cliente", standarFont));
                    clDocCli.BorderWidth = 0;
                    clDocCli.BorderWidthBottom = 0.75f;

                    PdfPCell clNomCli = new PdfPCell(new Phrase("Nombre Cliente", standarFont));
                    clNomCli.BorderWidth = 0;
                    clNomCli.BorderWidthBottom = 0.75f;

                    PdfPCell clPrePro = new PdfPCell(new Phrase("Precio Producto", standarFont));
                    clPrePro.BorderWidth = 0;
                    clPrePro.BorderWidthBottom = 0.75f;

                    PdfPCell clCantidad = new PdfPCell(new Phrase("Cantidad", standarFont));
                    clCantidad.BorderWidth = 0;
                    clCantidad.BorderWidthBottom = 0.75f;

                    PdfPCell clMonPag = new PdfPCell(new Phrase("Monto Pagado", standarFont));
                    clMonPag.BorderWidth = 0;
                    clMonPag.BorderWidthBottom = 0.75f;

                    PdfPCell clMonCam = new PdfPCell(new Phrase("Monto Cambio", standarFont));
                    clMonCam.BorderWidth = 0;
                    clMonCam.BorderWidthBottom = 0.75f;

                    PdfPCell clTotPag = new PdfPCell(new Phrase("Total Venta", standarFont));
                    clTotPag.BorderWidth = 0;
                    clTotPag.BorderWidthBottom = 0.75f;

                    tblDetalle.AddCell(clTipoDoc);
                    tblDetalle.AddCell(clNroDoc);
                    tblDetalle.AddCell(clDocCli);
                    tblDetalle.AddCell(clNomCli);
                    tblDetalle.AddCell(clPrePro);
                    tblDetalle.AddCell(clCantidad);
                    tblDetalle.AddCell(clMonPag);
                    tblDetalle.AddCell(clMonCam);
                    tblDetalle.AddCell(clTotPag);

                    clTipoDoc = new PdfPCell(new Phrase(txtTipoDocumento.Text, standarFont));
                    clTipoDoc.BorderWidth = 0;

                    clNroDoc = new PdfPCell(new Phrase(txtDocumentoId.Text, standarFont));
                    clNroDoc.BorderWidth = 0;

                    clDocCli = new PdfPCell(new Phrase(txtNumeroDocumento.Text, standarFont));
                    clDocCli.BorderWidth = 0;

                    clNomCli = new PdfPCell(new Phrase(txtNombreCompleto.Text, standarFont));
                    clNomCli.BorderWidth = 0;

                    clPrePro = new PdfPCell(new Phrase(detalleVenta.PrecioVenta.ToString("F", CultureInfo.InvariantCulture), standarFont));
                    clPrePro.BorderWidth = 0;

                    clCantidad = new PdfPCell(new Phrase(detalleVenta.Cantidad.ToString(), standarFont));
                    clCantidad.BorderWidth = 0;

                    clMonPag = new PdfPCell(new Phrase(venta.MontoPago.ToString("F", CultureInfo.InvariantCulture), standarFont));
                    clMonPag.BorderWidth = 0;

                    clMonCam = new PdfPCell(new Phrase(venta.MontoCambio.ToString("F", CultureInfo.InvariantCulture), standarFont));
                    clMonCam.BorderWidth = 0;

                    clTotPag = new PdfPCell(new Phrase(venta.MontoTotal.ToString("F", CultureInfo.InvariantCulture), standarFont));
                    clTotPag.BorderWidth = 0;

                    tblDetalle.AddCell(clTipoDoc);
                    tblDetalle.AddCell(clNroDoc);
                    tblDetalle.AddCell(clDocCli);
                    tblDetalle.AddCell(clNomCli);
                    tblDetalle.AddCell(clPrePro);
                    tblDetalle.AddCell(clCantidad);
                    tblDetalle.AddCell(clMonPag);
                    tblDetalle.AddCell(clMonCam);
                    tblDetalle.AddCell(clTotPag);

                    doc.Add(tblDetalle);
                    doc.Close();
                    pw.Close();

                    MessageBox.Show("PDF - Generado exitosamente.");
                }
                else
                {
                    MessageBox.Show("No se pudo obtener el detalle de la Venta.");
                }
            }
            else
            {
                MessageBox.Show("No se encuentra la venta.");
            }
        }

        private void Descargar_excel(object sender, RoutedEventArgs e)
        {
            if ((txtDocumentoId.Text == null || txtDocumentoId.Text == "") ||
                (txtFechaRegistro.Text == null || txtFechaRegistro.Text == "") ||
                (txtNombreCompleto.Text == null || txtNombreCompleto.Text == "") ||
                (txtNumeroDocumento.Text == null || txtNumeroDocumento.Text == "") ||
                (txtTipoDocumento.Text == null || txtTipoDocumento.Text == ""))
            {
                MessageBox.Show("Consulte primero si existe la Venta.");
                return;
            }

            var venta =
                (from v in dataEntities.Ventas
                 where v.NumeroDocumento == txtDocumentoId.Text
                 select v).FirstOrDefault();

            if (venta != null)
            {
                var detalleVenta =
                   (from d in dataEntities.DetalleVentas
                    where d.IdVenta == venta.IdVenta
                    select d).FirstOrDefault();

                if (detalleVenta != null)
                {
                    Excel.Application excel = new Excel.Application();

                    excel.Visible = true;
                    Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                    Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

                    for (int j = 0; j < getDataVenta.Columns.Count; j++)
                    {
                        Excel.Range myRange = (Excel.Range)sheet1.Cells[1, j + 1];
                        sheet1.Cells[1, j + 1].Font.Bold = true;
                        sheet1.Columns[j + 1].ColumnWidth = 15;
                        myRange.Value2 = getDataVenta.Columns[j].Header;
                    }

                    for (int i = 0; i < getDataVenta.Columns.Count; i++)
                    {
                        for (int j = 0; j < getDataVenta.Items.Count; j++)
                        {
                            TextBlock? b = getDataVenta.Columns[i].GetCellContent(getDataVenta.Items[j]) as TextBlock;
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                            myRange.Value2 = b.Text;
                        }
                    }

                    for (int j = 0; j < getDataDetalleVenta.Columns.Count; j++)
                    {
                        Excel.Range myRange2 = (Excel.Range)sheet1.Cells[5, j + 1];
                        sheet1.Cells[5, j + 1].Font.Bold = true;
                        sheet1.Columns[j + 1].ColumnWidth = 15;
                        myRange2.Value2 = getDataDetalleVenta.Columns[j].Header;
                    }

                    for (int i = 0; i < getDataDetalleVenta.Columns.Count; i++)
                    {
                        for (int j = 0; j < getDataDetalleVenta.Items.Count; j++)
                        {
                            TextBlock? b = getDataDetalleVenta.Columns[i].GetCellContent(getDataDetalleVenta.Items[j]) as TextBlock;
                            Microsoft.Office.Interop.Excel.Range myRange2 = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 6, i + 1];
                            myRange2.Value2 = b.Text;
                        }
                    }

                    MessageBox.Show("Excel - Generado exitosamente.");
                }
                else
                {
                    MessageBox.Show("No se pudo obtener el detalle de la Venta.");
                }
            }
            else
            {
                MessageBox.Show("No se encuentra la venta.");
            }
        }

        private void volver_Menu_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }
    }
}
