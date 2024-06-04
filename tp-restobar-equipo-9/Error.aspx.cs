using System;

namespace tp_restobar_equipo_9
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["error"] != null) 
            {
                lblMensaje.Text = Session["error"].ToString();
            }
        }
    }
}