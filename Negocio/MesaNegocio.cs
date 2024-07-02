using Modelo;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class MesaNegocio
    {
        public List<Mesa> Listar()
        {
            List<Mesa> lista = new List<Mesa>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT ID_MESA, ID_MESERO, ID_ADMIN, CAPACIDAD, COMENSALES_SENTADOS, RESERVADO, ESTADO FROM MESAS M WHERE ESTADO != 0");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Mesa _mesa = new Mesa
                    {
                        Id_Mesa = (int)datos.Lector["ID_MESA"],

                        Id_Mesero = datos.Lector["ID_MESERO"] is DBNull ? 0 : (int)datos.Lector["ID_MESERO"], // Asigna 0 u otro valor por defecto si es DBNull

                        Id_Admin = (int)datos.Lector["ID_ADMIN"],

                        Capacidad = (int)datos.Lector["CAPACIDAD"],

                        ComensalesSentados = (int)datos.Lector["COMENSALES_SENTADOS"],

                        Reservado = (bool)datos.Lector["RESERVADO"],

                        Estado = (bool)datos.Lector["ESTADO"]
                    };

                    lista.Add(_mesa);
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

        public void InsertarMesa(Mesa _mesa)
        {
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.setConsulta("INSERT INTO MESAS (ID_MESA, ID_ADMIN, CAPACIDAD, COMENSALES_SENTADOS) VALUES(@IDMESA, @IDADMIN, @CAP, @COMENSALESSENTADOS)");
                //datos.setParametro("@IDMESERO", _mesa.Id_Mesero);
                datos.setParametro("@IDMESA", _mesa.Id_Mesa);
                datos.setParametro("@IDADMIN", _mesa.Id_Admin);
                datos.setParametro("@CAP", _mesa.Capacidad);
                datos.setParametro("@COMENSALESSENTADOS", _mesa.ComensalesSentados);
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

        public bool ExisteMesa(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT COUNT(*) FROM MESAS WHERE ID_MESA = @IDMESA");
                datos.setParametro("@IDMESA", idMesa);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int count = Convert.ToInt32(datos.Lector[0]);
                    return count > 0;
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

        public void BajaFisicaMesa(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("DELETE FROM MESAS WHERE ID_MESA = @IDMESA");
                datos.setParametro("@IDMESA", idMesa);
                datos.ejecutarLectura();
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

        public void BajaLogicaMesa(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE MESAS SET ESTADO = 0, RESERVADO = 0 WHERE ID_MESA = @IDMESA");
                datos.setParametro("@IDMESA", idMesa);
                datos.ejecutarLectura();
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

        public void ModificarComensalSentadoMesa(Mesa _mesa, string sumaResta)
        {
            AccesoDatos datos = new AccesoDatos();
            switch (sumaResta)
            {
                case "resta":
                    {
                        try
                        {
                            datos.setConsulta("UPDATE MESAS SET COMENSALES_SENTADOS = COMENSALES_SENTADOS-1 WHERE ID_MESA = @IDMESA");
                            datos.setParametro("@IDMESA", _mesa.Id_Mesa);
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
                        break;
                    }
                case "suma":
                {
                    try
                    {
                        datos.setConsulta("UPDATE MESAS SET COMENSALES_SENTADOS = COMENSALES_SENTADOS+1 WHERE ID_MESA = @IDMESA");
                        datos.setParametro("@IDMESA", _mesa.Id_Mesa);
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
                    break;
                }
                
            }
        }

        public void ResetearMesa(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE MESAS SET COMENSALES_SENTADOS = 0, RESERVADO = 0 WHERE ID_MESA = @IDMESA");
                datos.setParametro("@IDMESA", idMesa);
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

        public void ModificarMesa(Mesa _mesa)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE MESAS SET ID_MESA = @IDMESA, ID_ADMIN = @IDADMIN, CAPACIDAD = @CAP, ESTADO = @ESTADO WHERE ID_MESA = @IDMESA");
                datos.setParametro("@IDMESA", _mesa.Id_Mesa);
                datos.setParametro("@IDADMIN", _mesa.Id_Admin);
                datos.setParametro("@CAP", _mesa.Capacidad);
                datos.setParametro("@ESTADO", _mesa.Estado);
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

        public void QuitarMesero(int idMesero)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE MESAS SET ID_MESERO = NULL WHERE ID_MESERO = @IDMESERO");
                datos.setParametro("@IDMESERO", idMesero);
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

        public void Reservar(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE MESAS SET RESERVADO = 1 WHERE ID_MESA = @IDMESA");
                datos.setParametro("@IDMESA", idMesa);
                datos.ejecutarLectura();
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
