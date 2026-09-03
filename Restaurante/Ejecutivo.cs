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

        }
    }
}
