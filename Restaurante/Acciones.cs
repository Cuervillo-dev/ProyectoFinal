using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Restaurante.Negocio;

namespace Restaurante
{
    // Acciones del cliente que estan disponibles desde varias pantallas (menu principal,
    // categorias, resumen y pantalla de cuenta): pedir la cuenta y llamar al mesero.
    public static class Acciones
    {
        private static readonly PedidoService _servicio = new PedidoService();

        // Boton con el estilo del menu (negro con borde naranja).
        public static void EstiloBoton(Button b, string texto, float tamano)
        {
            b.Text = texto;
            b.BackColor = Color.Black;
            b.ForeColor = Color.Peru;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderColor = Color.Peru;
            b.FlatAppearance.BorderSize = 2;
            b.Font = new Font("Niagara Solid", tamano);
        }

        // Escribe en la BD que esta mesa llama; el Mesero lo ve en su caja de "Llamadas".
        public static async Task LlamarMeseroAsync()
        {
            if (pedido.IdMesa == 0) return;

            // Evita que presionen el boton muchas veces seguidas y llenen de avisos al Mesero.
            if ((DateTime.Now - pedido.UltimaLlamada).TotalSeconds < 30)
            {
                MessageBox.Show("Ya avisamos al mesero. En un momento te atiende.", "Llamar al mesero");
                return;
            }

            try
            {
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(15)))
                    await _servicio.LlamarMeseroAsync(cts.Token);

                if (pedido.IdMesa == 0) return;   // el mesero libero la mesa mientras tanto
                pedido.UltimaLlamada = DateTime.Now;
                MessageBox.Show("Listo, avisamos al mesero. En un momento te atiende.", "Llamar al mesero");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo avisar al mesero:\n" + ex.Message, "Error");
            }
        }

        // Confirma, marca el pedido como CUENTA_SOLICITADA y deja la mesa en la pantalla de espera.
        public static async Task PedirCuentaAsync()
        {
            if (pedido.IdMesa == 0) return;

            if (pedido.Items.Any())
            {
                MessageBox.Show("Tienes productos sin enviar. Entra a \"Ver Pedido\" y presiona " +
                                "\"Finalizar Pedido\" antes de pedir la cuenta.", "Pedir cuenta");
                return;
            }

            if (MessageBox.Show("¿Deseas pedir la cuenta? El mesero la llevará a tu mesa.",
                                "Pedir cuenta", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                bool ok;
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(15)))
                    ok = await _servicio.PedirCuentaAsync(cts.Token);

                if (pedido.IdMesa == 0) return;   // el mesero libero la mesa mientras tanto

                if (ok)
                {
                    // La mesa queda bloqueada en esta pantalla hasta que el Mesero la libere;
                    // entonces SesionMesa reinicia todo y vuelve a la pantalla de mesas.
                    pedido.CuentaSolicitada = true;
                    Navegacion.IrA(Navegacion.Actual, new FormCuenta());
                }
                else
                {
                    MessageBox.Show("No hay un pedido abierto en esta mesa (todavia no has enviado " +
                                    "ninguno) o la cuenta ya fue solicitada.", "Pedir cuenta");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo solicitar la cuenta:\n" + ex.Message, "Error");
            }
        }
    }
}
