using Modelo;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace tp_restobar_equipo_9
{
    public partial class CheckOut : System.Web.UI.Page
    {
        protected Pedido pedidoActual;


        //Posiblemente haya que cambiar algunas cosas de la implementacion pero en general es una plantilla adaptable.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string mesaId = Request.QueryString["mesaId"];
                string nroComensales = Request.QueryString["nroComensales"];
                //Aplicar cuando se implemente la base de datos
                //string pedidoId = Request.QueryString["pedidoId"];

                if (!string.IsNullOrEmpty(mesaId) && !string.IsNullOrEmpty(nroComensales) /*&& !string.IsNullOrEmpty(pedidoId)*/)
                {
                    lblMesaId.Text = mesaId;
                    hiddenMesaId.Value = mesaId;
                    lblNroComensales.Text = nroComensales;

                    // Cargar el pedido usando el ID del pedido
                    CargarPedido(/*int.Parse(pedidoId)*/);
                }
            }
        }

        protected void CargarPedido(/*int pedidoId*/)
        {
            //Placeholder hasta implementar bien la base de datos
            pedidoActual = new Pedido
            {
                Id_Pedido = 1,
                Id_Mesa = int.Parse(lblMesaId.Text),
                Items = new List<ItemCarta>
                {
                    new ItemCarta { Nombre = "Pizza Margarita", Precio = 12.50m, Cantidad = 2 },
                    new ItemCarta { Nombre = "Ensalada César", Precio = 8.00m, Cantidad = 1 },
                    new ItemCarta { Nombre = "Refresco", Precio = 2.50m, Cantidad = 3 }
                }
            };

            // Llenar la tabla con los elementos del pedido
            foreach (var item in pedidoActual.Items)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell { Text = item.Cantidad.ToString() });
                row.Cells.Add(new TableCell { Text = item.Nombre });
                row.Cells.Add(new TableCell { Text = item.Precio.ToString("C") });
                row.Cells.Add(new TableCell { Text = (item.Cantidad * item.Precio).ToString("C") });
                orderItems.Rows.Add(row);
            }
        }

        protected void Btn_CerrarCuenta_Click(object sender, EventArgs e)
        {
            Resto restaurant = (Resto)HttpContext.Current.Session["Resto"];
            MesaNegocio mesaConexion = new MesaNegocio();
            ReservaNegocio reservaConexion = new ReservaNegocio();
            //PedidosNegocio pedidoConexion = new PedidosNegocio();

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

            //foreach (Pedido _pedido in restaurant.Pedidos)
            //{
            //    if (pedidoId == pedidoId.Id_Pedido)
            //    {
            //        pedidoConexion.CerrarPedido(_pedido);
            //        break;
            //    }
            //}

            string script = "<script type=\"text/javascript\">alert('Cuenta cerrada correctamente');window.location='Mesas.aspx';</script>";
            literalScript.Text = script;
        }
    }
}