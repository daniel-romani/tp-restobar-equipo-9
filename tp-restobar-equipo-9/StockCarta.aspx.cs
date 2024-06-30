using Modelo;
using Negocio;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace tp_restobar_equipo_9
{
    public partial class StockCarta : System.Web.UI.Page
    {
        public bool Estado { get; set; }
        public bool ConfirmarEliminacion { get; set; }
        Resto resto = new Resto();
        RestoConexion restoConexion = new RestoConexion();
        ItemCarta item = new ItemCarta();   

        protected void Page_Load(object sender, EventArgs e)
        {
          
            resto = restoConexion.Listar();
            if (!IsPostBack)
            {
                ConfirmarEliminacion = false;
                Estado = false;
                RecargarRepeter();
                
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

        public void RecargarRepeter()
        {
            Resto resti = new Resto();
            resti = restoConexion.Listar();
            repRepetidor.DataSource = resti.ItemCartas;
            repRepetidor.DataBind();
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
            Estado = true;
        }
        public void PrecargarDatos(int id)
        { 
            
            item = resto.ItemCartas.Find(x => x.IdProducto == (id));
            TxtIdProducto.Text = id.ToString();
            TxtNombre.Text = item.Nombre;
            ddlTipo.Text = item.Tipo;
            ddlUnidad.Text= item.Unidad;
            TxtCantidad.Text = item.Cantidad.ToString();
            TxtPrecio.Text = item.Precio.ToString();
            TxtCargarImagen.Text = item.UrlImagen;

            if(!(item.UrlImagen == ""))
            {
                ImgProducto.ImageUrl = item.UrlImagen;
            }
            else
            {
                ImgProducto.ImageUrl = "https://img2.freepnges.com/20180715/gez/aavg9iox9.webp";
            }

            




        }

        protected void bttAceptar_Click(object sender, EventArgs e)
        {
            ItemCartaNegocio itemCarta = new ItemCartaNegocio();
            try
            {
                item.IdProducto = string.IsNullOrEmpty(TxtIdProducto.Text) ? 0 : int.Parse(TxtIdProducto.Text);
                item.Nombre = TxtNombre.Text;
                item.Tipo = ddlTipo.Text;
                item.Unidad = ddlUnidad.Text;
                item.Cantidad = int.Parse(TxtCantidad.Text);
                item.Precio = decimal.Parse(TxtPrecio.Text);
                item.UrlImagen = TxtCargarImagen.Text;

                if (!(item.UrlImagen == ""))
                {
                    item.UrlImagen = ImgProducto.ImageUrl;
                }
                else
                {
                    item.UrlImagen = "https://img2.freepnges.com/20180715/gez/aavg9iox9.webp";
                }
                if(!(string.IsNullOrEmpty(TxtIdProducto.Text)))
                { 
                    itemCarta.ActualizarItem(item);
                }else
                {
                    itemCarta.AgregarItem(item);
                }
                
            }
            catch (Exception ex)
            {

                Session.Add("Error", ex);
            }
            finally
            {
                RecargarRepeter();
                Estado = false; 
            }
           
        }

        protected void bttEliminar_Click1(object sender, EventArgs e)
        {
            Estado = false;

        }

        protected void TxtCargarImagen_TextChanged(object sender, EventArgs e)
        {
            ImgProducto.ImageUrl = TxtCargarImagen.Text;
            Estado = true;

        }

        protected void bttConfirmar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            ItemCartaNegocio itemCarta = new ItemCartaNegocio();
            itemCarta.EliminarItem(id);
            ConfirmarEliminacion = false;
            RecargarRepeter();
        }

        protected void bttEliminar_Click2(object sender, EventArgs e)
        {
            Estado = false;
            ConfirmarEliminacion = true;
        }
    }
}