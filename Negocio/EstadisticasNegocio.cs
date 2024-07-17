using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Negocio
{
    public class EstadisticasNegocio
    {
        AccesoDatos datos = new AccesoDatos();
        
        public List<int> ListarCantidadPedidosXMes()
        {
            List<int> pedidosXMes = new List<int>(new int[12]);
            try
            {
                datos.setConsulta("SELECT YEAR(Fecha) AS Año, MONTH(Fecha) AS Mes, COUNT(*) AS TotalPedidos FROM Pedidos GROUP BY YEAR(Fecha), MONTH(Fecha) ORDER BY Año, Mes;");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    int mes = datos.Lector.GetInt32(datos.Lector.GetOrdinal("Mes"));
                    int totalPedidos = datos.Lector.GetInt32(datos.Lector.GetOrdinal("TotalPedidos"));

                    pedidosXMes[mes - 1] = totalPedidos;
                }

                return pedidosXMes;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public List<int> ListarCantidadProductosXMes()
        {
            List<int> productosXMes = new List<int>(new int[12]);
            try
            {
                datos.setConsulta("SELECT YEAR(P.Fecha) AS Año, MONTH(P.Fecha) AS Mes, PR.Id_Producto, COUNT(PR.Id_Producto) AS CantidadVendida FROM Pedidos P INNER JOIN DETALLEPEDIDOS PR ON P.Id_Pedido = PR.Id_Pedido GROUP BY YEAR(P.Fecha), MONTH(P.Fecha), PR.Id_Producto ORDER BY Año, Mes;");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    int mes = datos.Lector.GetInt32(datos.Lector.GetOrdinal("Mes"));
                    int cantidadVendida = datos.Lector.GetInt32(datos.Lector.GetOrdinal("CantidadVendida"));

                    productosXMes[mes - 1] = cantidadVendida;
                }

                return productosXMes;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
