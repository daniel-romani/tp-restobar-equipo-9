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
                datos.setConsulta("SELECT ID_PRODUCTO, ID_TIPO, CANTIDAD, NOMBRE FROM STOCKCARTA");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ItemCarta item = new ItemCarta()
                    {
                        idProducto = (int)datos.Lector["ID_PRODUCTO"],
                        cantidad = (int)datos.Lector["CANTIDAD"],
                        nombre = (string)datos.Lector["NOMBRE"],
                       //Tengo que terminar de ver la validacion con imagenes y precio
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
