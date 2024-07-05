using Modelo;
using Negocio;
using System;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using System.Web.UI;

namespace tp_restobar_equipo_9
{
    public partial class Jornadas : System.Web.UI.Page
    {
        Jornada jornada = new Jornada();
        JornadaNegocio jornadaConexion = new JornadaNegocio();
        private Resto restaurant = new Resto();
        protected void Page_Load(object sender, EventArgs e)
        {
            RestoConexion restoConexion = new RestoConexion();
            restaurant = restoConexion.Listar();

            Session["Resto"] = restaurant;

            if (!IsPostBack)
            {
                Fecha_Jornada.Text = DateTime.Now.ToString("yyyy/MM/dd");
                dgvJornada.DataSource = jornadaConexion.Listar();
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
            //bool jorna = true;
            //Session["Jorna"] = jorna;
            txtHora_ini.Text = DateTime.Now.ToString("HH:mm:ss");
            Session["txtFecha"] = Fecha_Jornada.Text;
            Session["txtHora_ini"] = txtHora_ini.Text;

            jornada.fecha = Fecha_Jornada.Text;
            jornada.hora_Ini = jornada.hora_Ini = TimeSpan.Parse(txtHora_ini.Text);

            jornadaConexion.InsertarInicioJornada(jornada);
        }

        protected void btnHora_fin_Click(object sender, EventArgs e)
        {
            //bool jorna = false;
            //Session["Jorna"] = jorna;
            txtHora_fin.Text = DateTime.Now.ToString("HH:mm:ss");
            jornada.hora_Fin = TimeSpan.Parse(txtHora_fin.Text);

            int idjornada = jornadaConexion.BuscarJornadaActiva();

            jornadaConexion.InsertarFinJornada(jornada, idjornada);


            Session.Remove("txtFecha");
            Session.Remove("txtHora_ini");
            Response.Redirect(Request.RawUrl);
        }

    }
   
}