using Modelo;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web;
using tp_restobar_equipo_9.Modelo;
using System.Web.UI;

namespace tp_restobar_equipo_9
{
    public partial class Configuracion : System.Web.UI.Page
    {
        Resto restaurant = new Resto();
        public Usuario usuario_actual;
        protected List<Mesa> mesas = new List<Mesa>();
        protected List<Mesa> mesasConMesero = new List<Mesa>();

        protected void Page_Load(object sender, EventArgs e)
        {
            RestoConexion restoConexion = new RestoConexion();
            restaurant = restoConexion.Listar();
            Session["Resto"] = restaurant;

            usuario_actual = (Usuario)Session["Usuario"];
            Session["Usuario"] = usuario_actual;

            mesas = ObtenerMesas();
        }

        private List<Mesa> ObtenerMesas()
        {
            MeseroNegocio meseroConexion = new MeseroNegocio();

            foreach (Mesa _mesas in restaurant.Mesas)
            {
                if (_mesas.Id_Mesero != -1)
                {
                    Mesero mesero = meseroConexion.ObtenerMeseroPorId(_mesas.Id_Mesero);
                    _mesas.MeseroAsignado = mesero;
                }
                mesasConMesero.Add(_mesas);
            }
            return mesasConMesero;   
        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            bool dniValido = true;

            // Obtener valores del modal
            int idMesa = int.Parse(hiddenFieldMesaId.Value);
            try
            {
                if (!Validaciones.EsNumero(txtDniMesero.Text))
                {
                    lblErrorDniMesero.Visible = true;
                    dniValido = false;
                }

                if (dniValido)
                {
                    MeseroNegocio meseroConexion = new MeseroNegocio();
                    int idMesero = meseroConexion.BuscarIdMeseroPorDni(txtDniMesero.Text);

                    MesaNegocio mesaConexion = new MesaNegocio();
                    mesaConexion.AsignarMesero(idMesa, idMesero);

                    ScriptManager.RegisterStartupScript(this, GetType(), "refresh", "window.location = window.location.href;", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Datos inexistentes o invalidos. Intente otra vez');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public static bool BajaAsignacion(int idMesa)
        {
            Resto restaurant = (Resto)HttpContext.Current.Session["Resto"];
            MesaNegocio mesaConexion = new MesaNegocio();

            foreach (Mesa _mesa in restaurant.Mesas)
            {
                if (_mesa.Id_Mesa == idMesa)
                {
                    mesaConexion.QuitarMeseroMesa(idMesa);
                    return true;
                }
            }
            return false;
        }
    }
}