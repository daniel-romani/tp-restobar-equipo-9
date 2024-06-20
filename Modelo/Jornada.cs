using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Jornada
    {
        public int idJornada {  get; set; }
        public DateTime fecha {  get; set; }
        public TimeSpan hora_Ini { get; set; }
        public TimeSpan hora_Fin { get; set; }


    }
}
