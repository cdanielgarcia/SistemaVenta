using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SistemaVenta.Data;
using SistemaVenta.Model;
using Torbellino;

namespace SistemaVenta.ViewModel
{
    public class IniciarSesionVM : INotifyObject
    {
        public RelayCommand cmd_Consultar { get; set; }

        public Usuario Usuario { get { return usuario; } set { usuario = value; OnPropertyChanged(); } }
        private Usuario usuario;

        public IniciarSesionVM()
        {
            this.cmd_Consultar = new RelayCommand(p => this.Consultar());
            this.Usuario = new Usuario();
        }

        public void Consultar()
        {
            try
            {
                var getUsuario = this.Usuario.Correo;
                var getClave = this.Usuario.Clave;

                using (var dbc = new ApplicationDbContext())
                {
                    var existUser = (from u in dbc.Usuarios
                                    where u.Correo == getUsuario &&
                                    u.Clave == getClave &&
                                    u.Estado == true
                                    select u.NombreCompleto).FirstOrDefault();

                    if (existUser != null)
                    {
                        this.Usuario.Estado = true;
                        MessageBox.Show($"¡Bienvenido {existUser}!");
                    }
                    else
                    {
                        MessageBox.Show("El correo o la contraseña no coinciden, verifiquelas.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error IniciarSesionVM -> No se pudo consultar el Usuario | " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("Error " + ex.InnerException.Message);
            }
        }
    }
}
