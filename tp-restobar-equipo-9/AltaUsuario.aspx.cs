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
                    TipoUsuario = "Mesero"
                };
                usuarioConexion.InsertarUsuarioEnBBDD(nuevo_usuario);

                Mesero Moso = new Mesero();
                Moso.Id_Usuario = usuarioConexion.Get_ID_Usuario(nuevo_usuario);
                Moso.Nombre = txtNombreEdit.Text;
                Moso.Dni = txtDniEdit.Text;
                Moso.Apellido = txtApellidoEdit.Text;
                Moso.Mail = txtMailEdit.Text;
                Moso.Telefono = txtTelefonoEdit.Text;
                Moso.Direccion = txtDireccionEdit.Text;
                Moso.Fecha_Nacimiento = DateTime.Parse(txtFechaNacimientoEdit.Text);

                MeseroNegocio pacienteConexion = new MeseroNegocio();
                pacienteConexion.InsertarMesero(Moso);

                Response.Redirect("Default.aspx");
            }
            else
            {
                lbl_Error_Registro.Visible = true;
            }
        }
    }
}