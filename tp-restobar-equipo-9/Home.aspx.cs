using Modelo;
using Negocio;
using System;
using tp_restobar_equipo_9.Modelo;

namespace tp_restobar_equipo_9
{
    public partial class Home : System.Web.UI.Page
    {
        Resto Restaurant;
        Usuario usuario_actual;
        protected void Page_Load(object sender, EventArgs e)
        {
            Cargar_Componentes();
        }
        public void Cargar_Componentes()
        {
            RestoConexion restoConexion = new RestoConexion();
            Restaurant = restoConexion.Listar();
            Session["Clinica"] = Restaurant;
            usuario_actual = (Usuario)Session["Usuario"];
            usuario_actual = Cargar_Datos_Usuario(usuario_actual);
            Session["Usuario"] = usuario_actual;
        }

        private Usuario Cargar_Datos_Usuario(Usuario usuario)
        {
            switch (usuario.TipoUsuario)
            {

                case "Mesero":
                    Mesero paciente_actual = Cargar_Mesero_Resto(usuario.Id);
                    if (paciente_actual.Id != -1)
                    {
                        usuario.Nombre = paciente_actual.Nombre;
                        usuario.Apellido = paciente_actual.Apellido;
                        usuario.Dni = int.Parse(paciente_actual.Dni);
                        usuario.Telefono = paciente_actual.Telefono;
                        usuario.Direccion = paciente_actual.Direccion;
                        usuario.Fecha_Nacimiento = paciente_actual.Fecha_Nacimiento;
                        usuario.Mail = paciente_actual.Mail;
                    }
                    break;

                case "Administrador":
                    Administrador Administrador_actual = Cargar_Administracion_Resto(usuario.Id);
                    if (Administrador_actual.Id != -1)
                    {
                        usuario.Nombre = Administrador_actual.Nombre;
                        usuario.Apellido = Administrador_actual.Apellido;
                        usuario.Dni = int.Parse(Administrador_actual.Dni);
                        usuario.Telefono = Administrador_actual.Telefono;
                        usuario.Direccion = Administrador_actual.Direccion;
                        usuario.Fecha_Nacimiento = Administrador_actual.Fecha_Nacimiento;
                        usuario.Mail = Administrador_actual.Mail;
                    }
                    break;
            }
            return usuario;
        }
        private Mesero Cargar_Mesero_Resto(int IDUsuario)
        {
            foreach (Mesero Moso in Restaurant.Meseros)
            {
                if (Moso.Id_Usuario == IDUsuario)
                {
                    return Moso;
                }
            }
            return new Mesero();
        }

        private Administrador Cargar_Administracion_Resto(int IDUsuario)
        {
            foreach (Administrador administrador in Restaurant.Administracion)
            {
                if (administrador.Id_Usuario == IDUsuario)
                {
                    return administrador;
                }
            }
            return new Administrador();
        }
    }
}