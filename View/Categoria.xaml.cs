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

        private void Insertar_Click(object sender, RoutedEventArgs e)
        {
            txtDescripcion.Text = "";
            comboStatus.SelectedIndex = 0;
        }

        private void Modificar_Click(object sender, RoutedEventArgs e)
        {
            txtDescripcion.Text = "";
            comboStatus.SelectedIndex = 0;
        }

        private void Borrar_Click(object sender, RoutedEventArgs e)
        {
            txtDescripcion.Text = "";
            comboStatus.SelectedIndex = 0;
        }

        private void Consultar_Click(object sender, RoutedEventArgs e)
        {
            var query =
            from category in dataEntities.Categorias
            orderby category.FechaCreacion descending
            select new { category.IdCategoria, category.Descripcion, category.Estado, category.FechaCreacion };

            getData.ItemsSource = query.ToList();
        }
    }
}
