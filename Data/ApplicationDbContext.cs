using System.Data;
using SistemaVenta.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SistemaVenta.Data
{

    public class ApplicationDbContext : DbContext
    {


        /*public ApplicationDbContext()
  : base("name=ApplicationDbContext")
{
}*/
    
        public ApplicationDbContext() 
            : base(GetOptions("Server=(localdb)\\MSSQLLocalDB;Database=SistemaVentas;Trusted_Connection=True;MultipleActiveResultSets=true"))
        {
        }


        private static DbContextOptions GetOptions(string connectionString)
        {
            var prueba = SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
            return prueba;
        }

        public virtual DbSet<Cliente>? Clientes { get; set; }

        public virtual DbSet<Categoria>? Categorias { get; set; }
        public virtual DbSet<Compra>? Compras { get; set; }
        public virtual DbSet<DetalleCompra>? DetalleCompras { get; set; }
        public virtual DbSet<DetalleVenta>? DetalleVentas { get; set; }
        public virtual DbSet<Permiso>? Permisos { get; set; }
        public virtual DbSet<Producto>? Productos { get; set; }
        public virtual DbSet<Proveedor>? Proveedores { get; set; }
        public virtual DbSet<Rol>? Roles { get; set; }
        public virtual DbSet<Usuario>? Usuarios { get; set; }
        public virtual DbSet<Venta>? Ventas { get; set; }

    }

}


/*protected override void OnModelCreating(DbModelBuilder modelBuilder)
{
    // Aquí haremos nuestras configuraciones con Fluent API.

    // Especificar el nombre de una tabla.
    modelBuilder.Entity<Categoria>().Map(m => m.ToTable("Categoria"));
    modelBuilder.Entity<Cliente>().Map(m => m.ToTable("Cliente"));
    modelBuilder.Entity<Compra>().Map(m => m.ToTable("Compra"));

    /*
    // establecer una primary key.
    modelBuilder.Entity<Producto>().HasKey(c => c.Codigo);

    // Definir un campo como requerida.
    modelBuilder.Entity<Producto>().Property(c => c.Nombre).IsRequired();

    // Definir el nombre de un campo.
    modelBuilder.Entity<Producto>().Property(c => c.Nombre).HasColumnName("Nombre");

    // Definir el tipo de un campo.
    modelBuilder.Entity<Producto>().Property(c => c.Nombre).HasColumnType("varchar");

    // Definir el orden de un campo.
    modelBuilder.Entity<Producto>().Property(c => c.Nombre).HasColumnOrder(2);

    // Definir el máximo de caracteres permitidos para un campo.
    modelBuilder.Entity<Producto>().Property(c => c.Descripcion).HasMaxLength(100);

    // indicar que no se debe mapear una pripiedad a la base de datos.
    modelBuilder.Entity<Producto>().Ignore(c => c.CodigoIso);   */

//base.OnModelCreating(modelBuilder);




//public class MyEntity
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//}

