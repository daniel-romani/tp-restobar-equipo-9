using Modelo;
using System;
using System.Collections.Generic;
using tp_restobar_equipo_9.Modelo;

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
                        Fecha = date,
                        Estado = (bool)datos.Lector["Estado"]
                    };

                    pedidos.Add(pedido);
                }
                return pedidos;
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
        public Pedido ObetenerPedidoPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("select Id_Pedido, Id_Mesa, Id_Admin, Id_Mesero, Total, Fecha, Estado From Pedidos Where Id_Pedido = @IdPedido");
                datos.setParametro("@IdPedido", id);
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
                    Fecha = date,
                    Estado = (bool)datos.Lector["Estado"]
                };

                return pedido;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CerrarPedido(Mesa _mesa)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("insert into Pedidos (Id_Pedido, Id_Mesa, Id_Admin, Id_Mesero, Total, Fecha, Estado) values(@IdPedido, @IdMesa, @IdAdmin, @IdMesero, @Total, @Fecha, @Estado)");
                datos.setParametro("@Id_Pedido", _mesa.Pedido.Id_Pedido);
                datos.setParametro("@Id_Mesa", _mesa.Id_Mesa);
                datos.setParametro("@Id_Admin", _mesa.Id_Admin);
                datos.setParametro("@Id_Mesero", _mesa.Id_Mesero);
                datos.setParametro("@Total", _mesa.Pedido.Total);
                datos.setParametro("@Fecha", _mesa.Pedido.Fecha);
                datos.setParametro("@Estado", _mesa.Estado);
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
    }
}
