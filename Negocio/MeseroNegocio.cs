using Modelo;
using System;
using System.Collections.Generic;

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
                datos.setConsulta("SELECT ID_MESERO, ID_USUARIO, DNI, NOMBRE, APELLIDO, TELEFONO, DIRECCION, FECHA_NACIMIENTO, MAIL, ESTADO FROM MESEROS WHERE ESTADO != 0 AND DNI = @DNIMOSO");
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

        //Me quedo en el proceso, si sirve para adelante la usaremos.
        public bool MeseroExistente(string dniMesero)
        {
            AccesoDatos datos = new AccesoDatos();
            bool existe = false;

            try
            {
                datos.setConsulta("SELECT ID_MESERO FROM MESEROS M WHERE M.DNI = @DNI_MESERO\r\n");
                datos.setParametro("@DNI_MESERO", dniMesero);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    if (!(datos.Lector["ESTADO"] is 1))
                    {
                        existe = true;
                    }
                }
                return existe;
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
