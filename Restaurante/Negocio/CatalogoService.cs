using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Restaurante.Datos;
using Restaurante.Modelo;

namespace Restaurante.Negocio
{
    // Catalogo de productos y precios tomados de la BD (una sola fuente de verdad).
    // Se carga una vez y se reutiliza; el SemaphoreSlim evita que dos formularios
    // lo carguen al mismo tiempo.
    public static class CatalogoService
    {
        private static readonly SemaphoreSlim _candado = new SemaphoreSlim(1, 1);
        private static readonly ProductoDAO _dao = new ProductoDAO();
        private static Dictionary<string, Producto> _porNombre;

        public static async Task CargarAsync(CancellationToken ct, bool forzar = false)
        {
            await _candado.WaitAsync(ct);
            try
            {
                if (_porNombre != null && !forzar) return;

                var lista = await _dao.ListarAsync(ct);
                var dic = new Dictionary<string, Producto>();
                foreach (var p in lista)
                    dic[Normalizar(p.Nombre)] = p;   // si hay nombres repetidos queda el ultimo
                _porNombre = dic;
            }
            finally
            {
                _candado.Release();
            }
        }

        // null si el catalogo no esta cargado o el producto no existe en la BD.
        public static Producto Buscar(string nombre)
        {
            if (_porNombre == null) return null;
            Producto p;
            return _porNombre.TryGetValue(Normalizar(nombre), out p) ? p : null;
        }

        // Compara sin importar mayusculas ni tildes ("Te Helado" == "Té Helado").
        private static string Normalizar(string s)
        {
            if (s == null) return "";
            string d = s.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (char c in d)
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            return sb.ToString();
        }
    }
}
