using Modelo;
using System;
using System.Collections.Generic;
using tp_restobar_equipo_9.Modelo;

namespace Negocio
{
    public class RestoConexion
    {
        public Resto Listar()
        {
            Resto objetoResto = new Resto();
            try
            {
                objetoResto.Usuarios = new List<Usuario>();
                objetoResto.Mesas = new List<Mesa>();
                objetoResto.ItemCartas = new List<ItemCarta>();
                objetoResto.Meseros = new List<Mesero>();
                objetoResto.Pedidos = new List<Pedido>();
                objetoResto.Comensales = new List<Comensal>();
                objetoResto.Reservas = new List<Reserva>();

                UsuarioNegocio usuarioConexion = new UsuarioNegocio();
                objetoResto.Usuarios = usuarioConexion.Listar_todos();

                MesaNegocio MesaConexion = new MesaNegocio();
                objetoResto.Mesas = MesaConexion.Listar();

                ItemCartaNegocio ItemCartaConexion = new ItemCartaNegocio();
                objetoResto.ItemCartas = ItemCartaConexion.Listar();

                MeseroNegocio MeseroConexion = new MeseroNegocio();
                objetoResto.Meseros = MeseroConexion.Listar();

                AdministradorNegocio AdminConexion = new AdministradorNegocio();
                objetoResto.Administracion = AdminConexion.Listar();

                PedidosNegocio PedidosConexion = new PedidosNegocio();
                objetoResto.Pedidos = PedidosConexion.Listar();

                ComensalNegocio ComensalesConexion = new ComensalNegocio();
                objetoResto.Comensales = ComensalesConexion.Listar();

                ReservaNegocio ReservaConexion = new ReservaNegocio();
                objetoResto.Reservas = ReservaConexion.Listar();

                return objetoResto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
