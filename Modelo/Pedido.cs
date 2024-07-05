using System.Collections.Generic;

namespace Modelo
{
    public class Pedido
    {
        public int Id_Pedido {  get; set; }
        public int Id_Mesa { get; set; }
        public int Id_Admin { get; set; }
        public int Id_Mesero { get; set; }
        public decimal Total {  get; set; }
        public string Fecha {  get; set; }
        public bool Estado { get; set; }

        public Pedido() 
        {
            Id_Mesa = 0;
            Id_Admin = 0;
            Id_Mesero = 0;
            Total = 0;
            Fecha = string.Empty;
        }
    }
}
