using System;
using System.Windows.Forms;
using Restaurante.Presentacion;

namespace Restaurante
{
    public partial class Postres : Form
    {
        private readonly MenuController _menu;

        public Postres()
        {
            InitializeComponent();

            _menu = new MenuController(this, "Postre", btnTotalP);
            _menu.Agregar(pastel, labpastel, "Pastel");
            _menu.Agregar(panquei, labpanquei, "Panquei");
            _menu.Agregar(arrozLeche, labarrozLeche, "Arroz de leche");

            Load += async (s, e) => await _menu.IniciarAsync();
        }

        private void btnVolverP_Click(object sender, EventArgs e)
        {
            Navegacion.VolverAlMenu(this);
        }

        private void btnTotalP_Click(object sender, EventArgs e)
        {
            _menu.AgregarAlPedido();
        }
    }
}
