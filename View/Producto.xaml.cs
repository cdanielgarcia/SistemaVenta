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

namespace SistemaVenta.View
{
    /// <summary>
    /// Lógica de interacción para Producto.xaml
    /// </summary>
    public partial class Producto : Window
    {
        ApplicationDbContext dataEntities = new ApplicationDbContext();

        public Producto()
        {
            InitializeComponent();

            var categoria =
            from c in dataEntities.Categorias
            select new { Name = c.Descripcion, ID = c.IdCategoria };
            comboCategoria.ItemsSource = categoria.ToList();
            comboCategoria.DisplayMemberPath = "Name";
            comboCategoria.SelectedValuePath = "ID";
        }

        private void Consultar_click(object sender, RoutedEventArgs e)
        {
            var query =
            from product in dataEntities.Productos
            orderby product.FechaRegistro descending
            select new {
                product.Codigo,
                product.Nombre,
                product.Descripcion,
                product.IdCategoria,
                product.Estado,
                product.IdProducto
            };

            getData.ItemsSource = query.ToList();
        }

        private void getData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (getData.SelectedItem == null) return;

            if (getData.SelectedCells.Count > 0)
            {
                for (int i = 0; i < getData.SelectedCells.Count; i++)
                {
                    int categoryId = 0;
                    var CellValue = GetSelectedValue(getData, i);

                    if (i == 0)
                        txtCodigo.Text = CellValue;

                    if (i == 1)
                        txtNombre.Text = CellValue;

                    if (i == 2)
                        txtDescripcion.Text = CellValue;

                    if (i == 3)
                    {
                        categoryId = Convert.ToInt32(CellValue);

                        var getCategory =
                        (from r in dataEntities.Categorias
                         where r.IdCategoria == categoryId
                         select r).FirstOrDefault();

                        comboCategoria.Text = getCategory?.Descripcion;
                    }

                    if (i == 4)
                    {
                        if (CellValue == "True")
                        {
                            comboStatus.SelectedIndex = 1;
                        }
                        else
                        {
                            comboStatus.SelectedIndex = 2;
                        }
                    }

                    if (i == 5)
                        txtId.Text = CellValue;
                }
            }
        }

        private string GetSelectedValue(DataGrid grid, int i)
        {
            DataGridCellInfo cellInfo = grid.SelectedCells[i];
            if (cellInfo == null) return null;

            DataGridBoundColumn column = cellInfo.Column as DataGridBoundColumn;
            if (column == null) return null;

            FrameworkElement element = new FrameworkElement() { DataContext = cellInfo.Item };
            BindingOperations.SetBinding(element, TagProperty, column.Binding);

            return element.Tag.ToString();
        }

        private void volver_Menu_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }
    }
}
