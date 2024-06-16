using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Negocio;
using tp_restobar_equipo_9.Modelo;


namespace tp_restobar_equipo_9
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        public Usuario Usuario_Actual;
        public Resto Restaurant = new Resto();
        protected void Page_Load(object sender, EventArgs e)
        {
            RestoConexion restoConexion = new RestoConexion();
            Restaurant = restoConexion.Listar();
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
                Btn_alta_mesero.Visible = true;
                Btn_alta_mesa.Visible = true;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx", false);
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

                        //Aca estoy teniendo problemas para enviar el mail, parecen haber varias cosas de por medio asi que por ahora queda ahi
                        //emailService.enviarCorreo();

                    }
                    else
                    {
                        //
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
            bool dniMosoValido = true, dniAdmValido = true, capValido = true;
            try
            {
                if (!Validaciones.EsNumero(txtDniMoso.Text))
                {
                    lblErrorDniMoso.Visible = true;
                    dniMosoValido = false;
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

                if (dniMosoValido && dniAdmValido && capValido)
                {
                    Mesero mesero = new Mesero();
                    Mesa _mesa = new Mesa();
                    Administrador adm = new Administrador();

                    //INSERTAR NUEVA MESA
                    MeseroNegocio meseroConexion = new MeseroNegocio();
                    MesaNegocio mesaConexion = new MesaNegocio();
                    AdministradorNegocio admConexion = new AdministradorNegocio();

                    mesero = meseroConexion.getMesero(txtDniMoso.Text);
                    adm = admConexion.getAdm(txtDniAdmin.Text);

                    if(mesero.Estado && adm.Estado)
                    {
                        _mesa.Id_Mesero = mesero.Id;
                        _mesa.Id_Admin = adm.Id;
                        _mesa.Capacidad = int.Parse(txtCapMesa.Text);
                        _mesa.ComensalesSentados = 0;
                        _mesa.Estado = true;

                        mesaConexion.InsertarMesa(_mesa);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Datos inexistentes o invalidos. Intente otra vez');", true);
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