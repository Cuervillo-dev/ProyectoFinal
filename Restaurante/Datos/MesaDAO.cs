using System;
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

        // Estado actual de una mesa (LIBRE / OCUPADA); null si no existe.
        public async Task<string> ObtenerEstadoAsync(int idMesa, CancellationToken ct)
        {
            using (var con = await Conexion.AbrirAsync(ct))
            using (var cmd = new MySqlCommand("SELECT estado FROM Mesas WHERE idMesa = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", idMesa);
                object r = await cmd.ExecuteScalarAsync(ct);
                return (r == null || r == DBNull.Value) ? null : Convert.ToString(r);
            }
        }

        // El cliente llama al mesero: el Mesero ve la mesa en su caja de "Llamadas".
        // Requiere la columna Mesas.llamaMesero (ver ALTER TABLE).
        public async Task LlamarMeseroAsync(int idMesa, CancellationToken ct)
        {
            using (var con = await Conexion.AbrirAsync(ct))
            using (var cmd = new MySqlCommand(
                "UPDATE Mesas SET llamaMesero = 1 WHERE idMesa = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", idMesa);
                await cmd.ExecuteNonQueryAsync(ct);
            }
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
