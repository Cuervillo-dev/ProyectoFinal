using System;
using System.Drawing;
using System.Windows.Forms;

namespace Restaurante
{
    // Pantalla que ve el cliente despues de pedir la cuenta. No tiene botones: no hay forma de
    // salir ni de cambiar de mesa. Cuando el Mesero libera la mesa, SesionMesa la reemplaza
    // sola por la pantalla de mesas para el siguiente cliente.
    public class FormCuenta : Form
    {
        private readonly Panel _tarjeta = new Panel();
        private readonly Button _btnMesero = new Button();

        public FormCuenta()
        {
            Text = "Food Zone - Mesa " + pedido.NumeroMesa;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1143, 796);
            BackgroundImage = Properties.Resources.WhatsApp_Image_2026_09_02_at_9_09_07_PM;
            BackgroundImageLayout = ImageLayout.Stretch;
            Font = new Font("Niagara Solid", 18F);

            _tarjeta.BackColor = Color.Black;
            _tarjeta.Size = new Size(760, 230);

            var titulo = new Label
            {
                Text = "Tu cuenta viene en camino",
                ForeColor = Color.Peru,
                Font = new Font("Niagara Solid", 32F),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 110
            };
            var detalle = new Label
            {
                Text = "Mesa " + pedido.NumeroMesa + "\nGracias por visitarnos",
                ForeColor = Color.White,
                TextAlign = ContentAlignment.TopCenter,
                Dock = DockStyle.Fill
            };
            _tarjeta.Controls.Add(detalle);
            _tarjeta.Controls.Add(titulo);
            Controls.Add(_tarjeta);

            Acciones.EstiloBoton(_btnMesero, "Llamar al Mesero", 22F);
            _btnMesero.Size = new Size(320, 60);
            _btnMesero.Click += async (s, e) => await Acciones.LlamarMeseroAsync();
            Controls.Add(_btnMesero);

            Resize += (s, e) => Centrar();
            Load += (s, e) => Centrar();
        }

        private void Centrar()
        {
            _tarjeta.Left = (ClientSize.Width - _tarjeta.Width) / 2;
            _tarjeta.Top = (ClientSize.Height - _tarjeta.Height) / 2;
            _btnMesero.Left = (ClientSize.Width - _btnMesero.Width) / 2;
            _btnMesero.Top = _tarjeta.Bottom + 20;
        }
    }
}
