using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Restaurante.Negocio;

namespace Restaurante
{
    public partial class formCliente : Form
    {
        // Las categorias del menu salen de la tabla Productos: si el Mesero crea una categoria
        // nueva (o deja una sin productos), el menu de arriba se actualiza solo.
        private readonly List<ToolStripMenuItem> _itemsCategoria = new List<ToolStripMenuItem>();
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public formCliente()
        {
            InitializeComponent();
            Text = pedido.NumeroMesa > 0 ? "Food Zone - Mesa " + pedido.NumeroMesa : "Food Zone";

            ConstruirMenu();
            AgregarAccion("Pedir Cuenta", async (s, e) => await Acciones.PedirCuentaAsync());
            AgregarAccion("Llamar al Mesero", async (s, e) => await Acciones.LlamarMeseroAsync());
            CatalogoService.Cambio += CatalogoCambio;
            FormClosed += (s, e) =>
            {
                CatalogoService.Cambio -= CatalogoCambio;
                _cts.Cancel();
            };

            Load += async (s, e) =>
            {
                try { await CatalogoService.ActualizarAsync(_cts.Token); }
                catch (OperationCanceledException) { }
                catch (Exception ex)
                {
                    if (!IsDisposed)
                        MessageBox.Show("No se pudo cargar el menu desde la base de datos:\n" + ex.Message);
                }
            };
        }

        // Botones fijos al final del menu: siempre a la vista, sin importar cuantas categorias haya.
        private void AgregarAccion(string texto, EventHandler accion)
        {
            var item = new ToolStripMenuItem(texto)
            {
                Font = verPedidoToolStripMenuItem.Font,
                ForeColor = Color.Peru
            };
            item.Click += accion;
            menuStrip1.Items.Add(item);
        }

        private void CatalogoCambio(object sender, EventArgs e)
        {
            if (!IsDisposed) ConstruirMenu();
        }

        private void ConstruirMenu()
        {
            menuStrip1.SuspendLayout();
            foreach (ToolStripMenuItem viejo in _itemsCategoria)
            {
                menuStrip1.Items.Remove(viejo);
                viejo.Dispose();
            }
            _itemsCategoria.Clear();

            int posicion = 0;   // antes de "Ver Pedido"
            foreach (string cat in CatalogoService.Categorias())
            {
                string categoria = cat;
                var item = new ToolStripMenuItem(categoria)
                {
                    Font = verPedidoToolStripMenuItem.Font,
                    ForeColor = Color.SeaShell
                };
                item.Click += (s, e) => Navegacion.IrA(this, new MenuCategoria(categoria));
                menuStrip1.Items.Insert(posicion++, item);
                _itemsCategoria.Add(item);
            }
            menuStrip1.ResumeLayout();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void verPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navegacion.IrA(this, new ResumenPedido());
        }
    }
}
