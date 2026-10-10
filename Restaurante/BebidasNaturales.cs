using System;
using System.Windows.Forms;
using Restaurante.Presentacion;

namespace Restaurante
{
    public partial class BebidasNaturales : Form
    {
        private readonly MenuController _menu;

        public BebidasNaturales()
        {
            InitializeComponent();

            _menu = new MenuController(this, "Bebida Natural", btnTotalN);
            _menu.Agregar(cacao, labcacao, "Cacao");
            _menu.Agregar(limonada, lablimonada, "Limonada");
            _menu.Agregar(fresa, labfresa, "Fresa");
            _menu.Agregar(te, labte, "Té Helado");

            Load += async (s, e) => await _menu.IniciarAsync();
        }

        private void btnVolveralMenuN_Click(object sender, EventArgs e)
        {
            Navegacion.VolverAlMenu(this);
        }

        private void btnTotalN_Click(object sender, EventArgs e)
        {
            _menu.AgregarAlPedido();
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }
    }
}
