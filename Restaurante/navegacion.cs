using System.Windows.Forms;

namespace Restaurante
{
    // Navegacion entre formularios. Cierra el formulario anterior (en vez de ocultarlo)
    // y pasa el "formulario principal" al nuevo, asi la aplicacion termina cuando
    // el usuario cierra la ventana en la que esta y no quedan procesos escondidos.
    //
    // Ademas protege la sesion: mientras el cliente tiene una mesa asignada no puede cerrar la
    // ventana (X / Alt+F4) ni hay camino de regreso a la pantalla de mesas. La unica salida es
    // que el Mesero libere la mesa (ver SesionMesa).
    public static class Navegacion
    {
        public static ApplicationContext Contexto { get; set; }

        // Formulario que se esta mostrando ahora.
        public static Form Actual { get; private set; }

        private static bool _navegando;

        // Para el primer formulario (Program.Main) y para todo el que se abre con IrA.
        public static void Registrar(Form form)
        {
            Actual = form;
            form.FormClosing += ProtegerCierre;
        }

        public static void IrA(Form actual, Form siguiente)
        {
            Registrar(siguiente);
            if (Contexto != null) Contexto.MainForm = siguiente;
            siguiente.Show();
            if (actual != null)
            {
                _navegando = true;   // cierre hecho por el programa: se permite
                try { actual.Close(); }
                finally { _navegando = false; }
            }
        }

        // Metodo reutilizable para regresar al menu principal
        public static void VolverAlMenu(Form actual)
        {
            IrA(actual, new formCliente());
        }

        private static void ProtegerCierre(object sender, FormClosingEventArgs e)
        {
            if (_navegando) return;
            if (pedido.IdMesa != 0 && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                MessageBox.Show("Tu mesa sigue activa. Si necesitas ayuda, usa el boton \"Llamar al Mesero\".", "Food Zone");
            }
        }
    }
}
