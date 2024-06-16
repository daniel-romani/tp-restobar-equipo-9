using Modelo;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        List<Mesa> mesas = new List<Mesa>();

        protected void Page_Load(object sender, EventArgs e)
        {
            RestoConexion restoConexion = new RestoConexion();
            restaurant = restoConexion.Listar();

            Session["Resto"] = restaurant;

            usuario_actual = (Usuario)Session["Usuario"];
            Session["Usuario"] = usuario_actual;


            if (usuario_actual.TipoUsuario == "Mesero")
            {
                mesero_actual = new Mesero();
                mesero_actual = Cargar_Mesero_Resto(usuario_actual.Id);
            }
            
            mesas = ObtenerMesas();

            CargarMesas();
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


        private void CargarMesas()
        {
            foreach (Mesa mesa in mesas)
            {
                mesasContainer.Controls.Add(new LiteralControl($@"
                    <div class='col-md-3 mesa'>
                        <div class=""mesa-info"">
                            <div class=""datos"">
                                <div class=""mesa-numero"">
                                    <p><strong>N°:</strong> {mesa.Id_Mesa}</p>
                                </div>
                                <div class=""mesa-imagen"">
                                    <img src='Resources/mesa.png' alt='Mesa' />
                                </div>
                                <div class=""mesa-capacidad {(mesa.Capacidad > mesa.ComensalesSentados ? "mesa-capacidad-verde" : "mesa-capacidad-rojo")}"">
                                    <i class='bx bx-user bx-sm' ></i>
                                    <p>{mesa.Capacidad}/{mesa.ComensalesSentados}</p>
                                </div>
                            </div>
                        </div>
                        <div class=""btn-container"">
                            <div class=""dropdown"">
                                <button class=""btn btn-secondary dropdown-toggle"" type=""button"" data-bs-toggle=""dropdown"" aria-expanded=""false"">
                                    <i class='bx bx-user-plus'></i> Sentar Comensales
                                </button>
                                <ul class=""dropdown-menu dropdown-menu-end"">
                                    <div ID=""botones"">
                                        <li><button class=""btn btn-secondary"" onclick=""decreaseComensal({mesa.Id_Mesa})"">-</button></li>
                                        <span>{mesa.ComensalesSentados}</span>
                                        <button class=""btn btn-secondary"" onclick=""increaseComensal({mesa.Id_Mesa})"">+</button>
                                    </div>
                                </ul>
                            </div>
                            <a href='SeleccionOrden.aspx?mesaId={mesa.Id_Mesa}&nroComensales={mesa.ComensalesSentados}' class='btn btn-info'><i class='bx bx-bookmark-alt-plus'></i>Hacer Pedido</a>
                            <a href='Checkout.aspx?mesaId={mesa.Id_Mesa}&nroComensales={mesa.ComensalesSentados}' class='btn btn-success' onclick=""return confirm('¿Está seguro de cerrar la mesa?');""><i class='bx bx-dollar-circle' ></i>CheckOut</a>
                        </div>                    
                    </div>
                "));
            }
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
    }
}