using Modelo;
using Negocio;
using System;
using tp_restobar_equipo_9.Modelo;

namespace tp_restobar_equipo_9
{
    public partial class AltaUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool EsNumero(string valor)
        {
            // Intentar convertir el valor a un número
            return double.TryParse(valor, out _);
        }

        protected void btn_AceptarAltaUsuario_Click(object sender, EventArgs e)
        {
            bool usuarioValido = false;

            //Comprueba que los campos pasen las condiciones
            if (txtRegistrarUsuario != null && txtRegistrarUsuario.Text != "" && txtRegistrarContrasena.Text != "" && txtRegistrarContrasena.Text == txtRegistrarContrasena2.Text)
            {
                if (EsNumero(txtDniEdit.Text) && EsNumero(txtTelefonoEdit.Text) && DateTime.Parse(txtFechaNacimientoEdit.Text) <= DateTime.Now && DateTime.Parse(txtFechaNacimientoEdit.Text).Year >= 1900)
                {
                    usuarioValido = true;
                }
            }
            //Si las pasa, se crea un usuario nuevo y se lo ingresa en la BBDD
            if (usuarioValido)
            {
                UsuarioNegocio usuarioConexion = new UsuarioNegocio();

                Usuario nuevo_usuario = new Usuario
                {
                    Username = txtRegistrarUsuario.Text,
                    Contraseña = txtRegistrarContrasena.Text,
                    TipoUsuario = "Comensal"
                };
                usuarioConexion.InsertarUsuarioEnBBDD(nuevo_usuario);

                Comensal comensal = new Comensal();
                comensal.Id_Usuario = usuarioConexion.Get_ID_Usuario(nuevo_usuario);
                comensal.Nombre = txtNombreEdit.Text;
                comensal.Dni = txtDniEdit.Text;
                comensal.Apellido = txtApellidoEdit.Text;
                comensal.Mail = txtMailEdit.Text;
                comensal.Telefono = txtTelefonoEdit.Text;
                comensal.Direccion = txtDireccionEdit.Text;
                comensal.Fecha_Nacimiento = DateTime.Parse(txtFechaNacimientoEdit.Text);

                EmailService emailService = new EmailService();
                emailService.cuerpoCorreo(nuevo_usuario, txtMailEdit.Text);

                if (comensal.Id_Usuario != 0)
                {
                    ComensalNegocio comensalConexion = new ComensalNegocio();
                    comensalConexion.InsertarComensal(comensal);
                    emailService.enviarCorreo();
                }

                Response.Redirect("AltaUsuarioEndMail.aspx");
            }
            else
            {
                lbl_Error_Registro.Visible = true;
            }
        }
    }
}