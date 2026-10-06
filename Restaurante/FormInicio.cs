using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Restaurante
{
    // Pantalla de inicio de la app Cliente: muestra las mesas y deja elegir una LIBRE.
    public class FormInicio : Form
    {
        private readonly FlowLayoutPanel panelMesas = new FlowLayoutPanel();
        private readonly Label lblTitulo = new Label();
        private readonly Label lblInfo = new Label();
        private readonly Button btnRefrescar = new Button();
        private readonly Timer timerRefresco = new Timer();

        public FormInicio()
        {
            Text = "Food Zone Restaurante";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(640, 460);
            BackColor = Color.White;

            lblTitulo.Text = "Food Zone Restaurante";
            lblTitulo.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Height = 60;
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            lblInfo.Text = "Selecciona tu mesa (las mesas en verde están disponibles)";
            lblInfo.Font = new Font("Segoe UI", 11);
            lblInfo.Dock = DockStyle.Top;
            lblInfo.Height = 36;
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;

            btnRefrescar.Text = "Refrescar";
            btnRefrescar.Dock = DockStyle.Bottom;
            btnRefrescar.Height = 40;
            btnRefrescar.Click += (s, e) => CargarMesas();

            panelMesas.Dock = DockStyle.Fill;
            panelMesas.Padding = new Padding(20);
            panelMesas.AutoScroll = true;

            // Orden importa: Fill primero, luego los Dock top/bottom
            Controls.Add(panelMesas);
            Controls.Add(lblInfo);
            Controls.Add(lblTitulo);
            Controls.Add(btnRefrescar);

            // Refresco automático cada 10 s (otras mesas pueden ocuparse mientras tanto)
            timerRefresco.Interval = 10000;
            timerRefresco.Tick += (s, e) => CargarMesas();

            Load += (s, e) => { CargarMesas(); timerRefresco.Start(); };
            FormClosed += (s, e) => timerRefresco.Stop();
        }

        // TODO: reemplaza por tu clase de conexión existente.
        // TiDB Cloud exige SSL: SslMode=Required (o VerifyCA con el certificado).
        private MySqlConnection AbrirConexion()
        {
            string cs = "Server=TU_HOST;Port=4000;Database=proyectoDB;Uid=TU_USER;Pwd=TU_PASS;SslMode=Required;";
            var con = new MySqlConnection(cs);
            con.Open();
            return con;
        }

        private void CargarMesas()
        {
            try
            {
                panelMesas.SuspendLayout();
                panelMesas.Controls.Clear();

                using (var con = AbrirConexion())
                using (var cmd = new MySqlCommand(
                    "SELECT idMesa, numeroMesa, estado FROM Mesas ORDER BY numeroMesa", con))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        int idMesa = rd.GetInt32("idMesa");
                        int numero = rd.GetInt32("numeroMesa");
                        bool libre = rd.GetString("estado") == "LIBRE";
                        panelMesas.Controls.Add(CrearTarjeta(idMesa, numero, libre));
                    }
                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = "No se pudo conectar: " + ex.Message;
            }
            finally
            {
                panelMesas.ResumeLayout();
            }
        }

        private Button CrearTarjeta(int idMesa, int numero, bool libre)
        {
            var btn = new Button
            {
                Width = 130,
                Height = 110,
                Margin = new Padding(12),
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = libre ? Color.FromArgb(46, 160, 67) : Color.FromArgb(190, 60, 60),
                Text = "Mesa " + numero + "\n" + (libre ? "Disponible" : "Ocupada"),
                Enabled = libre
            };
            btn.FlatAppearance.BorderSize = 0;
            if (libre)
                btn.Click += (s, e) => ElegirMesa(idMesa, numero);
            return btn;
        }

        private void ElegirMesa(int idMesa, int numero)
        {
            try
            {
                // UPDATE condicional: si otro cliente la tomó justo antes, no afecta filas
                using (var con = AbrirConexion())
                using (var cmd = new MySqlCommand(
                    "UPDATE Mesas SET estado = 'OCUPADA' WHERE idMesa = @id AND estado = 'LIBRE'", con))
                {
                    cmd.Parameters.AddWithValue("@id", idMesa);
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        MessageBox.Show("Esa mesa ya fue ocupada. Elige otra.", "Mesa no disponible");
                        CargarMesas();
                        return;
                    }
                }

                timerRefresco.Stop();
                Hide();

                
   

                var siguiente = new Form1();              
                siguiente.FormClosed += (s, e) => Close(); 
                siguiente.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al elegir la mesa: " + ex.Message);
            }
        }
    }
}