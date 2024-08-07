﻿using Modelo;
using Negocio;
using System;

namespace tp_restobar_equipo_9
{
    public partial class DefaultCambioContraseña : System.Web.UI.Page
    {
        public Resto resto;
        protected void Page_Load(object sender, EventArgs e)
        {
            resto = new Resto();
            RestoConexion restoConexion = new RestoConexion();
            resto = restoConexion.Listar();

        }

        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {
            //bool correoVerificado = false;
            EmailService emailService = new EmailService();
            emailService.cuerpoCorreo(txtIngresarMail.Text);

            try
            {
                bool correoEncontrado = VerificarCorreoEnBaseDeDatos(txtIngresarMail.Text);

                if (correoEncontrado)
                {
                    emailService.enviarCorreo();
                    lblMensaje.Text = "Correo enviado correctamente.";
                }
                else
                {
                    lblMensaje.Text = "No se encontró el correo en la base de datos.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al verificar el correo: " + ex.Message;
            }
        }

        private bool VerificarCorreoEnBaseDeDatos(string correo)
        {
            AccesoDatos datos = new AccesoDatos();

            // Query para verificar si el correo existe en alguna tabla de usuarios
            string query = "SELECT 1 FROM ( " +
                           "SELECT ID_USUARIO FROM COMENSALES WHERE MAIL = @correo " +
                           "UNION ALL " +
                           "SELECT ID_USUARIO FROM MESEROS WHERE MAIL = @correo " +
                           "UNION ALL " +
                           "SELECT ID_USUARIO FROM ADMINISTRADOR WHERE MAIL = @correo " +
                           ") AS Usuarios";

            datos.setConsulta(query);
            datos.setParametro("@correo", correo);

            try
            {
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    return true;
                }

                return false;
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


        protected void btnBuscarMailUsuario_Click(object sender, EventArgs e)
        {
            string dni = txtDNI.Text.Trim();

            AccesoDatos datos = new AccesoDatos();

            //esto se puede optimizar
            string query = "SELECT U.NOMBRE_USUARIO, C.MAIL " +
               "FROM COMENSALES C " +
               "INNER JOIN USUARIOS U ON C.ID_USUARIO = U.ID_USUARIO " +
               "WHERE C.DNI = @dni " +
               "UNION ALL " +
               "SELECT U.NOMBRE_USUARIO, M.MAIL " +
               "FROM MESEROS M " +
               "INNER JOIN USUARIOS U ON M.ID_USUARIO = U.ID_USUARIO " +
               "WHERE M.DNI = @dni " +
               "UNION ALL " +
               "SELECT U.NOMBRE_USUARIO, A.MAIL " +
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

                    string nombreUsuario = datos.Lector["NOMBRE_USUARIO"].ToString();
                    string email = datos.Lector["MAIL"].ToString();

                    lblMostrarMailUsuario.Text = "Su mail es: " + email;
                    lblMostrarNombreUsuario.Text = "Su nombre de usuario es: " + nombreUsuario;
                }
                else
                {
                    lblMostrarMailUsuario.Text = "No se encontraron resultados para el DNI ingresado.";

                }
            }
            catch (Exception ex)
            {
                lblMostrarMailUsuario.Text = "Error al ejecutar la consulta: " + ex.Message;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}