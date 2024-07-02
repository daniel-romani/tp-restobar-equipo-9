using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ReservaNegocio
    {

        public List<Reserva> Listar()
        {
            List<Reserva> lista = new List<Reserva>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT R.ID_RESERVA, R.ID_COMENSAL, C.DNI, R.ID_MESA, R.CANTIDAD_COMENSALES, R.ESTADO FROM RESERVAS R INNER JOIN COMENSALES C ON R.ID_COMENSAL = C.ID_COMENSAL WHERE R.ESTADO = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Reserva reserva = new Reserva
                    {
                        Id = (int)datos.Lector["ID_RESERVA"],
                        Id_Comensal = (int)datos.Lector["ID_COMENSAL"],
                        DniComensal = (string)datos.Lector["DNI"],
                        Id_Mesa = (int)datos.Lector["ID_MESA"],
                        Cantidad_Comensales = (int)datos.Lector["CANTIDAD_COMENSALES"],
                        Estado = (bool)datos.Lector["ESTADO"]
                    };

                    lista.Add(reserva);
                }
                return lista;
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

        public int BuscarIDComensal(string dniComensal)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT ID_COMENSAL FROM COMENSALES WHERE DNI = @DNI");
                datos.setParametro("@DNI", dniComensal);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["ID_COMENSAL"];
                }
                else
                {
                    return -1;
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

        public void InsertarReserva(Reserva reserva)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("INSERT INTO RESERVAS (ID_COMENSAL, ID_MESA, CANTIDAD_COMENSALES, ESTADO) VALUES(@IDCOMENSAL, @IDMESA, @CANTIDADCOMENSALES, @ESTADO)");
                datos.setParametro("@IDCOMENSAL", reserva.Id_Comensal);
                datos.setParametro("@IDMESA", reserva.Id_Mesa);
                datos.setParametro("@CANTIDADCOMENSALES", reserva.Cantidad_Comensales);
                datos.setParametro("@ESTADO", reserva.Estado);
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

        public void CancelarReserva(int idReserva)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE RESERVAS SET ESTADO = 0 WHERE ID_RESERVA = @IDRESERVA");
                datos.setParametro("@IDRESERVA", idReserva);
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

        public void BajaLogicaReservaCheckOut(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE RESERVAS SET ESTADO = 0 WHERE ID_MESA = @IDMESA");
                datos.setParametro("@IDMESA", idMesa);
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

        public void BajaLogicaReserva(int idReserva)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE RESERVAS SET ESTADO = 0 WHERE ID_RESERVA = @IDRESERVA");
                datos.setParametro("@IDRESERVA", idReserva);
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
