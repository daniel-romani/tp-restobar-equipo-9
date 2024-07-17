using Negocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;

namespace tp_restobar_equipo_9
{
    public partial class Estadisticas : System.Web.UI.Page
    {
        protected string xVentasMes = "Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre, Diciembre";
        protected List<int> pedidosXMes = new List<int>(new int[12]);
        protected int[] pedidosMes = new int[12];
        public List<int> productosXMes;
        EstadisticasNegocio negocio = new EstadisticasNegocio();
        protected string pxm = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            pedidosMes = negocio.ListarCantidadPedidosXMes().ToArray();
            foreach (int i in pedidosMes)
            {
                pxm.Concat(i.ToString());

            }
        }
        [WebMethod]
        public static List<int> cargarVentasXMes()
        {
            EstadisticasNegocio negocio = new EstadisticasNegocio();
            List<int> pedidos = negocio.ListarCantidadPedidosXMes();
            return pedidos;
        }
        [WebMethod]
        public static List<int> cargarProductosXMes()
        {
            EstadisticasNegocio negocio = new EstadisticasNegocio();
            List<int> productos = negocio.ListarCantidadProductosXMes();
            return productos;
        }
    }
}