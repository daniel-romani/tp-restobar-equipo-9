using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace tp_restobar_equipo_9
{
    public partial class PerfilUsuario : System.Web.UI.Page
    {
        public Usuario usuarioActual;

        protected void Page_Load(object sender, EventArgs e)
        {
            Cargar_Componentes();
        }

        private void Cargar_Componentes()
        {
            usuarioActual = (Usuario)Session["Usuario"];

            imgPerfil.ImageUrl = "https://cdn-icons-png.flaticon.com/512/5987/5987424.png";
        }

        protected void btn_CambiarImagen_Click(object sender, EventArgs e)
        {
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //oculta botones de defecto
            btnEditarDatos.Visible = false;
            btnCambioContraseña.Visible = false;
            btn_CambiarImagen.Visible = false;
            txtImagen.Visible = false;
            //lblnombreUsuario.Visible = false;
            lblUsername.Visible = false;
            lblnombreUsuario.Visible = true;

            //Habilitar TextBox para la edición
            Visibilidad_Texbox(true);

            // Oculta los Label originales
            Visibilidad_labels(false);

            // Mostrar botón de guardar
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Oculta el TextBox y botón de guardar
            OcultarControlesEdicion();
            // Redirecciona a la misma página para refrescar los datos
            Response.Redirect(Request.RawUrl);
        }

        public void Cargar_labels()
        {
            //Habria que ver utilidad de la funcion
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Ocultar_labels_Error();
            btnCancelar.Visible = false;
            OcultarControlesEdicion();
            Visibilidad_labels(true);
            Cargar_labels();
        }

        public void Visibilidad_labels(bool valor)
        {
            dniLbl.Visible = valor;
            nombrelbl.Visible = valor;
            apellidoLbl.Visible = valor;
            emailLbl.Visible = valor;
            telefonoLbl.Visible = valor;
            direccionLbl.Visible = valor;
            fechaNacimientoLbl.Visible = valor;
        }
        public void Visibilidad_Texbox(bool valor)
        {
            txtUsername.Visible = valor;
            txtDniEdit.Visible = valor;
            txtNombreEdit.Visible = valor;
            txtApellidoEdit.Visible = valor;
            txtMailEdit.Visible = valor;
            txtTelefonoEdit.Visible = valor;
            txtDireccionEdit.Visible = valor;
            txtFechaNacimientoEdit.Visible = valor;
        }

        private void OcultarControlesEdicion()
        {
            //ocultar TextBox y boton de guardar
            Visibilidad_Texbox(false);

            //Ocultar botón de guardar
            btnGuardar.Visible = false;
            btnEditarDatos.Visible = true;
            btnCambioContraseña.Visible = true;
        }

        public void Ocultar_labels_Error()
        {
            lblErrorDni.Visible = false;

            lblErrorTelefono.Visible = false;

            lblErrorNombre.Visible = false;

            lblErrorApellido.Visible = false;

            lblErrorMail.Visible = false;

        }
    }
}