using System;
using System.Windows.Forms;
using Restaurante.Presentacion;

namespace Restaurante
{
    public partial class Ejecutivo : Form
    {
        private readonly MenuController _menu;

        public Ejecutivo()
        {
            InitializeComponent();

            // Los nombres deben coincidir con la columna nombre de la tabla Productos.
            _menu = new MenuController(this, "Ejecutivo", btnTotalE);
            _menu.Agregar(alitapicante, labalitapicante, "Alitas Extra Picante");
            _menu.Agregar(alitaPeque, labalitaPeque, "Alitas Pequeñas");
            _menu.Agregar(alitaGrande, labalitaGrande, "Alitas Grande");
            _menu.Agregar(alitaPapasFritas, labalitaPapasFritas, "Alitas Pequeña + Papas Fritas");
            _menu.Agregar(alitasArroz, labalitasArroz, "Alitas + Arroz + Salsa Picante");
            _menu.Agregar(alitaPapaAsada, labalitaPapaAsada, "Alitas + Papa Asada");

            Load += async (s, e) => await _menu.IniciarAsync();
        }

        private void labalitaPeque_Click(object sender, EventArgs e)
        {
        }

        private void labalitasArroz_Click(object sender, EventArgs e)
        {
        }

        private void btnVolveralMenuE_Click(object sender, EventArgs e)
        {
            Navegacion.VolverAlMenu(this);
        }

        private void btnTotalE_Click(object sender, EventArgs e)
        {
            _menu.AgregarAlPedido();
        }

        private void alitaGrande_ValueChanged_1(object sender, EventArgs e)
        {
        }
    }
}
