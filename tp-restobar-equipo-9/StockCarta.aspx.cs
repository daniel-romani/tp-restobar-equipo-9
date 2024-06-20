using Modelo;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_restobar_equipo_9
{
    public partial class StockCarta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           Resto resto = new Resto();
            RestoConexion restoConexion = new RestoConexion();
            resto = restoConexion.Listar();
            repRepetidor.DataSource = resto.ItemCartas;
            repRepetidor.DataBind();
        }
    }
}