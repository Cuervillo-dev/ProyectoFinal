using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Restaurante.Modelo;

namespace Restaurante.Datos
{
    public class PedidoDAO
    {
        // Guarda los items en el pedido ABIERTO de la mesa (estado distinto de PAGADO).
        // Si no existe pedido abierto crea uno nuevo; asi no se crea un idPedido por cada envio.
        // Todo ocurre en UNA transaccion (cabecera + detalle): o se guarda todo o nada.
        // Se ejecuta en un hilo de fondo (Task.Run) para no congelar la interfaz.
        public Task<int> GuardarAsync(int idMesa, IList<ItemPedido> items, CancellationToken ct)
        {
            // Copia local: la lista del carrito pertenece al hilo de la interfaz.
            var copia = new List<ItemPedido>(items);
            return Task.Run(() => GuardarSync(idMesa, copia, ct), ct);
        }

        private int GuardarSync(int idMesa, List<ItemPedido> items, CancellationToken ct)
        {
            using (var con = Conexion.Abrir())
            using (var tx = con.BeginTransaction())
            {
                try
                {
                    int idPedido;
                    string estado = null;

                    // 1) Buscar pedido abierto de la mesa (FOR UPDATE: bloquea la fila mientras dura la transaccion)
                    using (var cmd = new MySqlCommand(
                        "SELECT idPedido, estado FROM Pedidos " +
                        "WHERE idMesa = @mesa AND estado <> 'PAGADO' " +
                        "ORDER BY idPedido DESC LIMIT 1 FOR UPDATE", con, tx))
                    {
                        cmd.Parameters.AddWithValue("@mesa", idMesa);
                        using (var rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                idPedido = rd.GetInt32(0);
                                estado = rd.GetString(1);
                            }
                            else
                            {
                                idPedido = 0;
                            }
                        }
                    }

                    ct.ThrowIfCancellationRequested();

                    if (idPedido == 0)
                    {
                        // 2a) No hay pedido abierto: crear cabecera
                        using (var cmd = new MySqlCommand(
                            "INSERT INTO Pedidos (idMesa, estado, fecha) VALUES (@mesa, 'PENDIENTE', NOW())", con, tx))
                        {
                            cmd.Parameters.AddWithValue("@mesa", idMesa);
                            cmd.ExecuteNonQuery();
                            idPedido = (int)cmd.LastInsertedId;
                        }
                    }
                    else if (estado == "CUENTA_SOLICITADA")
                    {
                        throw new InvalidOperationException(
                            "Esta mesa ya pidio la cuenta. No se pueden agregar mas productos.");
                    }

                    // 3) Detalle: una fila por producto
                    foreach (var it in items)
                    {
                        ct.ThrowIfCancellationRequested();
                        using (var cmd = new MySqlCommand(
                            "INSERT INTO DetallePedidos (idPedido, idProducto, cantidad, precioUnitario) " +
                            "VALUES (@p, @prod, @cant, @precio)", con, tx))
                        {
                            cmd.Parameters.AddWithValue("@p", idPedido);
                            cmd.Parameters.AddWithValue("@prod", it.IdProducto);
                            cmd.Parameters.AddWithValue("@cant", it.Cantidad);
                            cmd.Parameters.AddWithValue("@precio", it.Precio);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 2b) Si ya existia y ya estaba en cocina/entregado, vuelve a PENDIENTE
                    //     para que el procesador y el mesero vean que hay productos nuevos.
                    if (estado == "EN_PROCESO" || estado == "ENTREGADO")
                    {
                        using (var cmd = new MySqlCommand(
                            "UPDATE Pedidos SET estado = 'PENDIENTE' WHERE idPedido = @p", con, tx))
                        {
                            cmd.Parameters.AddWithValue("@p", idPedido);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    ct.ThrowIfCancellationRequested();
                    tx.Commit();
                    return idPedido;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        // El cliente pide la cuenta: el Mesero lo ve en su caja de notificaciones.
        public async Task<bool> SolicitarCuentaAsync(int idMesa, CancellationToken ct)
        {
            using (var con = await Conexion.AbrirAsync(ct))
            using (var cmd = new MySqlCommand(
                "UPDATE Pedidos SET estado = 'CUENTA_SOLICITADA' " +
                "WHERE idMesa = @mesa AND estado <> 'PAGADO' AND estado <> 'CUENTA_SOLICITADA'", con))
            {
                cmd.Parameters.AddWithValue("@mesa", idMesa);
                return await cmd.ExecuteNonQueryAsync(ct) > 0;
            }
        }
    }
}
