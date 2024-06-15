using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_restobar_equipo_9.Modelo
{ 
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Contraseña { get; set; }
        public string TipoUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Mail { get; set; }
        public bool Estado { get; set; }
        public int Id_Imagen { get; set; }
        public string Imagen { get; set; }
        public Usuario()
        {
            Id = -1;
            Dni = 0;
            Username = "";
            Contraseña = "";
            TipoUsuario = "";
            Nombre = "";
            Apellido = "";
            Telefono = "";
            Direccion = "";
            Fecha_Nacimiento = new DateTime();
            Mail = "";
            Estado = true;
            Imagen = "https://cdn-icons-png.flaticon.com/512/5987/5987424.png";
        }
    }
}
