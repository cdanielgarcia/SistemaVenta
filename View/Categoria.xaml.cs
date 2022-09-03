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
    /// Lógica de interacción para Categoria.xaml
    /// </summary>
    public partial class Categoria : Window
    {
        ApplicationDbContext dataEntities = new ApplicationDbContext();

        public Categoria()
        {
            InitializeComponent();
        }

        private void Categoria_salir(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Consultar_Click(object sender, RoutedEventArgs e)
        {
            var query =
            from category in dataEntities.Categorias
            orderby category.FechaCreacion descending
            select new { category.IdCategoria, category.Descripcion, category.Estado, category.FechaCreacion };

            getData.ItemsSource = query.ToList();
        }

        private void getData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (getData.SelectedItem == null) return;

            if (getData.SelectedCells.Count > 0)
            {
                for (int i = 0; i < getData.SelectedCells.Count; i++)
                {
                    var CellValue = GetSelectedValue(getData, i);

                    if (i == 0)
                        txtId.Text = CellValue;

                    if (i == 1) 
                        txtDescripcion.Text = CellValue;
                    
                    if (i == 2)
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

    }
}
