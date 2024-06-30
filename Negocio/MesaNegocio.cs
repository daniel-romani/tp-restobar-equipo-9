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
                datos.setConsulta("SELECT ID_MESA, ID_MESERO, ID_ADMIN, CAPACIDAD, COMENSALES_SENTADOS, ESTADO FROM MESAS M WHERE ESTADO != 0");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Mesa _mesa = new Mesa
                    {
                        Id_Mesa = (int)datos.Lector["ID_MESA"],

                        Id_Mesero = (int)datos.Lector["ID_MESERO"],

                        Id_Admin = (int)datos.Lector["ID_ADMIN"],

                        Capacidad = (int)datos.Lector["CAPACIDAD"],

                        ComensalesSentados = (int)datos.Lector["COMENSALES_SENTADOS"],

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
                datos.setConsulta("INSERT INTO MESAS (ID_MESERO, ID_ADMIN, CAPACIDAD, COMENSALES_SENTADOS, ESTADO) VALUES(@IDMESERO, @IDADMIN, @CAP, @COMENSALESSENTADOS, 1)");
                datos.setParametro("@IDMESERO", _mesa.Id_Mesero);
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

        public void ResetearMesa(Mesa _mesa)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE MESAS SET COMENSALES_SENTADOS = 0 WHERE ID_MESA = @IDMESA");
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
        }
    }
}
