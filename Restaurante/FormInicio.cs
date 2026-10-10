using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Restaurante.Modelo;
using Restaurante.Negocio;

namespace Restaurante
{
    // Pantalla de inicio de la app Cliente: muestra las mesas y deja elegir una LIBRE.
    // Concurrencia: la lectura de mesas corre en una tarea de fondo (Task) que se repite
    // cada 10 s y se puede cancelar (CancellationToken). La ventana nunca se congela.
    public class FormInicio : Form
    {
        private readonly FlowLayoutPanel panelMesas = new FlowLayoutPanel();
        private readonly Label lblTitulo = new Label();
        private readonly Label lblInfo = new Label();
        private readonly Button btnRefrescar = new Button();
        private readonly ProgressBar barra = new ProgressBar();

        private readonly MesaService _mesas = new MesaService();
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly SemaphoreSlim _cargando = new SemaphoreSlim(1, 1); // evita dos cargas a la vez
        private Task _monitor;

        public FormInicio()
        {
            Text = "Food Zone Restaurante";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(640, 460);
            BackColor = Color.White;

            lblTitulo.Text = "Food Zone Restaurante";
            lblTitulo.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Height = 60;
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            lblInfo.Text = "Selecciona tu mesa (las mesas en verde están disponibles)";
            lblInfo.Font = new Font("Segoe UI", 11);
            lblInfo.Dock = DockStyle.Top;
            lblInfo.Height = 36;
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;

            barra.Style = ProgressBarStyle.Marquee;
            barra.MarqueeAnimationSpeed = 30;
            barra.Dock = DockStyle.Bottom;
            barra.Height = 6;
            barra.Visible = false;

            btnRefrescar.Text = "Refrescar";
            btnRefrescar.Dock = DockStyle.Bottom;
            btnRefrescar.Height = 40;
            btnRefrescar.Click += async (s, e) => await CargarMesasAsync();

            panelMesas.Dock = DockStyle.Fill;
            panelMesas.Padding = new Padding(20);
            panelMesas.AutoScroll = true;

            // Orden importa: Fill primero, luego los Dock top/bottom
            Controls.Add(panelMesas);
            Controls.Add(lblInfo);
            Controls.Add(lblTitulo);
            Controls.Add(barra);
            Controls.Add(btnRefrescar);

            Load += (s, e) => { _monitor = MonitorearMesasAsync(_cts.Token); };
            FormClosed += (s, e) => _cts.Cancel();   // detiene la tarea de fondo
        }

        // Tarea de fondo: refresca las mesas cada 10 s (otras mesas pueden ocuparse o liberarse
        // desde la app Mesero mientras tanto) hasta que se cancele.
        private async Task MonitorearMesasAsync(CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    await CargarMesasAsync();
                    await Task.Delay(10000, ct);
                }
            }
            catch (OperationCanceledException) { }
        }

        private async Task CargarMesasAsync()
        {
            // Si ya hay una carga en curso, no lanzar otra
            if (!await _cargando.WaitAsync(0)) return;
            var ct = _cts.Token;
            try
            {
                btnRefrescar.Enabled = false;
                barra.Visible = true;

                List<Mesa> mesas = await _mesas.ListarAsync(ct);   // BD en segundo plano
                if (ct.IsCancellationRequested || IsDisposed) return;

                panelMesas.SuspendLayout();
                panelMesas.Controls.Clear();
                foreach (var m in mesas)
                    panelMesas.Controls.Add(CrearTarjeta(m));
                panelMesas.ResumeLayout();

                lblInfo.Text = "Selecciona tu mesa (las mesas en verde están disponibles)";
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                if (!IsDisposed) lblInfo.Text = "No se pudo conectar: " + ex.Message;
            }
            finally
            {
                if (!IsDisposed)
                {
                    barra.Visible = false;
                    btnRefrescar.Enabled = true;
                }
                _cargando.Release();
            }
        }

        private Button CrearTarjeta(Mesa mesa)
        {
            bool libre = mesa.Libre;
            var btn = new Button
            {
                Width = 130,
                Height = 110,
                Margin = new Padding(12),
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = libre ? Color.FromArgb(46, 160, 67) : Color.FromArgb(190, 60, 60),
                Text = "Mesa " + mesa.Numero + "\n" + (libre ? "Disponible" : "Ocupada"),
                Enabled = libre
            };
            btn.FlatAppearance.BorderSize = 0;
            if (libre)
                btn.Click += async (s, e) => await ElegirMesaAsync(mesa);
            return btn;
        }

        private async Task ElegirMesaAsync(Mesa mesa)
        {
            panelMesas.Enabled = false;
            barra.Visible = true;
            try
            {
                // Espera a que termine una carga en curso para no pisarla
                await _cargando.WaitAsync();
                bool ok;
                try { ok = await _mesas.ElegirMesaAsync(mesa, _cts.Token); }
                finally { _cargando.Release(); }

                if (!ok)
                {
                    MessageBox.Show("Esa mesa ya fue ocupada. Elige otra.", "Mesa no disponible");
                    panelMesas.Enabled = true;
                    await CargarMesasAsync();
                    return;
                }

                _cts.Cancel();   // ya no hace falta refrescar las mesas
                Navegacion.IrA(this, new Form1());
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show("Error al elegir la mesa: " + ex.Message);
                if (!IsDisposed) { panelMesas.Enabled = true; barra.Visible = false; }
            }
        }
    }
}
