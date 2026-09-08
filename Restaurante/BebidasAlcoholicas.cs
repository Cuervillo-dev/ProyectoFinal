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
    public partial class BebidasAlcoholicas : Form
    {
        public BebidasAlcoholicas()
        {
            InitializeComponent();
            toña.ValueChanged += toña_ValueChanged;
            victoria.ValueChanged += victoria_ValueChanged;
            corona.ValueChanged += corona_ValueChanged;
        }

        private void toña_ValueChanged(object sender, EventArgs e)
        {
            decimal precio = 50;
            labtoña.Text = (precio * toña.Value).ToString("C");
        }
        private void victoria_ValueChanged(object sender, EventArgs e)
        {
            decimal precio = 40;
            labvictoria.Text = (precio * victoria.Value).ToString("C");
        }

        private void corona_ValueChanged(object sender, EventArgs e)
        {
            decimal precio = 80;
            labcorona.Text = (precio * corona.Value).ToString("C");
        }

        private void labcorona_Click(object sender, EventArgs e)
        {

        }

        private void btnCalcularTotal_Click(object sender, EventArgs e)
        {
            decimal subtotal = 0;

            if (toña.Value > 0)
            {
                pedido.Items.Add(new ItemPedido
                {
                    Nombre = "Toña",
                    Categoria = "Bebida Alcoholica",
                    Precio = 50m,
                    Cantidad = (int)toña.Value
                });
                subtotal += 50m * toña.Value;
            }

            if (victoria.Value > 0)
            {
                pedido.Items.Add(new ItemPedido
                {
                    Nombre = "Victoria",
                    Categoria = "Bebida Alcoholica",
                    Precio = 40m,
                    Cantidad = (int)victoria.Value
                });
                subtotal += 40m * victoria.Value;
            }

            if (corona.Value > 0)
            {
                pedido.Items.Add(new ItemPedido
                {
                    Nombre = "Corona",
                    Categoria = "Bebida Alcoholica",
                    Precio = 80m,
                    Cantidad = (int)corona.Value
                });
                subtotal += 80m * corona.Value;
            }

            if (subtotal > 0)
            {
                MessageBox.Show("Bebidas agregadas al  pedido " + subtotal.ToString("C"));
            }

            // Resetear los numeric para evitar que se agregue dos veces si presiona otra vez
            toña.Value = 0;
            victoria.Value = 0;
            corona.Value = 0;
        }

        private void btnVolveralMenu_Click(object sender, EventArgs e)
        {
            formCliente formClientes = new formCliente();
            formClientes.Show();
            this.Hide();
        }
    }
}
