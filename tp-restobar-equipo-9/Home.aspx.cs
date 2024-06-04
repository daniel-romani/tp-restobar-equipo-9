using System;

namespace tp_restobar_equipo_9
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Ver si hay una manera de no replicar este codigo en cada una de las paginas
            if (Session["usuario"] == null)
            {
                Session.Add("Error", "¿A donde vas pichón? Logueate, dale...");
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}