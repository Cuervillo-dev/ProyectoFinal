using System;
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
        public static bool CuentaSolicitada { get; set; }
        public static DateTime UltimaLlamada { get; set; }   // ultima vez que llamo al mesero

        public static List<ItemPedido> Items = new List<ItemPedido>();
        public static decimal Total { get { return Items.Sum(i => i.Precio * i.Cantidad); } }

        // Un solo carrito para toda la mesa: si el producto ya esta, suma la cantidad.
        public static void Agregar(Producto p, int cantidad)
        {
            ItemPedido existente = Items.FirstOrDefault(i => i.IdProducto == p.IdProducto);
            if (existente != null)
            {
                existente.Cantidad += cantidad;
            }
            else
            {
                Items.Add(new ItemPedido
                {
                    IdProducto = p.IdProducto,
                    Nombre = p.Nombre,
                    Categoria = p.Categoria,
                    Precio = p.Precio,
                    Cantidad = cantidad
                });
            }
        }

        // Pone el carrito al dia con el catalogo vigente: actualiza precios y nombres, y quita
        // los productos que el Mesero desactivo. Devuelve los nombres de los que se quitaron.
        public static List<string> Sincronizar(IList<Producto> catalogo)
        {
            var quitados = new List<string>();
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                ItemPedido it = Items[i];
                Producto p = catalogo.FirstOrDefault(x => x.IdProducto == it.IdProducto);
                if (p == null)
                {
                    quitados.Add(it.Nombre);
                    Items.RemoveAt(i);
                }
                else
                {
                    it.Nombre = p.Nombre;
                    it.Categoria = p.Categoria;
                    it.Precio = p.Precio;
                }
            }
            return quitados;
        }

        public static void Reiniciar()
        {
            Items.Clear();
            IdMesa = 0;
            NumeroMesa = 0;
            CuentaSolicitada = false;
            UltimaLlamada = DateTime.MinValue;
        }
    }
}
