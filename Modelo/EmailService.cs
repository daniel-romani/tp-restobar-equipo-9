using System;
using System.Net;
using System.Net.Mail;
using tp_restobar_equipo_9.Modelo;

namespace Modelo
{
    public class EmailService
    {
        private SmtpClient ServerEmail;
        private MailMessage Email;

        //Cree un mail temporal para poder acceder a la aplicacion web.
        private string miEmail = "tobidanimaxi@Gmail.Com";
        private string miContraseña = "programacion312";
        private string miAlias = "EQUIPO 9";

        public EmailService()
        {
            ServerEmail = new SmtpClient();
            ServerEmail.Credentials = new NetworkCredential("706d2468b3a44c", "21d8fb01e454ce");
            ServerEmail.EnableSsl = true;
            ServerEmail.Port = 2525;
            ServerEmail.Host = "sandbox.smtp.mailtrap.io";
        }

        public void cuerpoCorreo(Usuario usuarioNuevo, string correo)
        {
            Email = new MailMessage();
            Email.From = new MailAddress("noresponder@reasteasy.com");
            Email.To.Add(correo);
            Email.Subject = "Usuario dado de alta";
            Email.IsBodyHtml = true;
            Email.Body = $"Le brindamos la informacion sobre su usuario: <br/>Username: {usuarioNuevo.Username}<br/>Contraseña: {usuarioNuevo.Contraseña}<br/>Con estos datos usted puede logerse, si desea cambiar su nombre de usuario y contraseña puede hacerlo desde el Perfil.";
            Email.Priority = MailPriority.High;
        }

        //cambio de contraseña
        public void cuerpoCorreo(string correo)
        {
            Email = new MailMessage();
            Email.From = new MailAddress("noresponder@reasteasy.com");
            Email.To.Add(correo);
            Email.Subject = ("Recuperar contaseña");
            Email.IsBodyHtml = true;
            string enlaceRecuperarContraseña = "https://localhost:44364/MailContrase%C3%B1a.aspx";
            Email.Body = "Hola! Usted solicitó recuperar su contraseña. Haga clic <a href='" + enlaceRecuperarContraseña + "'>aquí</a> para restablecerla.";
            Email.Priority = MailPriority.High;
        }

        public void enviarCorreo()
        {
            try
            {
                ServerEmail.Send(Email);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
