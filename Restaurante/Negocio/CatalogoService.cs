using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Restaurante.Datos;
using Restaurante.Modelo;

namespace Restaurante.Negocio
{
    // Catalogo de productos y precios tomados de la BD (una sola fuente de verdad).
    // Lo que el Mesero agrega, cambia de precio o desactiva se refleja aqui en cuanto se
    // vuelve a consultar: ActualizarAsync compara la lista nueva con la anterior y solo
    // avisa (evento Cambio) cuando algo cambio de verdad.
    public static class CatalogoService
    {
        private static readonly SemaphoreSlim _candado = new SemaphoreSlim(1, 1);
        private static readonly ProductoDAO _dao = new ProductoDAO();
        private static List<Producto> _productos = new List<Producto>();
        private static string _firma;   // "huella" del catalogo; null = nunca se ha cargado

        // Se dispara en el hilo de la interfaz, solo cuando el catalogo cambio.
        public static event EventHandler Cambio;

        public static IList<Producto> Productos { get { return _productos; } }

        // Categorias en el orden en que aparecen en la tabla (por idProducto).
        public static List<string> Categorias()
        {
            return _productos.Select(p => p.Categoria).Distinct().ToList();
        }

        public static List<Producto> DeCategoria(string categoria)
        {
            return _productos.Where(p => p.Categoria == categoria).ToList();
        }

        // Consulta la BD y actualiza el catalogo. Devuelve true si hubo cambios.
        public static async Task<bool> ActualizarAsync(CancellationToken ct)
        {
            bool cambio;
            await _candado.WaitAsync(ct);
            try
            {
                List<Producto> lista = await _dao.ListarAsync(ct);
                string firma = Firma(lista);
                cambio = firma != _firma;
                if (cambio)
                {
                    _productos = lista;
                    _firma = firma;
                }
            }
            finally
            {
                _candado.Release();
            }

            if (cambio)
            {
                EventHandler h = Cambio;
                if (h != null) h(null, EventArgs.Empty);
            }
            return cambio;
        }

        private static string Firma(List<Producto> lista)
        {
            return string.Join("|", lista.Select(p =>
                p.IdProducto + ":" + p.Nombre + ":" +
                p.Precio.ToString(CultureInfo.InvariantCulture) + ":" + p.Categoria));
        }
    }
}
