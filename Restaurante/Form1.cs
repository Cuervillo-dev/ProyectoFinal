using System;
using System.Windows.Forms;

namespace Restaurante
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btoCliente_Click(object sender, EventArgs e)
        {
            Navegacion.IrA(this, new formCliente());
        }
    }
}
