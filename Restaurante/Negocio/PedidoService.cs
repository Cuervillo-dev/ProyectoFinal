using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Restaurante.Datos;

namespace Restaurante.Negocio
{
    // Capa de negocio: validaciones y reglas del pedido.
    public class PedidoService
    {
        private readonly PedidoDAO _dao = new PedidoDAO();
        private readonly MesaDAO _mesas = new MesaDAO();

        // Valida, guarda en la BD y vacia el carrito. Devuelve el idPedido.
        public async Task<int> EnviarAsync(CancellationToken ct)
        {
            if (pedido.IdMesa == 0)
                throw new InvalidOperationException("Primero debes elegir una mesa.");

            // Antes de enviar se confirma el menu vigente: el Mesero pudo cambiar precios o
            // desactivar productos desde la ultima vez que se actualizo.
            await CatalogoService.ActualizarAsync(ct);
            var quitados = pedido.Sincronizar(CatalogoService.Productos);
            if (quitados.Count > 0)
                throw new InvalidOperationException(
                    "Estos productos ya no estan disponibles y se quitaron del pedido:\n - " +
                    string.Join("\n - ", quitados) + "\n\nRevisa tu pedido y vuelve a enviarlo.");

            if (!pedido.Items.Any())
                throw new InvalidOperationException("El pedido esta vacio. Agrega productos antes de finalizar.");
            if (pedido.Items.Any(i => i.IdProducto == 0 || i.Cantidad <= 0))
                throw new InvalidOperationException("Hay productos invalidos en el pedido.");

            int idPedido = await _dao.GuardarAsync(pedido.IdMesa, pedido.Items, ct);
            pedido.Items.Clear();   // solo se limpia si se guardo bien
            return idPedido;
        }

        public async Task LlamarMeseroAsync(CancellationToken ct)
        {
            if (pedido.IdMesa == 0)
                throw new InvalidOperationException("Primero debes elegir una mesa.");
            await _mesas.LlamarMeseroAsync(pedido.IdMesa, ct);
        }

        public async Task<bool> PedirCuentaAsync(CancellationToken ct)
        {
            if (pedido.IdMesa == 0)
                throw new InvalidOperationException("Primero debes elegir una mesa.");
            return await _dao.SolicitarCuentaAsync(pedido.IdMesa, ct);
        }
    }
}
