using Modelo;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class DetallePedidoNegocio
    {
        AccesoDatos datos = new AccesoDatos();

        public List<DetallePedido> ListarDetallePedidoXidPedido(int id)
        {
            List <DetallePedido> items = new List <DetallePedido>();

            try
            {
                datos.setearProcedimiento("select Id_DetallePedido, Id_Mesa, Id_Admin, Id_Producto, Id_Pedido, Cantidad, PrecioUnitario, Estado From DetallePedidos Where Id_Pedido = @IdPedido");
                datos.setParametro("@IdPedido", id);
                datos.ejecutarLectura();
                while(datos.Lector.Read())
                {
                    DetallePedido detallePedido = new DetallePedido()
                    {
                        Id_DetallePedido = (int)datos.Lector["Id_DetallePedido"],
                        Id_Mesa = (int)datos.Lector["Id_Mesa"],
                        Id_Admin = (int)datos.Lector["Id_Admin"],
                        Id_Producto = (int)datos.Lector["Id_Producto"],
                        Id_Pedidos = (int)datos.Lector["Id_Pedido"],
                        Cantidad = (int)datos.Lector["Cantidad"],
                        PrecioUnitario = (int)datos.Lector["PrecioUnitario"]
                    };

                    items.Add(detallePedido);
                }

                return items;
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

        public void AgregarDetalle(List<ItemCarta> item, Mesa mesa, int idPedido)
        {
            try
            {
                foreach(ItemCarta producto in item)
                {
                    datos.setConsulta("INSERT INTO DETALLEPEDIDOS (Id_Mesa, Id_Admin, Id_Producto, Id_Pedido, Cantidad, PrecioUnitario, Estado) VALUES(@IdMesa, @IdAdmin, @IdProducto, @IdPedido, @Cantidad, @PrecioUnitario)");
                    datos.setParametro("@IdMesa", mesa.Id_Mesa);
                    datos.setParametro("@IdAdmin", mesa.Id_Admin);
                    datos.setParametro("@IdProducto", producto.IdProducto);
                    datos.setParametro("@IdPedido", idPedido);
                    datos.setParametro("@Cantidad", producto.Cantidad);
                    datos.setParametro("@PrecioUnitario", producto.Precio);
                    datos.ejecutarAccion();
                }
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
