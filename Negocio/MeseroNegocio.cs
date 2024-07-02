using Modelo;
using System;
using System.Collections.Generic;
using tp_restobar_equipo_9.Modelo;

namespace Negocio
{
    public class MeseroNegocio
    {
        public List<Mesero> Listar()
        {
            List<Mesero> lista = new List<Mesero>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT ID_MESERO, ID_USUARIO, DNI, NOMBRE, APELLIDO, TELEFONO, DIRECCION, FECHA_NACIMIENTO, MAIL, ESTADO FROM MESEROS WHERE ESTADO != 0");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Mesero moso = new Mesero
                    {
                        Id = (int)datos.Lector["ID_MESERO"],

                        Id_Usuario = (int)datos.Lector["ID_USUARIO"],

                        Dni = (string)datos.Lector["DNI"],

                        Nombre = (string)datos.Lector["NOMBRE"],

                        Apellido = (string)datos.Lector["APELLIDO"],

                        Telefono = (string)datos.Lector["TELEFONO"],

                        Direccion = (string)datos.Lector["DIRECCION"],

                        Fecha_Nacimiento = (DateTime)datos.Lector["FECHA_NACIMIENTO"],

                        Mail = (string)datos.Lector["MAIL"],

                        Estado = (bool)datos.Lector["ESTADO"]
                    };

                    lista.Add(moso);
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

        public void InsertarMesero(Mesero moso)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("INSERT INTO MESEROS (ID_USUARIO, DNI, NOMBRE,APELLIDO, TELEFONO, DIRECCION, FECHA_NACIMIENTO, MAIL, ESTADO) VALUES(@IDUSUARIO, @DNI, @NOMBRE, @APELLIDO, @TELEFONO, @DIRECCION, @FECHANACIMIENTO, @MAIL, 1)");
                datos.setParametro("@IDUSUARIO", moso.Id_Usuario);
                datos.setParametro("@DNI", moso.Dni);
                datos.setParametro("@NOMBRE", moso.Nombre);
                datos.setParametro("@APELLIDO", moso.Apellido);
                datos.setParametro("@TELEFONO", moso.Telefono);
                datos.setParametro("@DIRECCION", moso.Direccion);
                datos.setParametro("@FECHANACIMIENTO", moso.Fecha_Nacimiento);
                datos.setParametro("@MAIL", moso.Mail);
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

        public Mesero getMesero(string dniMoso)
        {
            AccesoDatos datos = new AccesoDatos();
            Mesero moso = new Mesero();

            try
            {
                datos.setConsulta("SELECT ID_MESERO, ID_USUARIO, DNI, NOMBRE, APELLIDO, TELEFONO, DIRECCION, FECHA_NACIMIENTO, MAIL, ESTADO FROM MESEROS WHERE DNI = @DNIMOSO");
                datos.setParametro("@DNIMOSO", dniMoso);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    moso.Id = (int)datos.Lector["ID_MESERO"];

                    moso.Id_Usuario = (int)datos.Lector["ID_USUARIO"];

                    moso.Dni = (string)datos.Lector["DNI"];

                    moso.Nombre = (string)datos.Lector["NOMBRE"];

                    moso.Apellido = (string)datos.Lector["APELLIDO"];

                    moso.Telefono = (string)datos.Lector["TELEFONO"];

                    moso.Direccion = (string)datos.Lector["DIRECCION"];

                    moso.Fecha_Nacimiento = (DateTime)datos.Lector["FECHA_NACIMIENTO"];

                    moso.Mail = (string)datos.Lector["MAIL"];

                    moso.Estado = (bool)datos.Lector["ESTADO"];
                }
                return moso;
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

        public bool MeseroExistente(string dniMesero)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT COUNT(*) FROM MESEROS WHERE DNI = @DNI_MESERO");
                datos.setParametro("@DNI_MESERO", dniMesero);
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

        public void BajaLogicaMesero(int idMesero)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE MESEROS SET ESTADO = 0 WHERE ID_MESERO = @IDMESERO");
                datos.setParametro("@IDMESERO", idMesero);
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

        public void BajaFisicaMesero(int idMesero)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("DELETE FROM MESEROS WHERE ID_MESERO = @IDMESERO");
                datos.setParametro("@IDMESERO", idMesero);
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

        public void ModificarEstadoMesero(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE MESEROS SET ESTADO = 1 WHERE ID_MESERO = @IDMESERO");
                datos.setParametro("@IDMESERO", idUsuario);
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
