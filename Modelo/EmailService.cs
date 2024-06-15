using System.Net;
using System;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using tp_restobar_equipo_9.Modelo;

namespace Modelo
{
    public class EmailService
    {
        SmtpClient ServerEmail = new SmtpClient();
        MailMessage Correo = new MailMessage();

        //NECESITAMOS HARCODEAR UN MAIL TEMPORAL PARA DARLE FUNCIONALIDAD
        private string miEmail = "Tobidanimaxi@Gmail.Com";
        private string miContraseña = "fpqxtloemcfldqso";
        private string miAlias = "EQUIPO 9";

        private MailMessage miCorreo;

        //aca vamos a cargar el array de archivos a enviar (pdf, jpg, etc) si se necesita en algun momento
        private void AdjuntarArchivos()
        {

        }
        //cambio de contraseña
        public void cuerpoCorreo(string correo)
        {
            miCorreo = new MailMessage
            {
                From = new MailAddress(miEmail, miAlias, System.Text.Encoding.UTF8)
            };
            miCorreo.To.Add(correo);
            miCorreo.Subject = ("Recuperar contaseña");
            miCorreo.IsBodyHtml = true;
            string enlaceRecuperarContraseña = "https://localhost:44348/MailContrase%C3%B1a.aspx";
            miCorreo.Body = "Hola! Usted solicitó recuperar su contraseña. Haga clic <a href='" + enlaceRecuperarContraseña + "'>aquí</a> para restablecerla.";
            miCorreo.Priority = MailPriority.High;
        }

        //Alta Mesero con Usuario mediante Administrador 
        public void cuerpoCorreo(Usuario usuarioNuevo, string correo)
        {
            miCorreo = new MailMessage
            {
                From = new MailAddress(miEmail, miAlias)
            };
            miCorreo.To.Add(correo);
            miCorreo.Subject = "Usuario dado de alta";
            miCorreo.IsBodyHtml = true;
            miCorreo.Body = $"Le brindamos la informacion sobre su usuario: <br/>Username: {usuarioNuevo.Username}<br/>Contraseña: {usuarioNuevo.Contraseña}<br/>Con estos datos usted puede logerse, si desea cambiar su nombre de usuario y contraseña puede hacerlo desde el Perfil.";
            miCorreo.Priority = MailPriority.High;
        }


        //Aca estoy teniendo problemas para enviar el mail, parecen haber varias cosas de por medio asi que por ahora queda ahi
        public void enviarCorreo()
        {
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(miEmail, miContraseña),
                    EnableSsl = true
                };
                ServicePointManager.ServerCertificateValidationCallback =
                    (s, certificate, chain, sslPolicyErrors) => true;

                smtp.Send(miCorreo);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar correo. Detalles: " + ex.Message, ex);
            }
        }
    }
}
