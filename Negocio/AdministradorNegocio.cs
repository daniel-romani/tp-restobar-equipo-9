﻿using Modelo;
using System;
using System.Collections.Generic;
using tp_restobar_equipo_9.Modelo;

namespace Negocio
{
    public class AdministradorNegocio
    {
        public void InsertarAdministrador(Usuario usuario_actual)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("INSERT INTO ADMINISTRADOR (ID_USUARIO, DNI, NOMBRE,APELLIDO, TELEFONO, DIRECCION, FECHA_NACIMIENTO, MAIL, ESTADO) \r\nVALUES(@IDUSUARIO, @DNI, @NOMBRE, @APELLIDO, @TELEFONO, @DIRECCION, @FECHANACIMIENTO, @MAIL, 1)");
                datos.setParametro("@IDUSUARIO", usuario_actual.Id);
                datos.setParametro("@DNI", usuario_actual.Dni);
                datos.setParametro("@NOMBRE", usuario_actual.Nombre);
                datos.setParametro("@APELLIDO", usuario_actual.Apellido);
                datos.setParametro("@TELEFONO", usuario_actual.Telefono);
                datos.setParametro("@DIRECCION", usuario_actual.Direccion);
                datos.setParametro("@FECHANACIMIENTO", usuario_actual.Fecha_Nacimiento);
                datos.setParametro("@MAIL", usuario_actual.Mail);
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
        public List<Administrador> Listar()
        {
            List<Administrador> lista = new List<Administrador>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT ID_ADMINISTRADOR, ID_USUARIO, DNI, NOMBRE, APELLIDO, TELEFONO, DIRECCION, FECHA_NACIMIENTO, MAIL, ESTADO FROM ADMINISTRADOR WHERE ESTADO != 0");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Administrador administrador = new Administrador
                    {
                        Id = (int)datos.Lector["ID_ADMINISTRADOR"],

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

                    lista.Add(administrador);
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

        public Administrador getAdm(string dniAdm)
        {
            AccesoDatos datos = new AccesoDatos();
            Administrador adm = new Administrador();

            try
            {
                datos.setConsulta("SELECT ID_ADMINISTRADOR, ID_USUARIO, DNI, NOMBRE, APELLIDO, TELEFONO, DIRECCION, FECHA_NACIMIENTO, MAIL, ESTADO FROM ADMINISTRADOR WHERE ESTADO != 0 AND DNI = @DNIADM");
                datos.setParametro("@DNIADM", dniAdm);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    adm.Id = (int)datos.Lector["ID_ADMINISTRADOR"];

                    adm.Id_Usuario = (int)datos.Lector["ID_USUARIO"];

                    adm.Dni = (string)datos.Lector["DNI"];

                    adm.Nombre = (string)datos.Lector["NOMBRE"];

                    adm.Apellido = (string)datos.Lector["APELLIDO"];

                    adm.Telefono = (string)datos.Lector["TELEFONO"];

                    adm.Direccion = (string)datos.Lector["DIRECCION"];

                    adm.Fecha_Nacimiento = (DateTime)datos.Lector["FECHA_NACIMIENTO"];

                    adm.Mail = (string)datos.Lector["MAIL"];

                    adm.Estado = (bool)datos.Lector["ESTADO"];
                }
                return adm;
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
