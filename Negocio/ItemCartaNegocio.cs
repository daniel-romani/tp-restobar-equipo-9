using Modelo;
using System;
using System.Collections.Generic;


namespace Negocio
{
    public class ItemCartaNegocio
    {
        public List<ItemCarta> Listar()
        {


            List<ItemCarta> lista = new List<ItemCarta>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_LISTAR_STOCK");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ItemCarta item = new ItemCarta()
                    {
                        IdProducto = (int)datos.Lector["ID_PRODUCTO"],
                        Nombre = (string)datos.Lector["NOMBRE_P"],
                        Tipo = (string)datos.Lector["NOMBRE_T"],
                        Cantidad = (int)datos.Lector["CANTIDAD"],
                        UrlImagen = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("URLIMAGEN")) ? "" : (string)datos.Lector["URLIMAGEN"],
                        Precio = (decimal)datos.Lector["PRECIO"],
                        Unidad = (string)datos.Lector["NOMBRE_U"]
                    };

                    lista.Add(item);
                }
                return lista;
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

        public void ActualizarItem(ItemCarta item)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                
                datos.setearProcedimiento("SP_ACTUALIZACION_ITEM");
                
                datos.setParametro("@ID_PRODUCTO", item.IdProducto);
                datos.setParametro("@NOMBREPRODUCTO", item.Nombre);

                switch (item.Tipo)
                {
                    case "BEBIDAS":
                        datos.setParametro("@ID_TIPO", 1);
                        break;
                    case "VERDURA":
                        datos.setParametro("@ID_TIPO", 2);
                        break;
                    case "CARNE":
                        datos.setParametro("@ID_TIPO", 3);
                        break;
                    default:
                        datos.setParametro("@ID_TIPO", 4); ;
                        break;
                }

                switch (item.Unidad)
                {
                    case "Lts":
                        datos.setParametro("@ID_UNIDAD", 1);
                        break;
                    case "KG":
                        datos.setParametro("@ID_UNIDAD", 2);
                        break;
                    case "UN":
                        datos.setParametro("@ID_UNIDAD", 3);
                        break;
                    default:
                        datos.setParametro("@ID_UNIDAD", 4); ;
                        break;
                }
                datos.setParametro("@CANTIDAD", item.Cantidad);
                datos.setParametro("@PRECIO", item.Precio);
                datos.setParametro("@URLIMAGEN", item.UrlImagen);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();

            }
           



        }

    }
}
