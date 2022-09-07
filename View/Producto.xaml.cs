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

        private void volver_Menu_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }
    }
}
