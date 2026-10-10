using System;
using System.Windows.Forms;
using Restaurante.Presentacion;

namespace Restaurante
{
    public partial class BebidasAlcoholicas : Form
    {
        private readonly MenuController _menu;

        public BebidasAlcoholicas()
        {
            InitializeComponent();

            _menu = new MenuController(this, "Bebida Alcoholica", btnCalcularTotal);
            _menu.Agregar(toña, labtoña, "Toña");
            _menu.Agregar(victoria, labvictoria, "Victoria");
            _menu.Agregar(corona, labcorona, "Corona");

            Load += async (s, e) => await _menu.IniciarAsync();
        }

        private void labcorona_Click(object sender, EventArgs e)
        {
        }

        private void btnCalcularTotal_Click(object sender, EventArgs e)
        {
            _menu.AgregarAlPedido();
        }

        private void btnVolveralMenu_Click(object sender, EventArgs e)
        {
            Navegacion.VolverAlMenu(this);
        }
    }
}
