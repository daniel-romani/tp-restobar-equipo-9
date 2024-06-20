using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ItemCarta
    {
        public string nombre { get; set; }
        public Double precio { get; set; }

        public string tipo { get; set; }

        public int cantidad { get; set; }

        public string urlImagen { get; set; }

        public string unidad {  get; set; }

        public int idProducto { get; set; }
    }
}
