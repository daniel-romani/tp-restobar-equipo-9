using Modelo;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class JornadaNegocio
    {
        public List<Jornada> Listar()
        {
            List<Jornada> lista = new List<Jornada>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT ID_JORNADA, FECHA, HORA_INICIO, HORA_FIN, TERMINADO, ESTADO FROM JORNADAS WHERE ESTADO != 0");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    string date = DateConverter((DateTime)datos.Lector["FECHA"]);

                    Jornada _jornada = new Jornada
                    {
                        idJornada = (int)datos.Lector["ID_JORNADA"],

                        fecha = (string)datos.Lector["FECHA"],
                        
                        hora_Ini = (TimeSpan)datos.Lector["HORA_INICIO"],

                        hora_Fin = (TimeSpan)datos.Lector["HORA_FIN"],

                        terminado = (bool)datos.Lector["TERMINADO"],

                        estado = (bool)datos.Lector["ESTADO"]
                    };

                    lista.Add(_jornada);
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

        public void InsertarInicioJornada(Jornada _jornada)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("INSERT INTO JORNADAS (FECHA, HORA_INICIO) VALUES(@FECHA, @HORA_INI)");
                datos.setParametro("@FECHA", _jornada.fecha);
                datos.setParametro("@HORA_INI", _jornada.hora_Ini);
                datos.ejecutarAccion();
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

        public void InsertarFinJornada(Jornada _jornada, int idJornada)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE JORNADAS SET FECHA = @FECHA, HORA_FIN = @HORA_FIN, TERMINADO = 1 WHERE ID_JORNADA = @IDJORNADA");
                datos.setParametro("@FECHA", _jornada.fecha);
                datos.setParametro("@HORA_FIN", _jornada.hora_Fin);
                datos.setParametro("@IDJORNADA", idJornada);
                datos.ejecutarAccion();
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

        private string DateConverter(DateTime date)
        {
            var dateAndTime = date;
            int year = dateAndTime.Year;
            int month = dateAndTime.Month;
            int day = dateAndTime.Day;

            return string.Format("{0}/{1}/{2}", month, day, year);
        }


        public int BuscarJornadaActiva()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT ID_JORNADA FROM JORNADAS WHERE TERMINADO = 0 AND ESTADO = 1");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["ID_JORNADA"];
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
    }
}
