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
            Session["Resto"] = Restaurant;
            usuario_actual = (Usuario)Session["Usuario"];
            usuario_actual = Cargar_Datos_Usuario(usuario_actual);
            Session["Usuario"] = usuario_actual;
        }

        private Usuario Cargar_Datos_Usuario(Usuario usuario)
        {
            switch (usuario.TipoUsuario)
            {

                case "Mesero":
                    Mesero mesero_actual = Cargar_Mesero_Resto(usuario.Id);
                    if (mesero_actual.Id != -1)
                    {
                        usuario.Nombre = mesero_actual.Nombre;
                        usuario.Apellido = mesero_actual.Apellido;
                        usuario.Dni = int.Parse(mesero_actual.Dni);
                        usuario.Telefono = mesero_actual.Telefono;
                        usuario.Direccion = mesero_actual.Direccion;
                        usuario.Fecha_Nacimiento = mesero_actual.Fecha_Nacimiento;
                        usuario.Mail = mesero_actual.Mail;
                    }
                    break;

                case "Comensal":
                    Comensal comensal_actual = Cargar_Comensal_Resto(usuario.Id);
                    if (comensal_actual.Id != -1)
                    {
                        usuario.Nombre = comensal_actual.Nombre;
                        usuario.Apellido = comensal_actual.Apellido;
                        usuario.Dni = int.Parse(comensal_actual.Dni);
                        usuario.Telefono = comensal_actual.Telefono;
                        usuario.Direccion = comensal_actual.Direccion;
                        usuario.Fecha_Nacimiento = comensal_actual.Fecha_Nacimiento;
                        usuario.Mail = comensal_actual.Mail;
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

        private Comensal Cargar_Comensal_Resto(int IDUsuario)
        {
            foreach (Comensal comensal in Restaurant.Comensales)
            {
                if (comensal.Id_Usuario == IDUsuario)
                {
                    return comensal;
                }
            }
            return new Comensal();
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