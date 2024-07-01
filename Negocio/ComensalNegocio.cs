using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ComensalNegocio
    {
        public List<Comensal> Listar()
        {
            List<Comensal> lista = new List<Comensal>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT ID_COMENSAL, ID_USUARIO, DNI, NOMBRE, APELLIDO, TELEFONO, DIRECCION, FECHA_NACIMIENTO, MAIL, ESTADO FROM COMENSALES WHERE ESTADO != 0");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Comensal comensal = new Comensal
                    {
                        Id = (int)datos.Lector["ID_COMENSAL"],

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

                    lista.Add(comensal);
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

        public void InsertarComensal(Comensal comensal)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("INSERT INTO COMENSALES (ID_USUARIO, DNI, NOMBRE,APELLIDO, TELEFONO, DIRECCION, FECHA_NACIMIENTO, MAIL, ESTADO) VALUES(@IDUSUARIO, @DNI, @NOMBRE, @APELLIDO, @TELEFONO, @DIRECCION, @FECHANACIMIENTO, @MAIL, 1)");
                datos.setParametro("@IDUSUARIO", comensal.Id_Usuario);
                datos.setParametro("@DNI", comensal.Dni);
                datos.setParametro("@NOMBRE", comensal.Nombre);
                datos.setParametro("@APELLIDO", comensal.Apellido);
                datos.setParametro("@TELEFONO", comensal.Telefono);
                datos.setParametro("@DIRECCION", comensal.Direccion);
                datos.setParametro("@FECHANACIMIENTO", comensal.Fecha_Nacimiento);
                datos.setParametro("@MAIL", comensal.Mail);
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

        public Comensal getComensal(string dniComensal)
        {
            AccesoDatos datos = new AccesoDatos();
            Comensal comensal = new Comensal();

            try
            {
                datos.setConsulta("SELECT ID_COMENSAL, ID_USUARIO, DNI, NOMBRE, APELLIDO, TELEFONO, DIRECCION, FECHA_NACIMIENTO, MAIL, ESTADO FROM COMENSALES WHERE DNI = @DNICOMENSAL");
                datos.setParametro("@DNICOMENSAL", dniComensal);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    comensal.Id = (int)datos.Lector["ID_COMENSAL"];

                    comensal.Id_Usuario = (int)datos.Lector["ID_USUARIO"];

                    comensal.Dni = (string)datos.Lector["DNI"];

                    comensal.Nombre = (string)datos.Lector["NOMBRE"];

                    comensal.Apellido = (string)datos.Lector["APELLIDO"];

                    comensal.Telefono = (string)datos.Lector["TELEFONO"];

                    comensal.Direccion = (string)datos.Lector["DIRECCION"];

                    comensal.Fecha_Nacimiento = (DateTime)datos.Lector["FECHA_NACIMIENTO"];

                    comensal.Mail = (string)datos.Lector["MAIL"];

                    comensal.Estado = (bool)datos.Lector["ESTADO"];
                }
                return comensal;
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

        public bool ComensalExistente(string dniComensal)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT COUNT(*) FROM COMENSALES WHERE DNI = @DNI_COMENSAL");
                datos.setParametro("@DNI_COMENSAL", dniComensal);
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

        public void BajaLogicaComensal(int idComensal)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE COMENSALES SET ESTADO = 0 WHERE ID_COMENSAL = @IDCOMENSAL");
                datos.setParametro("@IDCOMENSAL", idComensal);
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

        public void BajaFisicaComensal(int idComensal)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("DELETE FROM COMENSALES WHERE ID_COMENSAL = @IDCOMENSAL");
                datos.setParametro("@IDCOMENSAL", idComensal);
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

        public void ModificarEstadoComensal(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE COMENSALES SET ESTADO = 1 WHERE ID_COMENSAL = @IDCOMENSAL");
                datos.setParametro("@IDCOMENSAL", idUsuario);
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
