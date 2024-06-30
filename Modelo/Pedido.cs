using System.Collections.Generic;

namespace Modelo
{
    public class Pedido
    {
        public int Id_Pedido {  get; set; }
        public int Id_Mesa { get; set; }
        public int Id_Admin { get; set; }
        public int Id_Mesero { get; set; }
        public List<ItemCarta> Items { get; set; }
        public bool Estado { get; set; }
    }
}
