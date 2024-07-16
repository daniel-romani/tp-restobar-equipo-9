using Modelo;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using tp_restobar_equipo_9.Modelo;

namespace tp_restobar_equipo_9
{
    public partial class Mesas : System.Web.UI.Page
    {
        private Usuario usuario_actual;
        private Resto restaurant = new Resto();
        private Mesero mesero_actual = new Mesero();
        private List<ItemCarta> ProductosEnMesa;

        protected List<Mesa> mesas = new List<Mesa>();

        protected void Page_Load(object sender, EventArgs e)
        {
            RestoConexion restoConexion = new RestoConexion();
            ProductosEnMesa = new List<ItemCarta>();
            restaurant = restoConexion.Listar();
            repRepetidor.DataSource = restaurant.ItemCartas;
            repRepetidor.DataBind();
            Session["Resto"] = restaurant;

            usuario_actual = (Usuario)Session["Usuario"];
            Session["Usuario"] = usuario_actual;


            if (usuario_actual.TipoUsuario == "Mesero")
            {
                mesero_actual = new Mesero();
                mesero_actual = Cargar_Mesero_Resto(usuario_actual.Id);
            }

             mesas = ObtenerMesas();

        }

        private Mesero Cargar_Mesero_Resto(int IDUsuario)
        {
            foreach (Mesero Moso in restaurant.Meseros)
            {
                if (Moso.Id_Usuario == IDUsuario)
                {
                    return Moso;
                }
            }
            return new Mesero();
        }

        private List<Mesa> ObtenerMesas()
        {
            if (usuario_actual.TipoUsuario == "Mesero")
            {
                foreach (Mesa _mesas in restaurant.Mesas)
                {
                    if (mesero_actual.Id == _mesas.Id_Mesero)
                    {
                        mesas.Add(_mesas);
                    }
                }
            }
            else if (usuario_actual.TipoUsuario == "Administrador")
            {
                foreach (Mesa _mesas in restaurant.Mesas)
                {
                    mesas.Add(_mesas);
                }
            }
            return mesas;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public static bool DecreaseComensal(int Id_Mesa)
        {
            Resto restaurant = (Resto)HttpContext.Current.Session["Resto"];
            MesaNegocio mesaConexion = new MesaNegocio();

            foreach (Mesa _mesa in restaurant.Mesas)
            {
                if (Id_Mesa == _mesa.Id_Mesa && _mesa.ComensalesSentados > 0)
                {
                    mesaConexion.ModificarComensalSentadoMesa(_mesa, "resta");
                    return true;
                }
            }
            return false;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public static bool IncreaseComensal(int Id_Mesa)
        {
            Resto restaurant = (Resto)HttpContext.Current.Session["Resto"];
            MesaNegocio mesaConexion = new MesaNegocio();

            foreach (Mesa _mesa in restaurant.Mesas)
            {
                if (Id_Mesa == _mesa.Id_Mesa && _mesa.ComensalesSentados < _mesa.Capacidad)
                {
                    mesaConexion.ModificarComensalSentadoMesa(_mesa, "suma");
                    return true;
                }
            }
            return false;
        }

        protected void btn_AgregarItem_Click(object sender, EventArgs e)
        {
            PedidosNegocio pedido = new PedidosNegocio();
            DetallePedidoNegocio detalleConexion = new DetallePedidoNegocio();
            int idProducto = int.Parse(((Button)sender).CommandArgument);
            int idMesa = int.Parse(hiddenFieldMesaId.Value);
            int idAdmin = int.Parse(hiddenFieldAdminId.Value);
            int idPedido = pedido.ObtenerPedidoXMesa(idMesa);
            foreach (var item in restaurant.ItemCartas)
            {
                if (item.IdProducto == idProducto && ItemDisponible(item))
                    ProductosEnMesa.Add(item);
            }
            detalleConexion.AgregarDetalle(ProductosEnMesa, idMesa, idAdmin, idPedido);
        }

        private bool ItemDisponible(ItemCarta item)
        {
            if (item.Cantidad == 0)
                return false;
            return true;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public static bool AbrirPedido(Mesa mesa) 
        {
            try
            {
                DateTime fechaActual = DateTime.Now;
                PedidosNegocio pedidoConexion = new PedidosNegocio();
                if (!pedidoConexion.PedidoAbierto(mesa.Id_Mesa))
                {
                    Pedido pedido = new Pedido()
                    {
                        Id_Mesa = mesa.Id_Mesa,
                        Id_Admin = mesa.Id_Admin,
                        Id_Mesero = mesa.Id_Mesero,
                        Fecha = fechaActual,
                        Total = 0
                    };

                    pedidoConexion.AbrirPedido(pedido);

                }

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }

        }

        private void GuardarPedidoEnMesa(Pedido pedido)
        {
            foreach(var mesa in mesas) 
            {
                if(mesa.Id_Mesa == pedido.Id_Mesa)
                {
                    mesa.Pedido = pedido;

                }
            }
            Application["MesasActualizado"] = mesas;
        }

        protected void Btn_HacerPedidoConfirmar_Click(object sender, EventArgs e)
        {
            PedidosNegocio pedidosNegocio = new PedidosNegocio();
            Pedido pedido = pedidosNegocio.ObetenerPedidoPorIdDeMesa(int.Parse(hiddenFieldMesaId.Value));
            GuardarPedidoEnMesa(pedido);
        }

        protected void btn_Checkout_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hiddenFieldMesaId.Value))
            {
                ShowAlert("Debes inicializar la mesa antes de intentar hacer un checkout.");
                return;
            }
            mesas = Application["MesasActualizado"] as List<Mesa>;

            Mesa validarMesa = new Mesa();
            PedidosNegocio pedido = new PedidosNegocio();
            int nroComensales = 0;
            int idMesa = int.Parse(hiddenFieldMesaId.Value);
            int idPedido = pedido.ObtenerPedidoXMesa(idMesa);
            foreach(var mesa in mesas)
            {
                if (mesa.Id_Mesa == idMesa)
                {
                    nroComensales = mesa.ComensalesSentados;
                    validarMesa = mesa;
                }
            }
            if(nroComensales != 0 && validarMesa.Pedido != null)
            {
                Response.Redirect($"CheckOut.aspx?nroComensales={nroComensales}&idMesa={idMesa}&idPedido={idPedido}");
            }
            else if(nroComensales == 0)
            {
                ShowAlert("No se puede hacer checkout sin comensales sentados.");
            }
            else if(validarMesa.Pedido == null) 
            {
                ShowAlert("No se puede hacer checkout sin un pedido hecho.");
            }
                

        }

        private void ShowAlert(string mensaje)
        {
            string script = $"alert('{mensaje}');";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
        }
    }
}