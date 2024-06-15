using Negocio;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_restobar_equipo_9
{
    public partial class SeleccionOrden : System.Web.UI.Page
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
            // Suponiendo que tienes un método para obtener los elementos de la carta
            List<ItemCarta> carta = ObtenerCarta(); // Método que retorna la lista de items de la carta

            rptCarta.DataSource = carta;
            rptCarta.DataBind();
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