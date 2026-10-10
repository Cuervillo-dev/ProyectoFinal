using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Restaurante.Datos;
using Restaurante.Modelo;

namespace Restaurante.Negocio
{
    // Capa de negocio: reglas sobre las mesas.
    public class MesaService
    {
        private readonly MesaDAO _dao = new MesaDAO();

        public Task<List<Mesa>> ListarAsync(CancellationToken ct)
        {
            return _dao.ListarAsync(ct);
        }

        // true = la mesa quedo asignada a este cliente; false = otro cliente la gano.
        public async Task<bool> ElegirMesaAsync(Mesa mesa, CancellationToken ct)
        {
            if (mesa == null || !mesa.Libre) return false;

            bool ok = await _dao.OcuparAsync(mesa.IdMesa, ct);
            if (ok)
            {
                pedido.Reiniciar();
                pedido.IdMesa = mesa.IdMesa;
                pedido.NumeroMesa = mesa.Numero;
            }
            return ok;
        }
    }
}
