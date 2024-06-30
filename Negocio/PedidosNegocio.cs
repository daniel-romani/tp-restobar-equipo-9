using Modelo;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class PedidosNegocio
    {
        //Placeholder
        public List<Pedido> Listar()
        {
            return new List<Pedido>();
        }

        //Para uso cuando se implemente la base de datos
        //public List<Pedido> Listar()
        //{
        //    List<Pedido> pedido = new List<Pedido>();
        //    AccesoDatos datos = new AccesoDatos();
        //    try
        //    {
        //        datos.setearProcedimiento("");
        //        datos.ejecutarLectura();

        //        while (datos.Lector.Read())
        //        {
        //            Pedido item = new Pedido()
        //            {
        //                //Id_Pedido = (int)datos.Lector[""],
        //                //Id_Mesa = (string)datos.Lector[""],
        //                //Id_Admin = (string)datos.Lector[""],
        //                //Id_Mesero = (int)datos.Lector[""],
        //                //Items =
        //                //Estado =
        //            };

        //            pedido.Add(item);
        //        }
        //        return pedido;
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.ToString());
        //        throw ex;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}

        //public void CerrarPedido(Mesa _mesa)
        //{
        //    AccesoDatos datos = new AccesoDatos();
        //    try
        //    {
               
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}
    }
}
