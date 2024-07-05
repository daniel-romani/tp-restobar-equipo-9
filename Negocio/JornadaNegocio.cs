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
                datos.setConsulta("SELECT ID_JORNADA, FECHA, HORA_INICIO, HORA_FIN FROM JORNADAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    string date = DateConverter((DateTime)datos.Lector["FECHA"]);

                    Jornada _jornada = new Jornada
                    {
                        idJornada = (int)datos.Lector["ID_JORNADA"],

                        fecha = date,
                        
                        hora_Ini = (TimeSpan)datos.Lector["HORA_INICIO"],

                        hora_Fin = (TimeSpan)datos.Lector["HORA_FIN"],
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

        public void InsertarJornada(Jornada _jornada)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("INSERT INTO JORNADAS (FECHA, HORA_INICIO, HORA_FIN) VALUES(@FECHA, @HORA_INI, @HORA_FIN)");
                datos.setParametro("@FECHA", _jornada.fecha);
                datos.setParametro("@HORA_INI", _jornada.hora_Ini);
                datos.setParametro("@HORA_FIN", _jornada.hora_Fin);
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


        public void InsertarHoraInicio()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_HORA_INICIO");
                datos.setParametro("@HORAINICIO", DateTime.Now);
                datos.setParametro("@ESTADO", true);
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

        public bool ModificarEstado(DateTime fecha)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_MODIFICAR_ESTADO_HORA");
                datos.setParametro("@DATE_TIME", fecha);

                datos.ejecutarAccion();

                
                int filasModificadas = Convert.ToInt32(datos.Lector["FilasModificadas"]);

                if (filasModificadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
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
        public bool CompararHoraInicio(DateTime fecha)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_OBTENER_HORA_INICIO");
                datos.setParametro("@DATE_TIME", fecha);  

                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    DateTime fechacomp = datos.Lector.GetDateTime(datos.Lector.GetOrdinal("HORA_INI"));  
                    return true;
                }
                return false;
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
