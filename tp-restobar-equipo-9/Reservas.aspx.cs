using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Negocio;
using tp_restobar_equipo_9.Modelo;

namespace tp_restobar_equipo_9
{
    public partial class Reservas : System.Web.UI.Page
    {
        Resto restaurant = new Resto();
        public Usuario usuario_actual;
        protected List<Reserva> reservas = new List<Reserva>();
        protected List<Mesa> mesas = new List<Mesa>();
        Mesero mesero_actual = new Mesero();
        Comensal comensal_actual = new Comensal();

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
            else if (usuario_actual.TipoUsuario == "Comensal")
            {
                comensal_actual = new Comensal();
                comensal_actual = Cargar_Comensal_Resto(usuario_actual.Id);
            }

            mesas = ObtenerMesas();
            reservas = ObtenerReservas();
            reservas.Sort((x, y) => x.Id_Mesa.CompareTo(y.Id_Mesa));
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

        private Comensal Cargar_Comensal_Resto(int IDUsuario)
        {
            foreach (Comensal comensal in restaurant.Comensales)
            {
                if (comensal.Id_Usuario == IDUsuario)
                {
                    return comensal;
                }
            }
            return new Comensal();
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
            else if (usuario_actual.TipoUsuario == "Administrador" || usuario_actual.TipoUsuario == "Comensal")
            {
                foreach (Mesa _mesas in restaurant.Mesas)
                {
                    mesas.Add(_mesas);
                }
            }
            return mesas;
        }

        private List<Reserva> ObtenerReservas()
        {
            if (usuario_actual.TipoUsuario == "Mesero")
            {
                foreach (Mesa _mesa in restaurant.Mesas)
                {
                    if(_mesa.Id_Mesero == mesero_actual.Id)
                    {
                        foreach (Reserva _reserva in restaurant.Reservas)
                        {
                            if (_mesa.Id_Mesa == _reserva.Id_Mesa)
                            {
                                reservas.Add(_reserva);
                            }
                        }
                    }
                }
            }
            else if (usuario_actual.TipoUsuario == "Administrador")
            {
                foreach (Reserva _reserva in restaurant.Reservas)
                {
                    reservas.Add(_reserva);
                }
            }
            else if (usuario_actual.TipoUsuario == "Comensal")
            {
                foreach (Comensal _comensal in restaurant.Comensales)
                {
                    if (_comensal.Dni == comensal_actual.Dni)
                    {
                        foreach (Reserva _reserva in restaurant.Reservas)
                        {
                            if (_comensal.Id == _reserva.Id_Comensal)
                            {
                                reservas.Add(_reserva);
                            }
                        }
                    }
                }
            }
            return reservas;
        }
        
        protected void btnReservar_Click(object sender, EventArgs e)
        {
            bool dniValido = true, cantComensal = true;

            // Obtener valores del modal
            int idMesa = int.Parse(hiddenFieldMesaId.Value);
            int capacidadMesa = int.Parse(hiddenFieldMesaCap.Value);
            try
            {
                
                if (!Validaciones.EsNumero(txtDniComensal.Text))
                {
                    lblErrorDniComensal.Visible = true;
                    dniValido = false;
                }
                if (!Validaciones.EsNumero(txtCantComensal.Text))
                {
                    lblErrorComensal.Visible = true;
                    cantComensal = false;
                }

                if(dniValido && cantComensal)
                {
                    if ((int.Parse(txtCantComensal.Text)) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No se puede ingresar una cantidad de comensales igual a 0');", true);
                        return;
                    }
                    else if ((int.Parse(txtCantComensal.Text)) < 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No se puede ingresar una cantidad de comensales negativa');", true);
                        return;
                    }
                    else if((int.Parse(txtCantComensal.Text)) > capacidadMesa)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No se puede ingresar una cantidad de comensales mayor a la admitida por la mesa.');", true);
                        return;
                    }
                    else if ((int.Parse(txtCantComensal.Text)) <= capacidadMesa)
                    {
                        // Crear objeto de reserva
                        Reserva reserva = new Reserva
                        {
                            DniComensal = txtDniComensal.Text,
                            Id_Mesa = idMesa,
                            Cantidad_Comensales = int.Parse(txtCantComensal.Text)
                        };

                        // Insertar en la base de datos
                        ReservaNegocio reservaNegocio = new ReservaNegocio();
                        reserva.Id_Comensal = reservaNegocio.BuscarIDComensal(reserva.DniComensal);

                        if(reserva.Id_Comensal == -1)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No se encontro un comensal con ese dni registrado.');", true);
                            return;
                        }

                        reservaNegocio.InsertarReserva(reserva);

                        MesaNegocio mesaConexion = new MesaNegocio();
                        mesaConexion.Reservar(reserva.Id_Mesa);

                        ScriptManager.RegisterStartupScript(this, GetType(), "refresh", "window.location = window.location.href;", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ocurrio un error.');", true);
                        return;
                    }
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

        public static bool BajaReserva(int idReserva, int idMesa)
        {
            Resto restaurant = (Resto)HttpContext.Current.Session["Resto"];
            ReservaNegocio reservaConexion = new ReservaNegocio();
            MesaNegocio mesaConexion = new MesaNegocio();

            foreach (Reserva _reserva in restaurant.Reservas)
            {
                if (idReserva == _reserva.Id && _reserva.Estado)
                {
                    mesaConexion.ResetearMesa(idMesa);
                    reservaConexion.BajaLogicaReserva(idReserva);
                    return true;
                }
            }
            return false;
        }
    }
}