using System;
using System.Windows.Forms;

namespace Restaurante
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ApplicationContext permite cambiar el formulario principal al navegar.
            var inicio = new FormInicio();
            Navegacion.Contexto = new ApplicationContext(inicio);
            Navegacion.Registrar(inicio);

            // Vigila la mesa del cliente (reinicio al liberar) y sincroniza el menu cada 5 s.
            Restaurante.Negocio.SesionMesa.Iniciar();

            Application.Run(Navegacion.Contexto);
        }
    }
}
