using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Reserva
    {
        public int Id { get; set; }
        public int Id_Comensal { get; set; }
        public string DniComensal { get; set; }
        public int Id_Mesa { get; set; }
        public int Cantidad_Comensales { get; set; }
        public bool Estado { get; set; }

        public Reserva()
        {
            Id = -1;
            Id_Comensal = -1;
            DniComensal = "no especificado";
            Id_Mesa = -1;
            Cantidad_Comensales = 1;
            Estado = true;
        }
    }
}
