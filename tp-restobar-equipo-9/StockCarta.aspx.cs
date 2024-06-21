using Modelo;
using Negocio;
using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_restobar_equipo_9
{
    public partial class StockCarta : System.Web.UI.Page
    {
        public bool Estado { get; set; }
        Resto resto = new Resto();
        RestoConexion restoConexion = new RestoConexion();

        protected void Page_Load(object sender, EventArgs e)
        {
          Estado = false;
            resto = restoConexion.Listar();
            if (!IsPostBack)
            {

                repRepetidor.DataSource = resto.ItemCartas;
                repRepetidor.DataBind();
                
            }

            var tiposDistintos = resto.ItemCartas
                              .GroupBy(x => x.Unidad)
                              .Select(g => g.First())
                              .ToList();

            // Asignar los tipos distintos al DropDownList
            ddlUnidad.DataSource = tiposDistintos;
            ddlUnidad.DataTextField = "Unidad"; // Suponiendo que "Tipo" es el campo que deseas mostrar en el DropDownList
            ddlUnidad.DataBind();

              tiposDistintos = resto.ItemCartas
                             .GroupBy(x => x.Tipo)
                             .Select(g => g.First())
                             .ToList();

            
            ddlTipo.DataSource = tiposDistintos;
            ddlTipo.DataTextField = "Tipo";
            ddlTipo.DataBind();




        }



        protected void bttModificar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            PrecargarDatos(id);
            Estado = true;
        }

        protected void bttEliminar_Click(object sender, EventArgs e)
        {
            string id = ((Button)sender).CommandArgument;
        }

        protected void bttAgregarItem_Click(object sender, EventArgs e)
        {

        }
        public void PrecargarDatos(int id)
        { 
            ItemCarta item = new ItemCarta();
            item = resto.ItemCartas.Find(x => x.IdProducto == (id));
            TxtNombre.Text = item.Nombre;
            ddlTipo.Text = item.Tipo;
            ddlUnidad.Text= item.Unidad;
            TxtCantidad.Text = item.Cantidad.ToString();
            TxtPrecio.Text = item.Precio.ToString();
            TxtCargarImagen.Text = item.UrlImagen;
            

        
        }

        protected void bttAceptar_Click(object sender, EventArgs e)
        {
            Estado=false;
        }

        protected void bttEliminar_Click1(object sender, EventArgs e)
        {
            Estado = false;

        }
    }
}