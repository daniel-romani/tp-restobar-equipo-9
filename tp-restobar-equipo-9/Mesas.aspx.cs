using Modelo;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using tp_restobar_equipo_9.Modelo;

namespace tp_restobar_equipo_9
{
    public partial class Mesas : System.Web.UI.Page
    {
        Resto restaurant = new Resto();
        public Usuario usuario_actual;
        Mesero mesero_actual = new Mesero();

        protected List<Mesa> mesas = new List<Mesa>();

        protected void Page_Load(object sender, EventArgs e)
        {
            RestoConexion restoConexion = new RestoConexion();
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

            //CargarMesas();
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
            // Este método debe retornar la lista de mesas desde el origen de datos
            // Aquí agregamos un ejemplo estático
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

        private void decreaseComensal(int Id_Mesa)
        {
            MesaNegocio mesaConexion = new MesaNegocio();

            foreach (Mesa _mesa in restaurant.Mesas)
            {
                if (Id_Mesa == _mesa.Id_Mesero && _mesa.ComensalesSentados <= 0)
                {
                    mesaConexion.ModificarComensalSentadoMesa(_mesa, "resta");
                    return;
                }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Mesa sin comensales. No se pueden quitar comensales inexistentes');", true);
                //}
            }
            throw new Exception("Mesa sin comensales. No se pueden quitar comensales inexistentes");
        }

        private void increaseComensal(int Id_Mesa)
        {
            MesaNegocio mesaConexion = new MesaNegocio();

            foreach (Mesa _mesa in restaurant.Mesas)
            {
                if (Id_Mesa == _mesa.Id_Mesero && _mesa.ComensalesSentados < _mesa.Capacidad)
                {
                    mesaConexion.ModificarComensalSentadoMesa(_mesa, "suma");
                    return;
                }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Mesa sin espacio');", true);
                //}
            }
            throw new Exception("Mesa sin espacio");
        }

        protected void Btn_hacer_pedido_Click(object sender, EventArgs e)
        {
            string script = @"
                $(document).ready(function () {
                    $('#mod_HacerPedido').modal('show');
                });
            ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarModal", script, true);
        }

        protected void Btn_HacerPedidoConfirmar_Click(object sender, EventArgs e) 
        {
            
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void btn_AgregarItem_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);

        }
    }
}