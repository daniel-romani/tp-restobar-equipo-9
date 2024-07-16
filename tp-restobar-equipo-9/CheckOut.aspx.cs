using Modelo;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

namespace tp_restobar_equipo_9
{
    public partial class CheckOut : System.Web.UI.Page
    {
        protected Pedido pedidoActual;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string mesaId = Request.QueryString["idMesa"];
                string nroComensales = Request.QueryString["nroComensales"];
                string pedidoId = Request.QueryString["idPedido"];

                if (!string.IsNullOrEmpty(mesaId) && !string.IsNullOrEmpty(nroComensales) && !string.IsNullOrEmpty(pedidoId))
                {
                    lblMesaId.Text = mesaId;
                    hiddenMesaId.Value = mesaId;
                    lblNroComensales.Text = nroComensales;

                    // Cargar el pedido usando el ID del pedido
                    CargarPedido(int.Parse(pedidoId));
                }
            }
        }

        protected void CargarPedido(int pedidoId)
        {
            DetallePedidoNegocio detalleNegocio = new DetallePedidoNegocio();
            List<DetallePedido> detallePedido = new List<DetallePedido>();
            PedidosNegocio pedidosNegocio = new PedidosNegocio();
            pedidoActual = pedidosNegocio.ObetenerPedidoPorId(pedidoId);
            detallePedido = detalleNegocio.ListarDetallePedidoXidPedido(pedidoId);


            foreach (var detalle in detallePedido)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell { Text = detalle.Cantidad.ToString() });
                row.Cells.Add(new TableCell { Text = detalle.Nombre });
                row.Cells.Add(new TableCell { Text = detalle.PrecioUnitario.ToString("C") });
                row.Cells.Add(new TableCell { Text = (detalle.Cantidad * detalle.PrecioUnitario).ToString("C") });
                orderItems.Rows.Add(row);
            }
        }

        protected void Btn_CerrarCuenta_Click(object sender, EventArgs e)
        {
            Resto restaurant = (Resto)HttpContext.Current.Session["Resto"];
            MesaNegocio mesaConexion = new MesaNegocio();
            ReservaNegocio reservaConexion = new ReservaNegocio();
            PedidosNegocio pedidoConexion = new PedidosNegocio();
            DetallePedidoNegocio detalleConexion = new DetallePedidoNegocio();

            int mesaId = int.Parse(hiddenMesaId.Value);

            foreach (Mesa _mesa in restaurant.Mesas)
            {
                if (mesaId == _mesa.Id_Mesa)
                {
                    mesaConexion.ResetearMesa(_mesa.Id_Mesa);
                    reservaConexion.BajaLogicaReservaCheckOut(_mesa.Id_Mesa);
                    break;
                }
            }

            foreach (Pedido _pedido in restaurant.Pedidos)
            {
                if (_pedido.Id_Pedido == pedidoActual.Id_Pedido)
                {
                    pedidoConexion.CerrarPedido(pedidoActual);
                    detalleConexion.BajarDetalles(pedidoActual.Id_Pedido);
                    break;
                }
            }

            string script = "<script type=\"text/javascript\">alert('Cuenta cerrada correctamente');window.location='Mesas.aspx';</script>";
            literalScript.Text = script;
        }
    }
}