using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Restaurante.Modelo;

namespace Restaurante.Datos
{
    public class MesaDAO
    {
        public async Task<List<Mesa>> ListarAsync(CancellationToken ct)
        {
            var lista = new List<Mesa>();
            using (var con = await Conexion.AbrirAsync(ct))
            using (var cmd = new MySqlCommand(
                "SELECT idMesa, numeroMesa, estado FROM Mesas ORDER BY numeroMesa", con))
            using (var rd = await cmd.ExecuteReaderAsync(ct))
            {
                while (await rd.ReadAsync(ct))
                {
                    lista.Add(new Mesa
                    {
                        IdMesa = rd.GetInt32(0),
                        Numero = rd.GetInt32(1),
                        Estado = rd.GetString(2)
                    });
                }
            }
            return lista;
        }

        // UPDATE condicional: si otro cliente la tomo justo antes, no afecta filas
        // y devuelve false (asi dos clientes nunca ocupan la misma mesa).
        public async Task<bool> OcuparAsync(int idMesa, CancellationToken ct)
        {
            using (var con = await Conexion.AbrirAsync(ct))
            using (var cmd = new MySqlCommand(
                "UPDATE Mesas SET estado = 'OCUPADA' WHERE idMesa = @id AND estado = 'LIBRE'", con))
            {
                cmd.Parameters.AddWithValue("@id", idMesa);
                int filas = await cmd.ExecuteNonQueryAsync(ct);
                return filas > 0;
            }
        }
    }
}
