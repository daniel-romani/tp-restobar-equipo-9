using Modelo;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_restobar_equipo_9
{
    public partial class Mesas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMesas();
            }
        }

        private void CargarMesas()
        {
            List<Mesa> mesas = ObtenerMesas(); // Método que retorna la lista de mesas

            foreach (Mesa mesa in mesas)
            {
                mesasContainer.Controls.Add(new LiteralControl($@"
                    <div class='col-md-3 mesa'>
                        <p><strong>Mesa N°:</strong> {mesa.Numero}</p>
                        <img src='Resources/mesa.png' alt='Mesa' />
                        <div class='mesa-info'>
                            <p><strong>Capacidad:</strong> {mesa.Capacidad} comensales</p>
                            <p><strong>Comensales Sentados:</strong> {mesa.ComensalesSentados}</p>
                            <a href='SeleccionOrden.aspx?mesaId={mesa.Id}&nroComensales={mesa.ComensalesSentados}' class='btn btn-primary'>Hacer Pedido</a>
                        </div>
                    </div>
                "));
            }
        }

        private List<Mesa> ObtenerMesas()
        {
            // Este método debe retornar la lista de mesas desde el origen de datos
            // Aquí agregamos un ejemplo estático
            return new List<Mesa>
            {
                new Mesa { Id = 1, Numero = 1, Capacidad = 4, ComensalesSentados = 2 },
                new Mesa { Id = 2, Numero = 2, Capacidad = 2, ComensalesSentados = 1 },

                // Agregar más mesas según sea necesario
            };
        }

        protected void GuardarMesa(object sender, EventArgs e)
        {

            int id = string.IsNullOrEmpty(txtMesaId.Text) ? 0 : int.Parse(txtMesaId.Text);
            int numero = int.Parse(txtNumeroMesa.Text);
            int capacidad = int.Parse(txtCapacidad.Text);

            // Lógica para guardar o actualizar la mesa en la base de datos
            if (id == 0)
            {
                // Insertar nueva mesa
            }
            else
            {
                // Actualizar mesa existente
            }

            // Recargar la lista de mesas
            CargarMesas();

            // Cerrar el modal
            ScriptManager.RegisterStartupScript(this, GetType(), "HideModal", "$('#abmModal').modal('hide');", true);
        }
    }
}