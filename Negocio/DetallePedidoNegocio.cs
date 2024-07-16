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
                //datos.setConsulta("SELECT DETALLEPEDIDOS.Cantidad, DETALLEPEDIDOS.PrecioUnitario, STOCKCARTA.NOMBRE FROM DETALLEPEDIDOS INNER JOIN STOCKCARTA ON DETALLEPEDIDOS.Id_Producto = STOCKCARTA.ID_PRODUCTO where DETALLEPEDIDOS.Id_Pedido = @IdPedido");
                datos.setConsulta("SELECT STOCKCARTA.NOMBRE, SUM(DETALLEPEDIDOS.Cantidad) AS TotalCantidad, DETALLEPEDIDOS.PrecioUnitario FROM DETALLEPEDIDOS INNER JOIN STOCKCARTA ON DETALLEPEDIDOS.Id_Producto = STOCKCARTA.ID_PRODUCTO WHERE DETALLEPEDIDOS.Id_Pedido = @IdPedido AND ESTADO = 1 GROUP BY STOCKCARTA.NOMBRE, DETALLEPEDIDOS.PrecioUnitario;");
                datos.setParametro("@IdPedido", id);
                datos.ejecutarLectura();
                while(datos.Lector.Read())
                {
                    DetallePedido detallePedido = new DetallePedido()
                    {
                        Cantidad = (int)datos.Lector["TotalCantidad"],
                        PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"],
                        Nombre = (string)datos.Lector["NOMBRE"]
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

        public void AgregarDetalle(List<ItemCarta> item, int idMesa, int idAdmin, int idPedido)
        {
            try
            {
                foreach(ItemCarta producto in item)
                {
                    datos.setConsulta("INSERT INTO DETALLEPEDIDOS (Id_Producto, Id_Pedido, Cantidad, PrecioUnitario) VALUES(@IdProducto, @IdPedido, @Cantidad, @PrecioUnitario)");
                    datos.setParametro("@IdProducto", producto.IdProducto);
                    datos.setParametro("@IdPedido", idPedido);
                    datos.setParametro("@Cantidad", 1);
                    datos.setParametro("@PrecioUnitario", producto.Precio);
                    datos.ejecutarAccion();
                    datos.Comando.Parameters.Clear();
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

        public void BajarDetalles(int id_Pedido)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE DETALLEPEDIDOS SET ESTADO = 0 WHERE ID_PEDIDO = @IDPEDIDO");
                datos.setParametro("@IDPEDIDO", id_Pedido);
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
