using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurante
{
    public partial class Ejecutivo : Form
    {
        public Ejecutivo()
        {
            InitializeComponent();
            alitapicante.ValueChanged += alitapicante_ValueChanged;
            alitaPeque.ValueChanged += alitaPeque_ValueChanged;
            alitaGrande.ValueChanged += alitaGrande_ValueChanged;
            alitaPapasFritas.ValueChanged += alitaPapasFritas_ValueChanged;
            alitasArroz.ValueChanged += alitasArroz_ValueChanged;
            alitaPapaAsada.ValueChanged += alitaPapaAsada_ValueChanged;
            
        }
        private void alitapicante_ValueChanged (object sender, EventArgs e)
        {
            decimal precio = 200m;
            labalitapicante.Text = (precio * alitapicante.Value). ToString ("c");

        }
        private void alitaPeque_ValueChanged(object sender, EventArgs e)
        {
            decimal precio = 150m;
            labalitaPeque.Text = (precio * alitaPeque.Value). ToString ("c");
        }
         private void alitaGrande_ValueChanged(object sender, EventArgs e)
        {
            decimal precio = 180m;
            labalitaGrande.Text = (precio * alitaGrande.Value). ToString ("c");
        }
         private void alitaPapasFritas_ValueChanged(object sender, EventArgs e)
        {
            decimal precio = 160m;
            labalitaPapasFritas.Text = (precio * alitaPapasFritas.Value). ToString ("c");
        }
         private void alitasArroz_ValueChanged(object sender, EventArgs e)
        {
            decimal precio = 130m;
            labalitasArroz.Text = (precio * alitasArroz.Value). ToString ("c");
        }
         private void alitaPapaAsada_ValueChanged(object sender, EventArgs e)
        {
            decimal precio = 150m;
            labalitaPapaAsada.Text = (precio * alitaPapaAsada.Value). ToString ("c");
        }

        private void labalitaPeque_Click(object sender, EventArgs e)
        {

        }

        private void labalitasArroz_Click(object sender, EventArgs e)
        {

        }

        private void btnVolveralMenuE_Click(object sender, EventArgs e)
        {
            formCliente formClientes = new formCliente();
            formClientes.Show();
            this.Hide();
        }

        private void btnTotalE_Click(object sender, EventArgs e)
        {
            decimal subtotal = 0;

            if (alitapicante.Value > 0)
            {
                pedido.Items.Add(new ItemPedido { Nombre = "Alitas Picantes", Categoria = "Ejecutivo", Precio = 200m, Cantidad = (int)alitapicante.Value });
                subtotal += 200m * alitapicante.Value;
            }
            if (alitaPeque.Value > 0)
            {
                pedido.Items.Add(new ItemPedido { Nombre = "Alitas Pequeñas", Categoria = "Ejecutivo", Precio = 150m, Cantidad = (int)alitaPeque.Value });
                subtotal += 150m * alitaPeque.Value;
            }
            if (alitaGrande.Value > 0)
            {
                pedido.Items.Add(new ItemPedido { Nombre = "Alitas Grandes", Categoria = "Ejecutivo", Precio = 180m, Cantidad = (int)alitaGrande.Value });
                subtotal += 180m * alitaGrande.Value;
            }
            if (alitaPapasFritas.Value > 0)
            {
                pedido.Items.Add(new ItemPedido { Nombre = "Alitas con Papas Fritas", Categoria = "Ejecutivo", Precio = 200m, Cantidad = (int)alitaPapasFritas.Value });
                subtotal += 200m * alitaPapasFritas.Value;
            }
            if (alitasArroz.Value > 0)
            {
                pedido.Items.Add(new ItemPedido { Nombre = "Alitas con Arroz", Categoria = "Ejecutivo", Precio = 130m, Cantidad = (int)alitasArroz.Value });
                subtotal += 130m * alitasArroz.Value;
            }
            if (alitaPapaAsada.Value > 0)
            {
                pedido.Items.Add(new ItemPedido { Nombre = "Alitas con Papa Asada", Categoria = "Ejecutivo", Precio = 150m, Cantidad = (int)alitaPapaAsada.Value });
                subtotal += 150m * alitaPapaAsada.Value;
            }

            if (subtotal > 0)
            {
                MessageBox.Show("Platos agregados al pedido. Subtotal: " + subtotal.ToString("C"));
            }

            alitapicante.Value = 0;
            alitaPeque.Value = 0;
            alitaGrande.Value = 0;
            alitaPapasFritas.Value = 0;
            alitasArroz.Value = 0;
            alitaPapaAsada.Value = 0;
        }

        private void alitaGrande_ValueChanged_1(object sender, EventArgs e)
        {

        }
    }
}
