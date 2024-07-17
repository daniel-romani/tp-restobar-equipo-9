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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["estadoComanda"] is null && Session["comandaActual"] is null)
                {
                    string mesaId = Request.QueryString["idMesa"];
                    string nroComensales = Request.QueryString["nroComensales"];
                    string pedidoId = Request.QueryString["idPedido"];

                    if (!string.IsNullOrEmpty(mesaId) && !string.IsNullOrEmpty(nroComensales) && !string.IsNullOrEmpty(pedidoId))
                    {
                        Comanda comanda = new Comanda()
                        {
                            MesaId = mesaId,
                            NroComensales = nroComensales,
                            PedidoId = pedidoId
                        };
                        Session["estadoComanda"] = comanda;

                        lblMesaId.Text = mesaId;
                        hiddenMesaId.Value = mesaId;
                        lblNroComensales.Text = nroComensales;

                        // Cargar el pedido usando el ID del pedido
                        CargarPedido(int.Parse(pedidoId));
                    }
                }
                else
                {
                    RecuperarPedido();
                }
                CargarTotal();
            }
        }

        private void CargarTotal()
        {
            List<List<string>> tableData = Session["comandaActual"] as List<List<string>>;
            if (tableData != null)
            {
                List<string> sum = new List<string>();

                // Suponiendo que los valores que queremos sumar están en la cuarta columna (índice 3)
                foreach (List<string> rowData in tableData.Skip(1))
                {
                    if (rowData.Count == 4)
                    {
                        sum.Add(rowData[3]);
                    }
                }
                decimal total = sum.Sum(x => Convert.ToDecimal(x));

                // Mostrar la suma en una etiqueta (Label)
                lblPedidoAmount.Text = total.ToString("C");
                hiddenFieldTotal.Value = total.ToString();
            }
        }

        private void RecuperarPedido()
        {
            Comanda comandaExistente = Session["estadoComanda"] as Comanda;
            lblMesaId.Text = comandaExistente.MesaId;
            hiddenMesaId.Value = comandaExistente.MesaId;
            lblNroComensales.Text = comandaExistente.NroComensales;

            List<List<string>> tableData = Session["comandaActual"] as List<List<string>>;

            if (tableData != null)
            {
                // Limpiar la tabla existente
                orderItems.Rows.Clear();

                // Crear nuevas filas y agregar los datos recuperados
                foreach (List<string> rowData in tableData)
                {
                    TableRow newRow = new TableRow();
                    foreach (string cellText in rowData)
                    {
                        TableCell cell = new TableCell { Text = cellText };
                        newRow.Cells.Add(cell);
                    }
                    orderItems.Rows.Add(newRow);
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
                TableRow tableRow = new TableRow();
                tableRow.Cells.Add(new TableCell { Text = detalle.Cantidad.ToString() });
                tableRow.Cells.Add(new TableCell { Text = detalle.Nombre });
                tableRow.Cells.Add(new TableCell { Text = detalle.PrecioUnitario.ToString() });
                tableRow.Cells.Add(new TableCell { Text = (detalle.Cantidad * detalle.PrecioUnitario).ToString() });
                orderItems.Rows.Add(tableRow);

            }

            List<List<string>> tableData = new List<List<string>>();

            foreach (TableRow row in orderItems.Rows)
            {
                List<string> rowData = new List<string>();
                foreach (TableCell cell in row.Cells)
                {
                    rowData.Add(cell.Text);
                }
                tableData.Add(rowData);
            }

            Session["comandaActual"] = tableData;

        }

        protected void Btn_CerrarCuenta_Click(object sender, EventArgs e)
        {
            Resto restaurant = (Resto)HttpContext.Current.Session["Resto"];
            Comanda comandaExistente = Session["estadoComanda"] as Comanda;
            MesaNegocio mesaConexion = new MesaNegocio();
            ReservaNegocio reservaConexion = new ReservaNegocio();
            PedidosNegocio pedidoConexion = new PedidosNegocio();
            DetallePedidoNegocio detalleConexion = new DetallePedidoNegocio();
            pedidoActual = pedidoConexion.ObetenerPedidoPorId(int.Parse(comandaExistente.PedidoId));
            pedidoActual.Total = decimal.Parse(hiddenFieldTotal.Value);

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

            Session.Remove("estadoComanda");
            Session.Remove("comandaActual");

            string script = "<script type=\"text/javascript\">alert('Cuenta cerrada correctamente');window.location='Mesas.aspx';</script>";
            literalScript.Text = script;
        }
    }
}