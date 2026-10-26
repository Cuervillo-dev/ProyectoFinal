using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Restaurante.Negocio;

namespace Restaurante
{
    public partial class ResumenPedido : Form
    {
        private readonly PedidoService _servicio = new PedidoService();
        private CancellationTokenSource _cts = new CancellationTokenSource();

        // Controles que se crean por codigo (no tocan el diseñador)
        private readonly ProgressBar barra = new ProgressBar();
        private readonly Button btnCancelar = new Button();
        private readonly Button btnCuenta = new Button();
        private readonly Button btnMesero = new Button();
        private readonly Label lblEstado = new Label();

        public ResumenPedido()
        {
            InitializeComponent();
            CrearControlesExtra();
            CargarPedido();
            FormClosed += (s, e) => _cts.Cancel();
        }

        private void CrearControlesExtra()
        {
            Text = "Resumen del Pedido - Mesa " + pedido.NumeroMesa;

            btnCuenta.Text = "Pedir Cuenta";
            btnCuenta.Location = new System.Drawing.Point(30, 450);
            btnCuenta.Size = new System.Drawing.Size(160, 50);
            btnCuenta.Click += async (s, e) => await PedirCuentaAsync();

            // Un poco mas alto para que quepa la segunda fila de botones.
            ClientSize = new System.Drawing.Size(740, 600);
            btnMesero.Text = "Llamar al Mesero";
            btnMesero.Location = new System.Drawing.Point(30, 510);
            btnMesero.Size = new System.Drawing.Size(160, 50);
            btnMesero.Click += async (s, e) => await Acciones.LlamarMeseroAsync();

            barra.Style = ProgressBarStyle.Marquee;
            barra.MarqueeAnimationSpeed = 30;
            barra.Location = new System.Drawing.Point(30, 575);
            barra.Size = new System.Drawing.Size(560, 12);
            barra.Visible = false;

            btnCancelar.Text = "Cancelar";
            btnCancelar.Location = new System.Drawing.Point(610, 450);
            btnCancelar.Size = new System.Drawing.Size(100, 50);
            btnCancelar.Visible = false;
            btnCancelar.Click += (s, e) => _cts.Cancel();

            lblEstado.AutoSize = true;
            lblEstado.Location = new System.Drawing.Point(430, 405);

            Controls.Add(btnCuenta);
            Controls.Add(btnMesero);
            Controls.Add(barra);
            Controls.Add(btnCancelar);
            Controls.Add(lblEstado);
        }

        private void CargarPedido()
        {
            dgvPedido.Rows.Clear();

            foreach (var item in pedido.Items)
            {
                decimal subtotal = item.Precio * item.Cantidad;
                dgvPedido.Rows.Add(item.Nombre, item.Categoria, Moneda.Texto(item.Precio), item.Cantidad, Moneda.Texto(subtotal));
            }

            labGranTotal.Text = "Total a Pagar: " + Moneda.Texto(pedido.Total);
        }

        // Mientras se guarda: la ventana sigue viva, se muestra progreso y se puede cancelar.
        private void Ocupado(bool ocupado, string mensaje)
        {
            barra.Visible = ocupado;
            btnCancelar.Visible = ocupado;
            btnFinalizarPedido.Enabled = !ocupado;
            btnVolverResumen.Enabled = !ocupado;
            btnCuenta.Enabled = !ocupado;
            lblEstado.Text = mensaje;
        }

        private async void btnFinalizarPedido_Click(object sender, EventArgs e)
        {
            if (!pedido.Items.Any())
            {
                MessageBox.Show("El pedido esta vacio. Agrega productos antes de finalizar.");
                return;
            }

            decimal total = pedido.Total;
            _cts = new CancellationTokenSource();
            Ocupado(true, "Enviando pedido...");
            try
            {
                int idPedido = await _servicio.EnviarAsync(_cts.Token);
                MessageBox.Show("Pedido enviado a cocina. Total: " + Moneda.Texto(total) +
                                "\nPedido #" + idPedido, "Pedido confirmado");
                Navegacion.VolverAlMenu(this);
            }
            catch (OperationCanceledException)
            {
                if (!IsDisposed) { Ocupado(false, "Envio cancelado."); }
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                {
                    Ocupado(false, "");
                    CargarPedido();   // el carrito pudo cambiar (productos quitados / precios nuevos)
                    MessageBox.Show("No se pudo enviar el pedido:\n" + ex.Message, "Error");
                }
            }
        }

        // La logica (confirmar, validar, marcar la cuenta y bloquear la mesa) esta en Acciones,
        // para que el mismo boton funcione igual desde cualquier pantalla.
        private async System.Threading.Tasks.Task PedirCuentaAsync()
        {
            btnCuenta.Enabled = false;
            try { await Acciones.PedirCuentaAsync(); }
            finally { if (!IsDisposed) btnCuenta.Enabled = true; }
        }

        private void btnVolverResumen_Click(object sender, EventArgs e)
        {
            Navegacion.VolverAlMenu(this);
        }
    }
}
