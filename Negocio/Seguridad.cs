using Modelo;
using System;
using System.Collections.Generic;
using tp_restobar_equipo_9.Modelo;

namespace Negocio
{
    public static class Seguridad
    {
        public static bool SesionActiva(object user) // Valida si tiene una sesion activa
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            if (usuario != null && usuario.Id != -1)
                return true;
            else
                return false;
        }
    }
}
