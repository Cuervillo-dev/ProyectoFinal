using System.Collections.Generic;
using System.Linq;
using Restaurante.Modelo;

namespace Restaurante
{
    // Estado de la sesion del cliente: mesa elegida y carrito de productos.
    // Solo se toca desde el hilo de la interfaz (los await regresan a ese hilo).
    public static class pedido
    {
        public static int IdMesa { get; set; }
        public static int NumeroMesa { get; set; }

        public static List<ItemPedido> Items = new List<ItemPedido>();
        public static decimal Total { get { return Items.Sum(i => i.Precio * i.Cantidad); } }

        public static void Reiniciar()
        {
            Items.Clear();
            IdMesa = 0;
            NumeroMesa = 0;
        }
    }
}
