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
            Navegacion.Contexto = new ApplicationContext(new FormInicio());
            Application.Run(Navegacion.Contexto);
        }
    }
}
