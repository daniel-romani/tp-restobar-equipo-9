using System.Collections.Generic;
using tp_restobar_equipo_9.Modelo;

namespace Modelo
{
    public class Resto
    {
        /*
        Esta clase representa la entidad principal que gestiona todo el sistema. 
        Contiene listas de usuarios, pacientes, médicos y turnos. 
        Además, proporciona métodos para agregar usuarios, mesas, itemCartas, Meseros, Pedidos
        */

        public List<Usuario> Usuarios { get; set; }

        public List<Mesa> Mesas { get; set; }

        public List<ItemCarta> ItemCartas { get; set; }

        public List<Mesero> Meseros { get; set; }

        public List<Administrador> Administracion { get; set; }

        public List<Pedido> Pedidos { get; set; }

        public List<Comensal> Comensales {  get; set; }

        public List<Reserva> Reservas { get; set; }

        public List<Jornada> Jornadas { get; set;}
    }
}
