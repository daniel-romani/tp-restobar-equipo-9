using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_restobar_equipo_9
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaUsuario.aspx");
        }
    }
}