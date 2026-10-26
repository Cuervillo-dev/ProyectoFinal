using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Restaurante.Datos;

namespace Restaurante.Negocio
{
    // Vigilante de la mesa del cliente. Vive aparte de los formularios (que se cierran y se
    // crean en cada navegacion) y cada 5 segundos hace dos cosas, solo si ya hay mesa elegida:
    //   1) Pregunta el estado de SU mesa. Si el Mesero la libero (LIBRE) borra el carrito y
    //      regresa a la pantalla de mesas sin que el cliente haga nada.
    //   2) Vuelve a leer el catalogo: productos nuevos, precios o productos desactivados.
    // Si falla la conexion NO se reinicia nada: solo se reintenta en el siguiente ciclo.
    public static class SesionMesa
    {
        private static readonly MesaDAO _mesas = new MesaDAO();
        private static System.Windows.Forms.Timer _timer;
        private static bool _ocupado;

        // Llamar una sola vez desde Program.Main (hilo de la interfaz).
        public static void Iniciar()
        {
            _timer = new System.Windows.Forms.Timer { Interval = 5000 };
            _timer.Tick += async (s, e) => await VerificarAsync();
            _timer.Start();
        }

        private static async Task VerificarAsync()
        {
            if (_ocupado || pedido.IdMesa == 0) return;
            _ocupado = true;
            string aviso = null;
            try
            {
                int idMesa = pedido.IdMesa;
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)))
                {
                    string estado = await _mesas.ObtenerEstadoAsync(idMesa, cts.Token);
                    if (pedido.IdMesa != idMesa) return;   // la sesion cambio mientras consultaba

                    if (estado == "LIBRE")
                    {
                        ReiniciarSesion();
                        return;
                    }

                    if (await CatalogoService.ActualizarAsync(cts.Token))
                    {
                        var quitados = pedido.Sincronizar(CatalogoService.Productos);
                        if (quitados.Count > 0)
                            aviso = "Estos productos ya no estan disponibles y se quitaron de tu pedido:\n - " +
                                    string.Join("\n - ", quitados);
                    }
                }
            }
            catch (Exception)
            {
                // Sin red o BD lenta: se reintenta en 5 s. Nunca se reinicia la sesion por un error.
            }
            finally
            {
                _ocupado = false;
            }

            // El mensaje se muestra fuera del bloque para no frenar las siguientes verificaciones.
            if (aviso != null) MessageBox.Show(aviso, "Menu actualizado");
        }

        // El Mesero libero la mesa: se limpia todo y se vuelve a la pantalla de mesas.
        private static void ReiniciarSesion()
        {
            pedido.Reiniciar();
            Navegacion.IrA(Navegacion.Actual, new FormInicio());
        }
    }
}
