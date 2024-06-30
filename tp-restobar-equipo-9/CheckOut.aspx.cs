using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace tp_restobar_equipo_9
{
    public partial class CheckOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string mesaId = Request.QueryString["mesaId"];
                string nroComensales = Request.QueryString["nroComensales"];

                if (!string.IsNullOrEmpty(mesaId) && !string.IsNullOrEmpty(nroComensales))
                {
                    lblMesaId.Text = mesaId;
                    lblNroComensales.Text = nroComensales;

                    // Cargar la carta y los elementos seleccionables
                    CargarCarta();
                }
            }
        }

        protected void CargarCarta()
        {
            List<ItemCarta> carta = ObtenerCarta();
        }

        public List<ItemCarta> ObtenerCarta()
        {
            List<ItemCarta> carta = new List<ItemCarta>
            {

            };

            // Ordenar primero las bebidas y luego los platos
            return carta.OrderBy(item => item.Tipo).ToList();
        }
    }
}