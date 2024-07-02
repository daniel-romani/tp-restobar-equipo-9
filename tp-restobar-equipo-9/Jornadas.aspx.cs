using Modelo;
using Negocio;
using System;
using System.Globalization;
using System.Runtime.Remoting.Messaging;

namespace tp_restobar_equipo_9
{
    public partial class Jornadas : System.Web.UI.Page
    {
        Jornada jornada = new Jornada();
        JornadaNegocio negocio = new JornadaNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (!IsPostBack)
            {
                Fecha_Jornada.Text = DateTime.Now.ToString("yyyy/MM/dd");
                dgvJornada.DataSource = negocio.Listar();
                dgvJornada.DataBind();
              
            }
            if (Session["txtFecha"] != null)
            {
                Fecha_Jornada.Text = (string)Session["txtFecha"];
            }
            if (Session["txtHora_ini"] != null)
            {
                txtHora_ini.Text = (string)Session["txtHora_ini"];
            }
            


        }
       

        protected void btnHora_ini_Click(object sender, EventArgs e)
        {
            txtHora_ini.Text = DateTime.Now.ToString("HH:mm:ss");
            Session["txtFecha"] = Fecha_Jornada.Text;
            Session["txtHora_ini"] = txtHora_ini.Text;
            

           
        }

        protected void btnHora_fin_Click(object sender, EventArgs e)
        {

            txtHora_fin.Text = DateTime.Now.ToString("HH:mm:ss");
            
            jornada.fecha = Fecha_Jornada.Text; 
            jornada.hora_Ini = TimeSpan.Parse(txtHora_ini.Text);
            jornada.hora_Fin = TimeSpan.Parse(txtHora_fin.Text);

            negocio.InsertarJornada(jornada);
            Session.Remove("txtFecha");
            Session.Remove("txtHora_ini");
            Response.Redirect(Request.RawUrl);

        }

    }
   
}