using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tp_restobar_equipo_9.Modelo;

namespace tp_restobar_equipo_9
{
    public partial class MailContraseña : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string Contraseña = txtNuevaContraseña.Text;
            string Confirmacion = txtConfirmarContraseña.Text;
            int idUsuario = BuscarUsuarioPorDNI();

            if (idUsuario == -1)
            {
                lblMostrarDNIUsuario.Text = "No se encontraron resultados para el DNI ingresado.";
            }
            else if (Contraseña.Length != 0 && Contraseña != null && Contraseña.Equals(Confirmacion))
            {
                Cambiar_Contraseña(Contraseña, idUsuario);

                lblMostrarDNIUsuario.Text = "Contraseña cambiada correctamente. Vuelva al inicio para acceder al sistema.";
            }
            else
            {
                lblMostrarDNIUsuario.Text = "Las contraseñas no coinciden.";
            }
        }

        private void Cambiar_Contraseña(string Nueva_Contraseña, int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("UPDATE USUARIOS SET CONTRASENA = @CONTRASENIA WHERE ID_USUARIO = @IDUSUARIO");
                datos.setParametro("@CONTRASENIA", Nueva_Contraseña);
                datos.setParametro("@IDUSUARIO", idUsuario);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private int BuscarUsuarioPorDNI()
        {
            string dni = txtDNI.Text.Trim();

            AccesoDatos datos = new AccesoDatos();

            string query = "SELECT U.ID_USUARIO " +
               "FROM COMENSALES C " +
               "INNER JOIN USUARIOS U ON C.ID_USUARIO = U.ID_USUARIO " +
               "WHERE C.DNI = @dni " +
               "UNION ALL " +
               "SELECT U.ID_USUARIO " +
               "FROM MESEROS M " +
               "INNER JOIN USUARIOS U ON M.ID_USUARIO = U.ID_USUARIO " +
               "WHERE M.DNI = @dni " +
               "UNION ALL " +
               "SELECT U.ID_USUARIO " +
               "FROM ADMINISTRADOR A " +
               "INNER JOIN USUARIOS U ON A.ID_USUARIO = U.ID_USUARIO " +
               "WHERE A.DNI = @dni";

            datos.setConsulta(query);

            datos.setParametro("@dni", dni);

            try
            {
                datos.ejecutarLectura();

                if (datos.Lector.HasRows)
                {
                    datos.Lector.Read();

                    int idUsuario;

                    return idUsuario = int.Parse(datos.Lector["ID_USUARIO"].ToString());
                }
                return -1;
            }
            catch (Exception ex)
            {
                lblMostrarDNIUsuario.Text = "Error al ejecutar la consulta: " + ex.Message;
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}