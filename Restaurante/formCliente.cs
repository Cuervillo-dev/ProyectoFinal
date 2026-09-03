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
    public partial class formCliente : Form
    {
        public formCliente()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ejecutivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ejecutivo ejecutivo = new Ejecutivo();
            ejecutivo.Show();
            this.Hide();
        }

        private void bebidaNaturalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BebidasNaturales natural = new BebidasNaturales();
            natural.Show();
            this.Hide();
        }

        private void bebidasAlcoholicasToolStripMenuItem_Click(object sender, EventArgs e)
        {

            BebidasAlcoholicas alcoholicas = new BebidasAlcoholicas();
            alcoholicas.Show();
            this.Hide();
        }

        private void pToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Postres postre = new Postres();
            postre.Show();
            this.Hide();
        }
    }
}
