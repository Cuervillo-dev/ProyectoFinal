using System.Windows.Forms;

namespace Restaurante
{
    // Navegacion entre formularios. Cierra el formulario anterior (en vez de ocultarlo)
    // y pasa el "formulario principal" al nuevo, asi la aplicacion termina cuando
    // el usuario cierra la ventana en la que esta y no quedan procesos escondidos.
    public static class Navegacion
    {
        public static ApplicationContext Contexto { get; set; }

        public static void IrA(Form actual, Form siguiente)
        {
            if (Contexto != null) Contexto.MainForm = siguiente;
            siguiente.Show();
            if (actual != null) actual.Close();
        }

        // Metodo reutilizable para regresar al menu principal
        public static void VolverAlMenu(Form actual)
        {
            IrA(actual, new formCliente());
        }
    }
}
