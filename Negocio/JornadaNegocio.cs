using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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

                datos.ejecutarAccionYLeer();
                

                int filasModificadas = 0;
                if (datos.Lector != null && datos.Lector.Read())
                {
                    if (!datos.Lector.IsDBNull(datos.Lector.GetOrdinal("FilasModificadas")))
                    {
                        filasModificadas = Convert.ToInt32(datos.Lector["FilasModificadas"]);
                    }
                }

                return filasModificadas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el estado", ex);
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
        private string DateConverter(DateTime date)
        {
            var dateAndTime = date;
            int year = dateAndTime.Year;
            int month = dateAndTime.Month;
            int day = dateAndTime.Day;

            return string.Format("{0}/{1}/{2}", month, day, year);
        }



    }





    /*
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

                       fecha = date,

                       hora_Ini = (TimeSpan)datos.Lector["HORA_INICIO"],

                       hora_Fin = datos.Lector["HORA_FIN"] is DBNull ? TimeSpan.Zero : (TimeSpan)datos.Lector["HORA_FIN"], // Asigna -1 u otro valor por defecto si es DBNull,

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
        }*/


}
