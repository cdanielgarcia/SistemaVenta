// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaVenta.Data;

#nullable disable

namespace SistemaVenta.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SistemaVenta.Model.Categoria", b =>
                {
                    b.Property<int>("IdCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCategoria"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.HasKey("IdCategoria");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("SistemaVenta.Model.Cliente", b =>
                {
                    b.Property<int>("IdCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCliente"), 1L, 1);

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("NombreCompleto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroDocumento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCliente");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("SistemaVenta.Model.Compra", b =>
                {
                    b.Property<int>("IdCompra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCompra"), 1L, 1);

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdProveedor")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<decimal>("MontoTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NumeroDocumento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoDocumento")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCompra");

                    b.HasIndex("IdProveedor");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("SistemaVenta.Model.DetalleCompra", b =>
                {
                    b.Property<int>("IdDetalleCompra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDetalleCompra"), 1L, 1);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCompra")
                        .HasColumnType("int");

                    b.Property<int>("IdProducto")
                        .HasColumnType("int");

                    b.Property<decimal>("MontoTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecioCompra")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecioVenta")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdDetalleCompra");

                    b.HasIndex("IdCompra");

                    b.HasIndex("IdProducto");

                    b.ToTable("DetalleCompras");
                });

            modelBuilder.Entity("SistemaVenta.Model.DetalleVenta", b =>
                {
                    b.Property<int>("IdDetalleVenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDetalleVenta"), 1L, 1);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdProducto")
                        .HasColumnType("int");

                    b.Property<int>("IdVenta")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecioVenta")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdDetalleVenta");

                    b.HasIndex("IdProducto");

                    b.HasIndex("IdVenta");

                    b.ToTable("DetalleVentas");
                });

            modelBuilder.Entity("SistemaVenta.Model.Permiso", b =>
                {
                    b.Property<int>("IdPermiso")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPermiso"), 1L, 1);

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.Property<string>("NombreMenu")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPermiso");

                    b.HasIndex("IdRol");

                    b.ToTable("Permisos");
                });

            modelBuilder.Entity("SistemaVenta.Model.Producto", b =>
                {
                    b.Property<int>("IdProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProducto"), 1L, 1);

                    b.Property<string>("Codigo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCategoria")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PrecioCompra")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecioVenta")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("IdProducto");

                    b.HasIndex("IdCategoria");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("SistemaVenta.Model.Proveedor", b =>
                {
                    b.Property<int>("IdProveedor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProveedor"), 1L, 1);

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("NumeroDocumento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RazonSocial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdProveedor");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("SistemaVenta.Model.Rol", b =>
                {
                    b.Property<int>("IdRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRol"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.HasKey("IdRol");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SistemaVenta.Model.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"), 1L, 1);

                    b.Property<string>("Clave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.Property<string>("NombreCompleto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroDocumento")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUsuario");

                    b.HasIndex("IdRol");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("SistemaVenta.Model.Venta", b =>
                {
                    b.Property<int>("IdVenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdVenta"), 1L, 1);

                    b.Property<string>("DocumentoCliente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<decimal>("MontoCambio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MontoPago")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MontoTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NombreCompleto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroDocumento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoDocumento")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdVenta");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("SistemaVenta.Model.Compra", b =>
                {
                    b.HasOne("SistemaVenta.Model.Proveedor", "Proveedor")
                        .WithMany("cCompra")
                        .HasForeignKey("IdProveedor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaVenta.Model.Usuario", "Usuario")
                        .WithMany("cCompra")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proveedor");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("SistemaVenta.Model.DetalleCompra", b =>
                {
                    b.HasOne("SistemaVenta.Model.Compra", "Compra")
                        .WithMany("cDetalleCompra")
                        .HasForeignKey("IdCompra")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaVenta.Model.Producto", "Producto")
                        .WithMany("cDetalleCompra")
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compra");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("SistemaVenta.Model.DetalleVenta", b =>
                {
                    b.HasOne("SistemaVenta.Model.Producto", "Producto")
                        .WithMany("cDetalleVenta")
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaVenta.Model.Venta", "Venta")
                        .WithMany("cDetalleVenta")
                        .HasForeignKey("IdVenta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("SistemaVenta.Model.Permiso", b =>
                {
                    b.HasOne("SistemaVenta.Model.Rol", "Rol")
                        .WithMany("cPermiso")
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("SistemaVenta.Model.Producto", b =>
                {
                    b.HasOne("SistemaVenta.Model.Categoria", "Categoria")
                        .WithMany("cProducto")
                        .HasForeignKey("IdCategoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("SistemaVenta.Model.Usuario", b =>
                {
                    b.HasOne("SistemaVenta.Model.Rol", "Rol")
                        .WithMany("cUsuario")
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("SistemaVenta.Model.Venta", b =>
                {
                    b.HasOne("SistemaVenta.Model.Usuario", "Usuario")
                        .WithMany("vVenta")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("SistemaVenta.Model.Categoria", b =>
                {
                    b.Navigation("cProducto");
                });

            modelBuilder.Entity("SistemaVenta.Model.Compra", b =>
                {
                    b.Navigation("cDetalleCompra");
                });

            modelBuilder.Entity("SistemaVenta.Model.Producto", b =>
                {
                    b.Navigation("cDetalleCompra");

                    b.Navigation("cDetalleVenta");
                });

            modelBuilder.Entity("SistemaVenta.Model.Proveedor", b =>
                {
                    b.Navigation("cCompra");
                });

            modelBuilder.Entity("SistemaVenta.Model.Rol", b =>
                {
                    b.Navigation("cPermiso");

                    b.Navigation("cUsuario");
                });

            modelBuilder.Entity("SistemaVenta.Model.Usuario", b =>
                {
                    b.Navigation("cCompra");

                    b.Navigation("vVenta");
                });

            modelBuilder.Entity("SistemaVenta.Model.Venta", b =>
                {
                    b.Navigation("cDetalleVenta");
                });
#pragma warning restore 612, 618
        }
    }
}
