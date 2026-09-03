using System.Windows.Forms;

namespace Restaurante
{
    public static class Navegacion
    {
        // Método reutilizable para regresar al menú principal
        public static void VolverAlMenu(Form formularioActual)
        {
            FormCliente menuPrincipal = new FormCliente(); 
            menuPrincipal.Show();

            
            formularioActual.Close(); 
        }
    }
}