using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_restobar_equipo_9
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        public bool contraseñaCambiada = false;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e) ///Faltarian confirmaciones, chequeos, conexiones, etc, etc.
        {
            String ContraseñaActual = txtContraseñaActual.Text;
            String Contraseña = txtNuevaContraseña.Text;
            String Confirmacion = txtConfirmarContraseña.Text;

            contraseñaCambiada = true;

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            // cerramos la sesion del usuario aca
            Response.Redirect("Default.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PerfilUsuario.aspx");
        }
    }
}