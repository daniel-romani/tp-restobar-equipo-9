using Modelo;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using tp_restobar_equipo_9.Modelo;


namespace tp_restobar_equipo_9
{
    
    public partial class Site1 : System.Web.UI.MasterPage
    {   //Validacion de estado de la jornada
        public bool Jorna {  get; set; }
        public Usuario Usuario_Actual;
        public Resto Restaurant = new Resto();
        public JornadaNegocio Negocio = new JornadaNegocio();
        //Aca se establecen las pagians expetuadas de la validacion
        public List<string> exepciones = new List<string> { "Jornadas.aspx", "Default.aspx", "DefaultCambioContraseña.aspx","Home.aspx","Estadistica.aspx" };
      
        protected void Page_Load(object sender, EventArgs e)
        {
            //Aca se recupera la pagina desde la cual se genera el pageload
            string paginaActual= System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            RestoConexion restoConexion = new RestoConexion();
                Restaurant = restoConexion.Listar();

            if (/*!Negocio.CompararHoraInicio(DateTime.Now) &&*/ !exepciones.Contains(paginaActual, StringComparer.OrdinalIgnoreCase) && !Negocio.BuscarJornadaActiva())
            {
                Session["error"] = "No se ha comenzado la jornada";
                Response.Redirect("Error.aspx");
            }

            /*if (Session["Jorna"]==null)
            {
                Jorna = false; 
                Session["Jorna"] = false;
                
            //}else
            //{
            //    Jorna = (bool)Session["Jorna"];
            //}
           
            
            
            //se hace la comparacion y en caso de que no se cumplan las condiciones redirige a la pagina de error
            if (!Jorna && !exepciones.Contains(paginaActual, StringComparer.OrdinalIgnoreCase))
            {
                Session["error"] = "No se ha comenzado la jornada";
                Response.Redirect("Error.aspx");
            }*/

            if (!Seguridad.SesionActiva(Session["Usuario"]))
            {
                Session.Abandon();
                Response.Redirect("Default.aspx", false);
            }
            else
            {
                Usuario_Actual = (Usuario)Session["Usuario"];
                //litNombreUsuario.Text = Usuario_Actual.Nombre;

                if (Usuario_Actual.Imagen != "https://cdn-icons-png.flaticon.com/512/5987/5987424.png")
                    imgPerfil.ImageUrl = "~/Resources/" + Usuario_Actual.Imagen;
                else
                    imgPerfil.ImageUrl = "https://cdn-icons-png.flaticon.com/512/5987/5987424.png";
            }

            if (Usuario_Actual.TipoUsuario == "Administrador")
            {
                Btn_baja_comensal.Visible = true;
                Btn_modificacion_estado_comensal.Visible = true;
                Btn_alta_mesero.Visible = true;
                Btn_baja_mesero.Visible = true;
                Btn_modificacion_estado_mesero.Visible = true;
                Btn_alta_mesa.Visible = true;
                Btn_baja_mesa.Visible = true;
                Btn_modificacion_mesa.Visible = true;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx", false);
        }

        //-----MODAL BAJA COMENSAL---
        protected void Btn_Baja_Comensal_Click(object sender, EventArgs e)
        {
            string script = @"
                $(document).ready(function () {
                    $('#mod_BajaComensal').modal('show');
                });
            ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarModal", script, true);
        }

        //--Logica
        protected void Btn_BajaLogicaComensalConfirmar_Click(object sender, EventArgs e)
        {
            bool dniValido = true;
            try
            {
                if (!Validaciones.EsNumero(txtDniComensal.Text))
                {
                    lblErrorDniComensal.Visible = true;
                    dniValido = false;
                }

                if (dniValido)
                {
                    UsuarioNegocio usuarioConexion = new UsuarioNegocio();
                    ComensalNegocio comensalConexion = new ComensalNegocio();

                    Comensal comensal = comensalConexion.getComensal(txtDniComensal.Text);

                    if (comensalConexion.ComensalExistente(comensal.Dni))
                    {
                        comensalConexion.BajaLogicaComensal(comensal.Id);
                        usuarioConexion.BajaLogicaUsuario(comensal.Id_Usuario);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Dni inexistente. Ingrese un dni valido.');", true);
                        return;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //--Fisica
        protected void Btn_BajaFisicaComensalConfirmar_Click(object sender, EventArgs e)
        {
            bool dniValido = true;
            try
            {
                if (!Validaciones.EsNumero(txtDniComensal.Text))
                {
                    lblErrorDniComensal.Visible = true;
                    dniValido = false;
                }

                if (dniValido)
                {
                    UsuarioNegocio usuarioConexion = new UsuarioNegocio();
                    ComensalNegocio comensalConexion = new ComensalNegocio();

                    Comensal comensal = comensalConexion.getComensal(txtDniComensal.Text);

                    if (comensalConexion.ComensalExistente(comensal.Dni))
                    {
                        comensalConexion.BajaFisicaComensal(comensal.Id);
                        usuarioConexion.BajaFisicaUsuario(comensal.Id_Usuario);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Dni inexistente. Ingrese un dni valido.');", true);
                        return;
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //-----MODAL MODIFICACION ESTADO COMENSAL---
        //--modifica el estado del usuario a su vez
        protected void Btn_Modificacion_Estado_Comensal_Click(object sender, EventArgs e)
        {
            string script = @"
                $(document).ready(function () {
                    $('#mod_ModificarEstadoComensal').modal('show');
                });
            ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarModal", script, true);
        }


        protected void Btn_ModificarEstadoComensalConfirmar_Click(object sender, EventArgs e)
        {
            bool dniValido = true;
            try
            {
                if (!Validaciones.EsNumero(txtDniComensal2.Text))
                {
                    lblErrorDniComensal2.Visible = true;
                    dniValido = false;
                }

                if (dniValido)
                {
                    UsuarioNegocio usuarioConexion = new UsuarioNegocio();
                    ComensalNegocio comensalConexion = new ComensalNegocio();

                    Comensal comensal = comensalConexion.getComensal(txtDniComensal2.Text);

                    if (comensalConexion.ComensalExistente(comensal.Dni))
                    {
                        if (!comensal.Estado)
                        {
                            comensalConexion.ModificarEstadoComensal(comensal.Id);
                            usuarioConexion.ModificarEstadoUsuario(comensal.Id_Usuario);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('El comensal seleccionado ya esta activo.');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Dni inexistente. Ingrese un dni valido.');", true);
                        return;
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //-----MODAL ALTA MESERO---
        protected void Btn_alta_mesero_Click(object sender, EventArgs e)
        {
            string script = @"
                $(document).ready(function () {
                    $('#mod_AltaMesero').modal('show');
                });
            ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarModal", script, true);
        }
        protected void Btn_AltaMeseroConfirmar_Click(object sender, EventArgs e)
        {
            bool nombreValido = true, apellidoValido = true, telefonoValido = true, emailValido = true, direccionValido = true, fechaNacimientoValido = true, dniValido = true;
            try
            {
                if (!Validaciones.EsNumero(txtDniMesero.Text))
                {
                    lblErrorDni.Visible = true;
                    dniValido = false;
                }
                if (!Validaciones.EsNumero(txtTelefonoMesero.Text))
                {
                    lblErrorTelefono.Visible = true;
                    telefonoValido = false;
                }
                if (Validaciones.ContieneNumeros(txtNombresMesero.Text))
                {
                    lblErrorNombre.Visible = true;
                    nombreValido = false;
                }
                if (Validaciones.ContieneNumeros(txtApellidosMesero.Text))
                {
                    lblErrorApellido.Visible = true;
                    apellidoValido = false;
                }
                if (!Validaciones.EsFormatoCorreoElectronico(txtEmailMesero.Text))
                {
                    lblErrorMail.Visible = true;
                    emailValido = false;
                }

                if (nombreValido && apellidoValido && telefonoValido && emailValido && direccionValido && fechaNacimientoValido && dniValido)
                {

                    Mesero mesero = new Mesero();

                    //INSERTAR NUEVO USUARIO CON MESERO 
                    MeseroNegocio meseroConexion = new MeseroNegocio();
                    UsuarioNegocio usuarioConexion = new UsuarioNegocio();

                    mesero.Dni = txtDniMesero.Text;
                    Usuario nuevo_usuario = new Usuario
                    {
                        Username = mesero.Dni,
                        Contraseña = mesero.Dni,
                        TipoUsuario = "Mesero"
                    };

                    usuarioConexion.InsertarUsuarioEnBBDD(nuevo_usuario);

                    mesero.Id_Usuario = usuarioConexion.buscarIdUsuarioPorDNI(mesero.Dni);

                    mesero.Nombre = txtNombresMesero.Text;
                    mesero.Apellido = txtApellidosMesero.Text;
                    mesero.Telefono = txtTelefonoMesero.Text;
                    mesero.Direccion = TxtDireccionMesero.Text;
                    mesero.Fecha_Nacimiento = DateTime.Parse(txtFechaNacimientoMesero.Text);
                    mesero.Mail = txtEmailMesero.Text;

                    EmailService emailService = new EmailService();
                    emailService.cuerpoCorreo(nuevo_usuario, txtEmailMesero.Text);

                    if (mesero.Id_Usuario != 0)
                    {
                        meseroConexion.InsertarMesero(mesero);
                        emailService.enviarCorreo();
                    }
                }


            }
            catch (Exception)
            {

                throw;
            }
        }


        //-----MODAL BAJA MESERO---
        protected void Btn_baja_mesero_Click(object sender, EventArgs e)
        {
            string script = @"
                $(document).ready(function () {
                    $('#mod_BajaMesero').modal('show');
                });
            ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarModal", script, true);
        }

        //--Logica
        protected void Btn_BajaLogicaMeseroConfirmar_Click(object sender, EventArgs e)
        {
            bool dniValido = true;
            try
            {
                if (!Validaciones.EsNumero(txtDniMesero2.Text))
                {
                    lblErrorDniMesero2.Visible = true;
                    dniValido = false;
                }

                if (dniValido)
                {
                    UsuarioNegocio usuarioConexion = new UsuarioNegocio();
                    MeseroNegocio meseroConexion = new MeseroNegocio();
                    MesaNegocio mesaConexion = new MesaNegocio();

                    Mesero mesero = meseroConexion.getMesero(txtDniMesero2.Text);

                    if (meseroConexion.MeseroExistente(mesero.Dni))
                    {
                        mesaConexion.QuitarMesero(mesero.Id);
                        meseroConexion.BajaLogicaMesero(mesero.Id);
                        usuarioConexion.BajaLogicaUsuario(mesero.Id_Usuario);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Dni inexistente. Ingrese un dni valido.');", true);
                        return;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //--Fisica
        protected void Btn_BajaFisicaMeseroConfirmar_Click(object sender, EventArgs e)
        {
            bool dniValido = true;
            try
            {
                if (!Validaciones.EsNumero(txtDniMesero2.Text))
                {
                    lblErrorDniMesero2.Visible = true;
                    dniValido = false;
                }

                if (dniValido)
                {
                    UsuarioNegocio usuarioConexion = new UsuarioNegocio();
                    MeseroNegocio meseroConexion = new MeseroNegocio();
                    MesaNegocio mesaConexion = new MesaNegocio();

                    Mesero mesero = meseroConexion.getMesero(txtDniMesero2.Text);

                    if (meseroConexion.MeseroExistente(mesero.Dni))
                    {
                        mesaConexion.QuitarMesero(mesero.Id);
                        meseroConexion.BajaFisicaMesero(mesero.Id);
                        usuarioConexion.BajaFisicaUsuario(mesero.Id_Usuario);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Dni inexistente. Ingrese un dni valido.');", true);
                        return;
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //-----MODAL MODIFICACION ESTADO MESERO---
        //--modifica el estado del usuario a su vez
        protected void Btn_modificar_estado_mesero_Click(object sender, EventArgs e)
        {
            string script = @"
                $(document).ready(function () {
                    $('#mod_ModificarEstadoMesero').modal('show');
                });
            ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarModal", script, true);
        }


        protected void Btn_ModificarEstadoMesereoConfirmar_Click(object sender, EventArgs e)
        {
            bool dniValido = true;
            try
            {
                if (!Validaciones.EsNumero(txtDniMesero3.Text))
                {
                    lblErrorDniMesero3.Visible = true;
                    dniValido = false;
                }

                if (dniValido)
                {
                    UsuarioNegocio usuarioConexion = new UsuarioNegocio();
                    MeseroNegocio meseroConexion = new MeseroNegocio();

                    Mesero mesero = meseroConexion.getMesero(txtDniMesero3.Text);

                    if(meseroConexion.MeseroExistente(mesero.Dni))
                    {
                        if (!mesero.Estado)
                        {
                            meseroConexion.ModificarEstadoMesero(mesero.Id);
                            usuarioConexion.ModificarEstadoUsuario(mesero.Id_Usuario);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('El mesero seleccionado ya esta activo.');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Dni inexistente. Ingrese un dni valido.');", true);
                        return;
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //-----MODAL ALTA MESA---
        protected void Btn_alta_mesa_Click(object sender, EventArgs e)
        {
            string script = @"
                $(document).ready(function () {
                    $('#mod_AltaMesa').modal('show');
                });
            ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarModal", script, true);
        }
        protected void Btn_AltaMesaConfirmar_Click(object sender, EventArgs e)
        {
            bool nroMesaValido = true, /*dniMosoValido = true,*/ dniAdmValido = true, capValido = true;
            try
            {
                //if (!Validaciones.EsNumero(txtDniMoso.Text))
                //{
                //    lblErrorDniMoso.Visible = true;
                //    dniMosoValido = false;
                //}
                if (!Validaciones.EsNumero(txtNroMesa.Text))
                {
                    lblErrorNumeroMesa.Visible = true;
                    nroMesaValido = false;
                }
                if (!Validaciones.EsNumero(txtDniAdmin.Text))
                {
                    lblErrorDniAdm.Visible = true;
                    dniAdmValido = false;
                }
                if (!Validaciones.EsNumero(txtCapMesa.Text))
                {
                    lblErrorCap.Visible = true;
                    capValido = false;
                }
                else if (int.Parse(txtCapMesa.Text) <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('El valor de capacidad para una mesa no puede ser menor o igual a 0.');", true);
                    return;
                }

                if (/*dniMosoValido &&*/ nroMesaValido && dniAdmValido && capValido)
                {
                    Mesero mesero = new Mesero();
                    Mesa _mesa = new Mesa();
                    Administrador adm = new Administrador();

                    //INSERTAR NUEVA MESA
                    MeseroNegocio meseroConexion = new MeseroNegocio();
                    MesaNegocio mesaConexion = new MesaNegocio();
                    AdministradorNegocio admConexion = new AdministradorNegocio();

                    //mesero = meseroConexion.getMesero(txtDniMoso.Text);
                    adm = admConexion.getAdm(txtDniAdmin.Text);

                    if(/*mesero.Estado &&*/ adm.Estado)
                    {
                        //_mesa.Id_Mesero = mesero.Id;
                        _mesa.Id_Mesa = int.Parse(txtNroMesa.Text);
                        _mesa.Id_Admin = adm.Id;
                        _mesa.Capacidad = int.Parse(txtCapMesa.Text);
                        _mesa.ComensalesSentados = 0;
                        _mesa.Estado = true;

                        // Primero, verifica si el ID de mesa ya existe
                        if (!mesaConexion.ExisteMesa(_mesa.Id_Mesa))
                        {
                            mesaConexion.InsertarMesa(_mesa);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Nro de mesa existente. Elija otro numero o elimine la mesa del sistema.');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Datos inexistentes o invalidos. Intente otra vez');", true);
                        return;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //-----MODAL BAJA MESA---
        protected void Btn_baja_mesa_Click(object sender, EventArgs e)
        {
            string script = @"
                $(document).ready(function () {
                    $('#mod_BajaMesa').modal('show');
                });
            ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarModal", script, true);
        }

        //--Fisica
        protected void Btn_BajaMesaFisicaConfirmar_Click(object sender, EventArgs e)
        {
            bool nroMesaValido = true;
            try
            {
                if (!Validaciones.EsNumero(txtNroMesa2.Text))
                {
                    lblErrorNumeroMesa2.Visible = true;
                    nroMesaValido = false;
                }

                if (nroMesaValido)
                {
                    MesaNegocio mesaConexion = new MesaNegocio();

                    // Primero, verifica si el ID de mesa ya existe
                    if (mesaConexion.ExisteMesa(int.Parse(txtNroMesa2.Text)))
                    {
                        mesaConexion.BajaFisicaMesa(int.Parse(txtNroMesa2.Text));
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Nro de mesa inexistente. Ingrese un numero valido.');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Datos inexistentes o invalidos. Intente otra vez');", true);
                    return;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //--Logica
        protected void Btn_BajaMesaLogicaConfirmar_Click(object sender, EventArgs e)
        {
            bool nroMesaValido = true;
            try
            {
                if (!Validaciones.EsNumero(txtNroMesa2.Text))
                {
                    lblErrorNumeroMesa2.Visible = true;
                    nroMesaValido = false;
                }

                if (nroMesaValido)
                {
                    MesaNegocio mesaConexion = new MesaNegocio();

                    // Primero, verifica si el ID de mesa ya existe
                    if (mesaConexion.ExisteMesa(int.Parse(txtNroMesa2.Text)))
                    {
                        mesaConexion.BajaLogicaMesa(int.Parse(txtNroMesa2.Text));
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Nro de mesa inexistente. Ingrese un numero valido.');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Datos inexistentes o invalidos. Intente otra vez');", true);
                    return;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //-----MODAL MODIFICACION MESA---
        protected void Btn_modificar_mesa_Click(object sender, EventArgs e)
        {
            string script = @"
                $(document).ready(function () {
                    $('#mod_ModificarMesa').modal('show');
                });
            ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarModal", script, true);
        }

        protected void Btn_ModificarMesaConfirmar_Click(object sender, EventArgs e)
        {
            bool nroMesaValido = true, estadoMesaValido = true, dniAdmValido = true, capValido = true;
            try
            {
                if (!Validaciones.EsNumero(txtNroMesa3.Text))
                {
                    lblErrorNumeroMesa3.Visible = true;
                    nroMesaValido = false;
                }
                if (!Validaciones.EsNumero(txtDniAdmin2.Text))
                {
                    lblErrorDniAdm2.Visible = true;
                    dniAdmValido = false;
                }
                if (!Validaciones.EsNumero(txtCapMesa2.Text))
                {
                    lblErrorCap2.Visible = true;
                    capValido = false;
                }
                else if (int.Parse(txtCapMesa2.Text) <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('El valor de capacidad para una mesa no puede ser menor o igual a 0.');", true);
                    return;
                }
                if (!Validaciones.EsNumero(txtEstado.Text))
                {
                    lblErrorEstado.Visible = true;
                    estadoMesaValido = false;
                }
                else if (int.Parse(txtEstado.Text) > 1 || int.Parse(txtEstado.Text) < 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('El valor de estado es unicamente 1 o 0. 1 = Activo, 0 = Inactivo.');", true);
                    return;
                }

                if (nroMesaValido && dniAdmValido && capValido && estadoMesaValido)
                {
                    Mesa _mesa = new Mesa();
                    Administrador adm = new Administrador();

                    MesaNegocio mesaConexion = new MesaNegocio();
                    AdministradorNegocio admConexion = new AdministradorNegocio();

                    adm = admConexion.getAdm(txtDniAdmin2.Text);

                    if (adm.Estado)
                    {
                        _mesa.Id_Mesa = int.Parse(txtNroMesa3.Text);
                        _mesa.Id_Admin = adm.Id;
                        _mesa.Capacidad = int.Parse(txtCapMesa2.Text);
                        _mesa.Estado = Convert.ToBoolean(int.Parse(txtEstado.Text));

                        if (mesaConexion.ExisteMesa(_mesa.Id_Mesa))
                        {
                            mesaConexion.ModificarMesa(_mesa);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Nro de mesa inexistente. Elija un numero de mesa valido.');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Datos inexistentes o invalidos. Intente otra vez');", true);
                        return;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}