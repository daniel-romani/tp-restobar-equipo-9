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

    }
}
