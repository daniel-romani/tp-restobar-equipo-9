﻿namespace Modelo
{
    public class DetallePedido
    {
        public int Id_DetallePedido { get; set; }
        public int Id_Mesa { get; set; }
        public int Id_Admin { get; set; }
        public int Id_Producto {  get; set; }
        public int Cantidad { get; set; }
        public int PrecioUnitario {  get; set; }
        public int Id_Pedidos { get; set; }

    }
}