using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurante
{
    public partial class Postres : Form
    {
        public Postres()
        {
            InitializeComponent();
            pastel.ValueChanged += pastel_ValueChanged;
            panquei.ValueChanged += panquei_ValueChanged;
            arrozLeche.ValueChanged += arrozLeche_ValueChanged;
        }

        private void btnVolverP_Click(object sender, EventArgs e)
        {
            formCliente formClientes = new formCliente();
            formClientes.Show();
            this.Hide();
        }

        private void pastel_ValueChanged(object sender, EventArgs e)
        {
            decimal precio = 80m;
            labpastel.Text = (precio * pastel.Value).ToString("C");
        }

        private void panquei_ValueChanged(object sender, EventArgs e)
        {
            decimal precio = 55m;
            labpanquei.Text = (precio * panquei.Value).ToString("C");
        }
        private void arrozLeche_ValueChanged(object sender, EventArgs e)
        {
            decimal precio = 30m;
            labarrozLeche.Text = (precio * arrozLeche.Value).ToString("C");
        }

        private void btnTotalP_Click(object sender, EventArgs e)
        {
            decimal subtotal = 0;

            if (pastel.Value > 0)
            {
                pedido.Items.Add(new ItemPedido { Nombre = "Pastel", Categoria = "Postre", Precio = 80m, Cantidad = (int)pastel.Value });
                subtotal += 80m * pastel.Value;
            }
            if (panquei.Value > 0)
            {
                pedido.Items.Add(new ItemPedido { Nombre = "Panque", Categoria = "Postre", Precio = 55m, Cantidad = (int)panquei.Value });
                subtotal += 55m * panquei.Value;
            }
            if (arrozLeche.Value > 0)
            {
                pedido.Items.Add(new ItemPedido { Nombre = "Arroz con Leche", Categoria = "Postre", Precio = 30m, Cantidad = (int)arrozLeche.Value });
                subtotal += 30m * arrozLeche.Value;
            }

            if (subtotal > 0)
            {
                MessageBox.Show("Postres agregados al pedido. Subtotal: " + subtotal.ToString("C"));
            }

            pastel.Value = 0;
            panquei.Value = 0;
            arrozLeche.Value = 0;
        }
    }
}
