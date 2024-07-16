using Modelo;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class PedidosNegocio
    {
        public List<Pedido> Listar()
        {
            List<Pedido> pedidos = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("select Id_Pedido, Id_Mesa, Id_Admin, Id_Mesero, Total, Fecha, Estado From Pedidos Where Estado != 0");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    string date = DateConverter((DateTime)datos.Lector["Fecha"]);

                    Pedido pedido = new Pedido()
                    {
                        Id_Pedido = (int)datos.Lector["Id_Pedido"],
                        Id_Mesa = (int)datos.Lector["Id_Mesa"],
                        Id_Admin = (int)datos.Lector["Id_Admin"],
                        Id_Mesero = (int)datos.Lector["Id_Mesero"],
                        Total = (decimal)datos.Lector["Total"],
                        Fecha = (DateTime)datos.Lector["Fecha"],
                        Estado = (bool)datos.Lector["Estado"]
                    };

                    pedidos.Add(pedido);
                }
                return pedidos;
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
        public Pedido ObetenerPedidoPorId(int idPedido)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("select Id_Pedido, Id_Mesa, Id_Admin, Id_Mesero, Total, Fecha, Estado From Pedidos Where Id_Pedido = @IdPedido");
                datos.setParametro("@IdPedido", idPedido);
                datos.ejecutarLectura();
                datos.Lector.Read();
                string date = DateConverter((DateTime)datos.Lector["Fecha"]);
                Pedido pedido = new Pedido()
                {
                    Id_Pedido = (int)datos.Lector["Id_Pedido"],
                    Id_Mesa = (int)datos.Lector["Id_Mesa"],
                    Id_Admin = (int)datos.Lector["Id_Admin"],
                    Id_Mesero = (int)datos.Lector["Id_Mesero"],
                    Total = (decimal)datos.Lector["Total"],
                    Fecha = (DateTime)datos.Lector["Fecha"],
                    Estado = (bool)datos.Lector["Estado"]
                };

                return pedido;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Pedido ObetenerPedidoPorIdDeMesa(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("select Id_Pedido, Id_Mesa, Id_Admin, Id_Mesero, Total, Fecha, Estado From Pedidos Where Id_Mesa = @IdMesa");
                datos.setParametro("@IdMesa", idMesa);
                datos.ejecutarLectura();
                datos.Lector.Read();
                string date = DateConverter((DateTime)datos.Lector["Fecha"]);
                Pedido pedido = new Pedido()
                {
                    Id_Pedido = (int)datos.Lector["Id_Pedido"],
                    Id_Mesa = (int)datos.Lector["Id_Mesa"],
                    Id_Admin = (int)datos.Lector["Id_Admin"],
                    Id_Mesero = (int)datos.Lector["Id_Mesero"],
                    Total = (decimal)datos.Lector["Total"],
                    Fecha = (DateTime)datos.Lector["Fecha"],
                    Estado = (bool)datos.Lector["Estado"]
                };

                return pedido;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AbrirPedido(Pedido pedido) 
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("insert into Pedidos (Id_Mesa, Id_Admin, Id_Mesero, Total, Fecha) values(@Id_Mesa, @Id_Admin, @Id_Mesero, @Total, @Fecha)");
                datos.setParametro("@Id_Mesa", pedido.Id_Mesa);
                datos.setParametro("@Id_Admin", pedido.Id_Admin);
                datos.setParametro("@Id_Mesero", pedido.Id_Mesero);
                datos.setParametro("@Total", pedido.Total);
                datos.setParametro("@Fecha", pedido.Fecha);
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
        public void CerrarPedido(Pedido _pedido)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("insert into Pedidos (Id_Pedido, Id_Mesa, Id_Admin, Id_Mesero, Total, Fecha, Estado) values(@IdPedido, @IdMesa, @IdAdmin, @IdMesero, @Total, @Fecha, @Estado)");
                datos.setParametro("@Id_Pedido", _pedido.Id_Pedido);
                datos.setParametro("@Id_Mesa", _pedido.Id_Mesa);
                datos.setParametro("@Id_Admin", _pedido.Id_Admin);
                datos.setParametro("@Id_Mesero", _pedido.Id_Mesero);
                datos.setParametro("@Total", _pedido.Total);
                datos.setParametro("@Fecha", _pedido.Fecha);
                datos.setParametro("@Estado", 0);
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

        private string DateConverter(DateTime date)
        {
            var dateAndTime = date;
            int year = dateAndTime.Year;
            int month = dateAndTime.Month;
            int day = dateAndTime.Day;

            return string.Format("{0}/{1}/{2}", month, day, year);
        }

        public int ObtenerPedidoXMesa(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta($"select Id_Pedido from Pedidos where Id_Mesa = @IdMesa and Estado = 1");
                datos.setParametro("@IdMesa", idMesa);
                datos.ejecutarLectura();
                datos.Lector.Read();

                int Id_Pedido = (int)datos.Lector["Id_Pedido"];

                return Id_Pedido;

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

        public bool PedidoAbierto(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta($"select Id_Pedido, Estado from Pedidos where Id_Mesa = @IdMesa");
                datos.setParametro("@IdMesa", idMesa);
                datos.ejecutarLectura();
                if(datos.Lector.Read())
                {
                    bool Estado = (bool)datos.Lector["Estado"];
                    return Estado;
                }

                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
