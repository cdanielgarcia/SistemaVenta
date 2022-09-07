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
    public partial class DetalleCompras : System.Windows.Window
    {
        ApplicationDbContext dataEntities = new ApplicationDbContext();

        public DetalleCompras()
        {
            InitializeComponent();
        }

        private void getDataGrid(string Id)
        {
            var existPurcharseId = (from e in dataEntities.Compras
                                where e.NumeroDocumento == Id
                                select e).FirstOrDefault();

            if (existPurcharseId != null)
            {
                var compra =
                from c in dataEntities.Compras
                where c.NumeroDocumento == Id
                select new
                {
                    c.IdCompra,
                    c.IdUsuario,
                    c.IdProveedor,
                    c.TipoDocumento,
                    c.NumeroDocumento,
                    c.MontoTotal,
                    c.FechaRegistro
                };

                var detalleCompra =
                from d in dataEntities.DetalleCompras
                where d.IdCompra == existPurcharseId.IdCompra
                select new
                {
                    d.IdDetalleCompra,
                    d.IdCompra,
                    d.IdProducto,
                    d.PrecioCompra,
                    d.PrecioVenta,
                    d.Cantidad,
                    d.MontoTotal,
                    d.FechaRegistro
                };

                getDataCompra.ItemsSource = compra.ToList();
                getDataDetalleCompra.ItemsSource = detalleCompra.ToList();
            }
            else
            {
                getDataCompra.ItemsSource = null;
                getDataCompra.Items.Refresh();

                getDataDetalleCompra.ItemsSource = null;
                getDataDetalleCompra.Items.Refresh();

                MessageBox.Show("No existe el numero de documento de la Compra.");
                return;
            }
        }

        private void DetalleCompras_salir(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Consultar_compra(object sender, RoutedEventArgs e)
        {
            this.getDataGrid(txtDocumentoId.Text);

            var getPurcharse = (from c in dataEntities.Compras
                                where c.NumeroDocumento == txtDocumentoId.Text
                                select c).FirstOrDefault();

            if (getPurcharse != null)
            {
                var getInfoPro = (from p in dataEntities.Proveedores
                                  where p.IdProveedor == getPurcharse.IdProveedor
                                  select p).FirstOrDefault();

                txtNumeroDocumento.Text = getPurcharse.NumeroDocumento;
                txtNombreCompleto.Text = getInfoPro.RazonSocial;
            }
        }

        private void Descargar_pdf(object sender, RoutedEventArgs e)
        {
            if ((txtDocumentoId.Text == null || txtDocumentoId.Text == "") ||
                (txtFechaRegistro.Text == null || txtFechaRegistro.Text == "") ||
                (txtNombreCompleto.Text == null || txtNombreCompleto.Text == "") ||
                (txtNumeroDocumento.Text == null || txtNumeroDocumento.Text == "") ||
                (txtTipoDocumento.Text == null || txtTipoDocumento.Text == ""))
            {
                MessageBox.Show("Consulte primero si existe la Compra.");
                return;
            }

            var compra =
                (from c in dataEntities.Compras
                 where c.NumeroDocumento == txtDocumentoId.Text
                 select c).FirstOrDefault();

            if (compra != null)
            {
                var detalleCompra =
                   (from d in dataEntities.DetalleCompras
                    where d.IdCompra == compra.IdCompra
                    select d).FirstOrDefault();

                if (detalleCompra != null)
                {
                    var getProduct =
                        (from p in dataEntities.Productos
                        where p.IdProducto == detalleCompra.IdProducto
                        select p).FirstOrDefault();

                    var getPro =
                        (from p in dataEntities.Proveedores
                         where p.IdProveedor == compra.IdProveedor
                         select p).FirstOrDefault();

                    var dateTime = DateTime.Now.ToString("MMddyyyyHHmmss");

                    string download = Environment.GetEnvironmentVariable("USERPROFILE") + @"\" + "Downloads" + @"\" + "Factura_";

                    FileStream fs = new FileStream(download + txtDocumentoId.Text + ".pdf", FileMode.Create);
                    Document doc = new Document(PageSize.LETTER, 5, 5, 7, 7);
                    PdfWriter pw = PdfWriter.GetInstance(doc, fs);
                    doc.Open();
                    doc.AddAuthor("SistemaVentas");
                    doc.AddTitle("Detalle Compra: " + dateTime);
                    iTextSharp.text.Font standarFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA,
                        8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                    doc.Add(new iTextSharp.text.Paragraph("SISTEMA DE VENTAS"));
                    doc.Add(new iTextSharp.text.Paragraph("DETALLE DE LA COMPRA | " + txtDocumentoId.Text));
                    doc.Add(new iTextSharp.text.Paragraph("Fecha: " + txtFechaRegistro.Text));
                    doc.Add(Chunk.NEWLINE);

                    PdfPTable tblDetalle = new PdfPTable(7);
                    tblDetalle.WidthPercentage = 100;

                    PdfPCell clNroDoc = new PdfPCell(new Phrase("Numero Documento", standarFont));
                    clNroDoc.BorderWidth = 0;
                    clNroDoc.BorderWidthBottom = 0.75f;

                    PdfPCell clNomCli = new PdfPCell(new Phrase("Nombre Proveedor", standarFont));
                    clNomCli.BorderWidth = 0;
                    clNomCli.BorderWidthBottom = 0.75f;

                    PdfPCell clNomPro = new PdfPCell(new Phrase("Nombre Producto", standarFont));
                    clNomPro.BorderWidth = 0;
                    clNomPro.BorderWidthBottom = 0.75f;

                    PdfPCell clCantidad = new PdfPCell(new Phrase("Cantidad", standarFont));
                    clCantidad.BorderWidth = 0;
                    clCantidad.BorderWidthBottom = 0.75f;

                    PdfPCell clMonPag = new PdfPCell(new Phrase("Precio Compra", standarFont));
                    clMonPag.BorderWidth = 0;
                    clMonPag.BorderWidthBottom = 0.75f;

                    PdfPCell clMonCam = new PdfPCell(new Phrase("Precio Venta", standarFont));
                    clMonCam.BorderWidth = 0;
                    clMonCam.BorderWidthBottom = 0.75f;

                    PdfPCell clTotPag = new PdfPCell(new Phrase("Monto Total", standarFont));
                    clTotPag.BorderWidth = 0;
                    clTotPag.BorderWidthBottom = 0.75f;

                    tblDetalle.AddCell(clNroDoc);
                    tblDetalle.AddCell(clNomCli);
                    tblDetalle.AddCell(clNomPro);
                    tblDetalle.AddCell(clCantidad);
                    tblDetalle.AddCell(clMonPag);
                    tblDetalle.AddCell(clMonCam);
                    tblDetalle.AddCell(clTotPag);

                    clNroDoc = new PdfPCell(new Phrase(txtDocumentoId.Text, standarFont));
                    clNroDoc.BorderWidth = 0;

                    clNomCli = new PdfPCell(new Phrase(txtNombreCompleto.Text, standarFont));
                    clNomCli.BorderWidth = 0;

                    clNomPro = new PdfPCell(new Phrase(getProduct.Nombre + " " + getProduct.Descripcion, standarFont));
                    clNomPro.BorderWidth = 0;

                    clCantidad = new PdfPCell(new Phrase(detalleCompra.Cantidad.ToString(), standarFont));
                    clCantidad.BorderWidth = 0;

                    clMonPag = new PdfPCell(new Phrase(detalleCompra.PrecioCompra.ToString("F", CultureInfo.InvariantCulture), standarFont));
                    clMonPag.BorderWidth = 0;

                    clMonCam = new PdfPCell(new Phrase(detalleCompra.PrecioVenta.ToString("F", CultureInfo.InvariantCulture), standarFont));
                    clMonCam.BorderWidth = 0;

                    clTotPag = new PdfPCell(new Phrase(compra.MontoTotal.ToString("F", CultureInfo.InvariantCulture), standarFont));
                    clTotPag.BorderWidth = 0;

                    tblDetalle.AddCell(clNroDoc);
                    tblDetalle.AddCell(clNomCli);
                    tblDetalle.AddCell(clNomPro);
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
                    MessageBox.Show("No se pudo obtener el detalle de la Compra.");
                }
            }
            else
            {
                MessageBox.Show("No se encuentra la Compra.");
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
                MessageBox.Show("Consulte primero si existe la Compra.");
                return;
            }

            var compra =
                 (from c in dataEntities.Compras
                  where c.NumeroDocumento == txtDocumentoId.Text
                  select c).FirstOrDefault();

            if (compra != null)
            {
                var detalleCompra =
                   (from d in dataEntities.DetalleCompras
                    where d.IdCompra == compra.IdCompra
                    select d).FirstOrDefault();

                if (detalleCompra != null)
                {
                    Excel.Application excel = new Excel.Application();

                    excel.Visible = true;
                    Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                    Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

                    for (int j = 0; j < getDataCompra.Columns.Count; j++)
                    {
                        Excel.Range myRange = (Excel.Range)sheet1.Cells[1, j + 1];
                        sheet1.Cells[1, j + 1].Font.Bold = true;
                        sheet1.Columns[j + 1].ColumnWidth = 15;
                        myRange.Value2 = getDataCompra.Columns[j].Header;
                    }

                    for (int i = 0; i < getDataCompra.Columns.Count; i++)
                    {
                        for (int j = 0; j < getDataCompra.Items.Count; j++)
                        {
                            TextBlock? b = getDataCompra.Columns[i].GetCellContent(getDataCompra.Items[j]) as TextBlock;
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                            myRange.Value2 = b.Text;
                        }
                    }

                    for (int j = 0; j < getDataDetalleCompra.Columns.Count; j++)
                    {
                        Excel.Range myRange2 = (Excel.Range)sheet1.Cells[5, j + 1];
                        sheet1.Cells[5, j + 1].Font.Bold = true;
                        sheet1.Columns[j + 1].ColumnWidth = 15;
                        myRange2.Value2 = getDataDetalleCompra.Columns[j].Header;
                    }

                    for (int i = 0; i < getDataDetalleCompra.Columns.Count; i++)
                    {
                        for (int j = 0; j < getDataDetalleCompra.Items.Count; j++)
                        {
                            TextBlock? b = getDataDetalleCompra.Columns[i].GetCellContent(getDataDetalleCompra.Items[j]) as TextBlock;
                            Microsoft.Office.Interop.Excel.Range myRange2 = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 6, i + 1];
                            myRange2.Value2 = b.Text;
                        }
                    }

                    MessageBox.Show("Excel - Generado exitosamente.");
                }
                else
                {
                    MessageBox.Show("No se pudo obtener el detalle de la Compra.");
                }
            }
            else
            {
                MessageBox.Show("No se encuentra la Compra.");
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
