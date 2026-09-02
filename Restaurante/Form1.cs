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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btoCliente.BackColor = ColorTranslator.FromHtml("#00CED1");
        }

        private void btoCliente_Click(object sender, EventArgs e)
        {
            formCliente formClientes = new formCliente();
            formClientes.Show(); 
            this.Hide();
        }
    }
}
