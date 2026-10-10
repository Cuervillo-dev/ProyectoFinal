using System;
using System.Windows.Forms;

namespace Restaurante
{
    public partial class formCliente : Form
    {
        public formCliente()
        {
            InitializeComponent();
            Text = pedido.NumeroMesa > 0 ? "Food Zone - Mesa " + pedido.NumeroMesa : "Food Zone";
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void ejecutivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navegacion.IrA(this, new Ejecutivo());
        }

        private void bebidaNaturalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navegacion.IrA(this, new BebidasNaturales());
        }

        private void bebidasAlcoholicasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navegacion.IrA(this, new BebidasAlcoholicas());
        }

        private void pToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navegacion.IrA(this, new Postres());
        }

        private void verPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Navegacion.IrA(this, new ResumenPedido());
        }
    }
}
