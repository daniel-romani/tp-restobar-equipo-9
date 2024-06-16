using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Mesa
    {
        public int Id_Mesa { get; set; }
        public int Id_Admin { get; set; }
        public int Id_Mesero { get; set; }
        public int Capacidad { get; set; }
        public int ComensalesSentados { get; set; }
        public List<ItemCarta> Pedido { get; set; }
        public bool Estado { get; set; }
    }
}
