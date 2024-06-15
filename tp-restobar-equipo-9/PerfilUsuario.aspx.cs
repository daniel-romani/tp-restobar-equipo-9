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
    public partial class PerfilUsuario : System.Web.UI.Page
    {
        Resto Restaurant;
        public Usuario Usuario_Actual;
        Mesero Mesero_actual;
        Administrador Administrador_actual;

        public Usuario usuarioActual;

        protected void Page_Load(object sender, EventArgs e)
        {
            Cargar_Componentes();
        }

        private void Cargar_Componentes()
        {

            RestoConexion RestoConexion = new RestoConexion();
            Restaurant = RestoConexion.Listar();
            Usuario_Actual = (Usuario)Session["Usuario"];

            Cargar_Datos_Usuario();
            Cargar_labels();

            if (Usuario_Actual.Imagen != "https://cdn-icons-png.flaticon.com/512/5987/5987424.png")
                imgPerfil.ImageUrl = "Resources/" + Usuario_Actual.Imagen;
            else
                imgPerfil.ImageUrl = "https://cdn-icons-png.flaticon.com/512/5987/5987424.png";
        }
        private void Cargar_Datos_Usuario()
        {
            switch (Usuario_Actual.TipoUsuario)
            {
                case "Mesero":
                    Mesero_actual = new Mesero();
                    Mesero_actual = Cargar_Mesero_Resto();
                    if (Mesero_actual.Id != -1)
                    {
                        Usuario_Actual.Nombre = Mesero_actual.Nombre;
                        Usuario_Actual.Apellido = Mesero_actual.Apellido;
                        Usuario_Actual.Dni = int.Parse(Mesero_actual.Dni);
                        Usuario_Actual.Telefono = Mesero_actual.Telefono;
                        Usuario_Actual.Direccion = Mesero_actual.Direccion;
                        Usuario_Actual.Fecha_Nacimiento = Mesero_actual.Fecha_Nacimiento;
                        Usuario_Actual.Mail = Mesero_actual.Mail;
                    }
                    break;

                case "Administrador":
                    Administrador_actual = new Administrador();
                    Administrador_actual = Cargar_Administracion_Resto();
                    if (Administrador_actual.Id != -1)
                    {

                        Usuario_Actual.Nombre = Administrador_actual.Nombre;
                        Usuario_Actual.Apellido = Administrador_actual.Apellido;
                        Usuario_Actual.Dni = int.Parse(Administrador_actual.Dni);
                        Usuario_Actual.Telefono = Administrador_actual.Telefono;
                        Usuario_Actual.Direccion = Administrador_actual.Direccion;
                        Usuario_Actual.Fecha_Nacimiento = Administrador_actual.Fecha_Nacimiento;
                        Usuario_Actual.Mail = Administrador_actual.Mail;
                    }
                    break;
                default:
                    Response.Write("Tipo de usuario desconocido: " + Usuario_Actual.TipoUsuario + "<br/>");
                    break;
            }
        }

        private Mesero Cargar_Mesero_Resto()
        {
            foreach (Mesero mesero in Restaurant.Meseros)
            {
                if (mesero.Id_Usuario == Usuario_Actual.Id)
                {
                    return mesero;
                }
            }
            return new Mesero();
        }
        private Administrador Cargar_Administracion_Resto()
        {
            foreach (Administrador administrador in Restaurant.Administracion)
            {
                if (administrador.Id_Usuario == Usuario_Actual.Id)
                {
                    return administrador;
                }

            }
            return new Administrador();
        }

        protected void btn_CambiarImagen_Click(object sender, EventArgs e)
        {
            if (!Imagen_Cambiada())
            {
                lbl_Error_Imagen.Visible = true;
            }
        }

        private bool Imagen_Cambiada()
        {

            string datoHorario = DateTime.Now.Hour.ToString() + DateTime.Now.Second + DateTime.Now.DayOfYear;

            try
            {
                //Escritura de imagen
                string ruta = Server.MapPath("./Resources/");

                txtImagen.PostedFile.SaveAs(ruta + "perfil-" + Usuario_Actual.Id + datoHorario + ".jpg");

                Usuario_Actual.Imagen = "perfil-" + Usuario_Actual.Id + datoHorario + ".jpg";

                UsuarioNegocio usuarioConexion = new UsuarioNegocio();

                usuarioConexion.EliminarImagen(Usuario_Actual.Id);

                usuarioConexion.actualizarImagen(Usuario_Actual);

                //Lectura de imagen
                Image img = (Image)Master.FindControl("imgPerfil");
                img.ImageUrl = "~/Resources/" + Usuario_Actual.Imagen;
                imgPerfil.ImageUrl = "~/Resources/" + Usuario_Actual.Imagen;

                return true;
            }
            catch (Exception ex)
            {
                Response.Write("Error en Imagen_Cambiada: " + ex.Message + "<br/>");
                return false;
                throw ex;
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //oculta botones de defecto
            btnEditarDatos.Visible = false;
            btnCambioContraseña.Visible = false;
            btn_CambiarImagen.Visible = false;
            txtImagen.Visible = false;
            //lblnombreUsuario.Visible = false;
            lblUsername.Visible = false;
            lblnombreUsuario.Visible = true;

            //Habilitar TextBox para la edición
            Visibilidad_Texbox(true);

            // Oculta los Label originales
            Visibilidad_labels(false);

            // Mostrar botón de guardar
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;

            txtUsername.Text = Usuario_Actual.Username;
            txtDniEdit.Text = Usuario_Actual.Dni.ToString();
            txtNombreEdit.Text = Usuario_Actual.Nombre;
            txtApellidoEdit.Text = Usuario_Actual.Apellido;
            txtMailEdit.Text = Usuario_Actual.Mail;
            txtTelefonoEdit.Text = Usuario_Actual.Telefono;
            txtDireccionEdit.Text = Usuario_Actual.Direccion;
            txtFechaNacimientoEdit.Text = Usuario_Actual.Fecha_Nacimiento.ToString("yyyy-MM-dd");

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool nombreValido = true, apellidoValido = true, telefonoValido = true, emailValido = true, direccionValido = true, fechaNacimientoValido = true, dniValido = true;


            if (!Validaciones.EsNumero(txtDniEdit.Text))
            {
                lblErrorDni.Visible = true;
                dniValido = false;
            }
            if (!Validaciones.EsNumero(txtTelefonoEdit.Text))
            {
                lblErrorTelefono.Visible = true;
                telefonoValido = false;
            }
            if (Validaciones.ContieneNumeros(txtNombreEdit.Text))
            {
                lblErrorNombre.Visible = true;
                nombreValido = false;
            }
            if (Validaciones.ContieneNumeros(txtApellidoEdit.Text))
            {
                lblErrorApellido.Visible = true;
                apellidoValido = false;
            }

            if (!Validaciones.EsFormatoCorreoElectronico(txtMailEdit.Text))
            {
                lblErrorMail.Visible = true;
                emailValido = false;
            }
            if (nombreValido && apellidoValido && telefonoValido && emailValido && direccionValido && fechaNacimientoValido && dniValido)
            {

                if (Usuario_Actual.Nombre == null)
                {
                    InsertarDatosEnBBDD();
                }
                else
                {
                    ActualizarDatosEnBBDD();
                }

            }
        }

        private void InsertarDatosEnBBDD()
        {
            string nuevoDni = txtDniEdit.Text;
            string nuevoNombre = txtNombreEdit.Text;
            string nuevoApellido = txtApellidoEdit.Text;
            string nuevoMail = txtMailEdit.Text;
            string nuevoTelefono = txtTelefonoEdit.Text;
            string nuevaDireccion = txtDireccionEdit.Text;
            DateTime nuevaFechaNacimiento = DateTime.Parse(txtFechaNacimientoEdit.Text);

            AccesoDatos datos = new AccesoDatos();
            try
            {
                switch (Usuario_Actual.TipoUsuario)
                {
                    case "Mesero":
                        datos.setConsulta("INSERT INTO MESEROS (ID_USUARIO, DNI, NOMBRE, APELLIDO, TELEFONO, DIRECCION, FECHA_NACIMIENTO, MAIL, ESTADO) VALUES (@IDUSUARIO, @NOMBRE, @APELLIDO, @TELEFONO, @DIRECCION, @FECHANACIMIENTO, @MAIL, 1)");
                        break;
                    case "Administrador":
                        datos.setConsulta("INSERT INTO ADMINISTRADOR (ID_USUARIO, DNI, NOMBRE, APELLIDO, TELEFONO, DIRECCION, FECHA_NACIMIENTO, MAIL, ESTADO) VALUES (@IDUSUARIO, @NOMBRE, @APELLIDO, @TELEFONO, @DIRECCION, @FECHANACIMIENTO, @MAIL, 1");
                        break;
                }
                datos.setParametro("@DNI", nuevoDni);
                datos.setParametro("@NOMBRE", nuevoNombre);
                datos.setParametro("@APELLIDO", nuevoApellido);
                datos.setParametro("@MAIL", nuevoMail);
                datos.setParametro("@TELEFONO", nuevoTelefono);
                datos.setParametro("@DIRECCION", nuevaDireccion);
                datos.setParametro("@FECHANACIMIENTO", nuevaFechaNacimiento);
                datos.setParametro("@IDUSUARIO", Usuario_Actual.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        private void ActualizarDatosEnBBDD()
        {
            // guardamos los valores editados de los TextBox
            string nuevoNombre = txtNombreEdit.Text;
            string nuevoApellido = txtApellidoEdit.Text;
            string nuevoDni = txtDniEdit.Text;
            string nuevoMail = txtMailEdit.Text;
            string nuevoTelefono = txtTelefonoEdit.Text;
            string nuevaDireccion = txtDireccionEdit.Text;
            DateTime nuevaFechaNacimiento = DateTime.Parse(txtFechaNacimientoEdit.Text);


            // Actualizar los datos del paciente
            switch (Usuario_Actual.TipoUsuario)
            {
                case "Mesero":
                    if (Usuario_Actual != null)
                    {
                        Mesero meseroActualizado = new Mesero
                        {
                            Id_Usuario = Usuario_Actual.Id,
                            Dni = nuevoDni,
                            Nombre = nuevoNombre,
                            Apellido = nuevoApellido,
                            Mail = nuevoMail,
                            Telefono = nuevoTelefono,
                            Direccion = nuevaDireccion,
                            Fecha_Nacimiento = nuevaFechaNacimiento
                        };

                        UsuarioNegocio conexionPaciente = new UsuarioNegocio();
                        conexionPaciente.ActualizarMesero(meseroActualizado);
                    }
                    break;

                case "Administrador":
                    if (Usuario_Actual != null)
                    {
                        Administrador administradorActualizado = new Administrador
                        {
                            Id_Usuario = Usuario_Actual.Id,
                            Dni = nuevoDni,
                            Nombre = nuevoNombre,
                            Apellido = nuevoApellido,
                            Mail = nuevoMail,
                            Telefono = nuevoTelefono,
                            Direccion = nuevaDireccion,
                            Fecha_Nacimiento = nuevaFechaNacimiento
                        };

                        UsuarioNegocio conexionAdministrador = new UsuarioNegocio();
                        conexionAdministrador.ActualizarAdministracion(administradorActualizado);
                    }
                    break;
            }
            // Oculta el TextBox y botón de guardar
            OcultarControlesEdicion();
            // Redirecciona a la misma página para refrescar los datos
            Response.Redirect(Request.RawUrl);
        }

        public void Cargar_labels()
        {
            lblUsername.Text = Usuario_Actual.Username;
            dniLbl.Text = Usuario_Actual.Dni.ToString();
            apellidoLbl.Text = Usuario_Actual.Apellido;
            nombrelbl.Text = Usuario_Actual.Nombre;
            emailLbl.Text = Usuario_Actual.Mail;
            telefonoLbl.Text = Usuario_Actual.Telefono;
            direccionLbl.Text = Usuario_Actual.Direccion;
            fechaNacimientoLbl.Text = Usuario_Actual.Fecha_Nacimiento.ToString("d/M/yyyy");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Ocultar_labels_Error();
            btnCancelar.Visible = false;
            OcultarControlesEdicion();
            Visibilidad_labels(true);
            Cargar_labels();
        }

        public void Visibilidad_labels(bool valor)
        {
            dniLbl.Visible = valor;
            nombrelbl.Visible = valor;
            apellidoLbl.Visible = valor;
            emailLbl.Visible = valor;
            telefonoLbl.Visible = valor;
            direccionLbl.Visible = valor;
            fechaNacimientoLbl.Visible = valor;
        }
        public void Visibilidad_Texbox(bool valor)
        {
            txtUsername.Visible = valor;
            txtDniEdit.Visible = valor;
            txtNombreEdit.Visible = valor;
            txtApellidoEdit.Visible = valor;
            txtMailEdit.Visible = valor;
            txtTelefonoEdit.Visible = valor;
            txtDireccionEdit.Visible = valor;
            txtFechaNacimientoEdit.Visible = valor;
        }

        private void OcultarControlesEdicion()
        {
            //ocultar TextBox y boton de guardar
            Visibilidad_Texbox(false);

            //Ocultar botón de guardar
            btnGuardar.Visible = false;
            btnEditarDatos.Visible = true;
            btnCambioContraseña.Visible = true;
        }

        public void Ocultar_labels_Error()
        {
            lblErrorDni.Visible = false;

            lblErrorTelefono.Visible = false;

            lblErrorNombre.Visible = false;

            lblErrorApellido.Visible = false;

            lblErrorMail.Visible = false;

        }
    }
}