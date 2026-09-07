using System;
using System.Linq;
using System.Windows.Forms;

namespace Restaurante
{
    public partial class ResumenPedido : Form
    {
        public ResumenPedido()
        {
            InitializeComponent();
            CargarPedido();
        }

        private void CargarPedido()
        {
            dgvPedido.Rows.Clear();

            foreach (var item in pedido.Items)
            {
                decimal subtotal = item.Precio * item.Cantidad;
                dgvPedido.Rows.Add(item.Nombre, item.Categoria, item.Precio.ToString("C"), item.Cantidad, subtotal.ToString("C"));
            }

            labGranTotal.Text = "Total a Pagar: " + pedido.Total.ToString("C");
        }

        private void btnFinalizarPedido_Click(object sender, EventArgs e)
        {
            if (!pedido.Items.Any())
            {
                MessageBox.Show("El pedido esta vacio. Agrega productos antes de finalizar.");
                return;
            }

            // TODO: aqui se conectara con la base de datos para guardar el pedido
            // (tabla cabecera con Nfactura + tabla detalle por cada item)
            MessageBox.Show("Pedido confirmado. Total: " + pedido.Total.ToString("C"));

            pedido.Items.Clear();

            formCliente formClientes = new formCliente();
            formClientes.Show();
            this.Hide();
        }

        private void btnVolverResumen_Click(object sender, EventArgs e)
        {
            formCliente formClientes = new formCliente();
            formClientes.Show();
            this.Hide();
        }
    }
}
