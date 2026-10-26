using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Restaurante.Modelo;

namespace Restaurante.Datos
{
    public class ProductoDAO
    {
        public async Task<List<Producto>> ListarAsync(CancellationToken ct)
        {
            var lista = new List<Producto>();
            using (var con = await Conexion.AbrirAsync(ct))
            using (var cmd = new MySqlCommand(
                "SELECT idProducto, nombre, precio, categoria FROM Productos " +
                "WHERE activo = 1 ORDER BY idProducto", con))
            using (var rd = await cmd.ExecuteReaderAsync(ct))
            {
                while (await rd.ReadAsync(ct))
                {
                    lista.Add(new Producto
                    {
                        IdProducto = rd.GetInt32(0),
                        Nombre = rd.GetString(1),
                        Precio = rd.GetDecimal(2),
                        Categoria = rd.IsDBNull(3) ? "Otros" : rd.GetString(3)
                    });
                }
            }
            return lista;
        }
    }
}
