using Modelo;
using System;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public bool Loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("Select id, usuario, pass, tipo from USUARIOS Where usuario = @user AND pass = @pass");
                datos.setParametro("@user", usuario.Username);
                datos.setParametro("@pass", usuario.Contraseña);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.TipoUsuario = (int)(datos.Lector["Tipo"]) == 2 ? TipoUsuario.ADMIN : TipoUsuario.MOZO;
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
