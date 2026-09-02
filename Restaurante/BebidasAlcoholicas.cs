using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
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
            labtoña.Text = (precio * toña.Value).ToString ("C");
        } 
        private void victoria_ValueChanged(object sender, EventArgs e)
        {
            decimal precio = 40;
            labvictoria.Text = (precio *  victoria.Value).ToString ("C");
        }

        private void corona_ValueChanged(object sender, EventArgs e)
        {
            decimal precio = 80;
            labcorona.Text = (precio * corona.Value).ToString ("C");
        }

            private void btnGuardar_Click(object sender, EventArgs e)
        {

        if (toña.Value > 0)
        {
            pedido.Items.Add(new ItemPedido
            {
                Nombre = "Toña",
                Categoria = "Bebida",
                Precio = 50m,
                Cantidad = (int)toña.Value
            });
        }

        if (victoria.Value > 0)
        {
            pedido.Items.Add(new ItemPedido
            {
                Nombre = "Victoria",
                Categoria = "Bebida",
                Precio = 40m,
                Cantidad = (int)victoria.Value
            });
        }

        if (corona.Value > 0)
        {
            pedido.Items.Add(new ItemPedido
            {
                Nombre = "Corona",
                Categoria = "Bebida",
                Precio = 80m,
                Cantidad = (int)corona.Value
            });
        }

        MessageBox.Show("Bebidas agregadas al pedido");

        // Resetear los numeric para evitar que se agregue dos veces si presiona otra vez
        toña.Value = 0;
        victoria.Value = 0;
        corona.Value = 0;
        }

    }
}
