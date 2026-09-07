using System.Windows.Forms;

namespace Restaurante
{
    public static class Navegacion
    {
        // Método reutilizable para regresar al menú principal
        public static void VolverAlMenu(Form formularioActual)
        {
            formCliente menuPrincipal = new formCliente();
            menuPrincipal.Show();

            formularioActual.Close();
        }
    }
}
