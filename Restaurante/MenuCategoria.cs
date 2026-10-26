using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Restaurante.Modelo;
using Restaurante.Negocio;

namespace Restaurante
{
    // Un solo formulario para TODAS las categorias (Ejecutivo, Bebidas, Postres, ...).
    // Las filas se crean por codigo a partir de la tabla Productos, asi que cuando el Mesero
    // agrega un producto, cambia un precio o desactiva uno, la lista se vuelve a dibujar sola
    // (evento CatalogoService.Cambio) conservando las cantidades que el cliente ya habia marcado
    // y la posicion del scroll.
    public class MenuCategoria : Form
    {
        private readonly string _categoria;
        private readonly Label _lblTitulo = new Label();
        private readonly Label _lblVacio = new Label();
        private readonly FlowLayoutPanel _lista = new FlowLayoutPanel();
        private readonly Button _btnVolver = new Button();
        private readonly Button _btnAgregar = new Button();
        private readonly Button _btnCuenta = new Button();
        private readonly Button _btnMesero = new Button();
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        // IdProducto -> control de cantidad de su fila (solo las filas que estan dibujadas).
        private readonly Dictionary<int, NumericUpDown> _cantidades = new Dictionary<int, NumericUpDown>();
        // IdProducto -> cantidad marcada antes de redibujar.
        private readonly Dictionary<int, int> _guardadas = new Dictionary<int, int>();

        public MenuCategoria(string categoria)
        {
            _categoria = categoria;

            Text = categoria;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1143, 796);
            BackColor = Color.Black;
            BackgroundImage = Properties.Resources.WhatsApp_Image_2026_09_02_at_9_20_30_PM;
            BackgroundImageLayout = ImageLayout.Stretch;
            Font = new Font("Niagara Solid", 18F);

            _lblTitulo.Text = categoria;
            _lblTitulo.ForeColor = Color.Peru;
            _lblTitulo.BackColor = Color.Transparent;
            _lblTitulo.Font = new Font("Niagara Solid", 26F);
            _lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            _lista.FlowDirection = FlowDirection.TopDown;
            _lista.WrapContents = false;
            _lista.AutoScroll = true;
            _lista.BackColor = Color.FromArgb(18, 18, 18);

            _lblVacio.Text = "No hay productos disponibles en esta categoria.";
            _lblVacio.ForeColor = Color.White;
            _lblVacio.BackColor = Color.Transparent;
            _lblVacio.TextAlign = ContentAlignment.MiddleCenter;
            _lblVacio.Visible = false;

            Acciones.EstiloBoton(_btnVolver, "Volver al Menu", 20F);
            Acciones.EstiloBoton(_btnAgregar, "Agregar al Pedido", 20F);
            Acciones.EstiloBoton(_btnCuenta, "Pedir Cuenta", 20F);
            Acciones.EstiloBoton(_btnMesero, "Llamar al Mesero", 20F);
            _btnCuenta.Click += async (s, e) => await Acciones.PedirCuentaAsync();
            _btnMesero.Click += async (s, e) => await Acciones.LlamarMeseroAsync();
            _btnVolver.Click += (s, e) => Navegacion.VolverAlMenu(this);
            _btnAgregar.Click += (s, e) => AgregarAlPedido();

            Controls.Add(_lblVacio);
            Controls.Add(_lista);
            Controls.Add(_lblTitulo);
            Controls.Add(_btnVolver);
            Controls.Add(_btnAgregar);
            Controls.Add(_btnCuenta);
            Controls.Add(_btnMesero);

            Resize += (s, e) => Distribuir();
            Distribuir();

            CatalogoService.Cambio += CatalogoCambio;
            FormClosed += (s, e) =>
            {
                CatalogoService.Cambio -= CatalogoCambio;
                _cts.Cancel();
            };

            Load += async (s, e) =>
            {
                Reconstruir();   // primero lo que ya hay en memoria
                try { await CatalogoService.ActualizarAsync(_cts.Token); }   // y luego lo ultimo de la BD
                catch (OperationCanceledException) { }
                catch (Exception ex)
                {
                    if (!IsDisposed)
                        MessageBox.Show("No se pudo cargar el menu desde la base de datos:\n" + ex.Message);
                }
            };
        }

        // Posiciona todo dentro del marco naranja de la imagen de fondo (proporcional al tamaño).
        private void Distribuir()
        {
            int w = ClientSize.Width, h = ClientSize.Height;
            int margen = (int)(w * 0.05);
            int ancho = w - 2 * margen;
            int altoBoton = 56;

            _lblTitulo.SetBounds(margen, (int)(h * 0.325), ancho, 52);
            int arriba = _lblTitulo.Bottom + 4;
            int abajoLista = (int)(h * 0.885) - altoBoton - 12;
            _lista.SetBounds(margen, arriba, ancho, Math.Max(100, abajoLista - arriba));
            _lblVacio.SetBounds(margen, arriba, ancho, 60);

            int yBotones = _lista.Bottom + 10;
            int separacion = 10;
            int anchoBoton = (ancho - 3 * separacion) / 4;
            _btnVolver.SetBounds(margen, yBotones, anchoBoton, altoBoton);
            _btnAgregar.SetBounds(margen + (anchoBoton + separacion), yBotones, anchoBoton, altoBoton);
            _btnCuenta.SetBounds(margen + 2 * (anchoBoton + separacion), yBotones, anchoBoton, altoBoton);
            _btnMesero.SetBounds(margen + 3 * (anchoBoton + separacion), yBotones, anchoBoton, altoBoton);

            foreach (Control fila in _lista.Controls) fila.Width = AnchoFila();
        }

        private int AnchoFila()
        {
            return Math.Max(300, _lista.Width - SystemInformation.VerticalScrollBarWidth - 6);
        }

        private void CatalogoCambio(object sender, EventArgs e)
        {
            if (!IsDisposed) Reconstruir();
        }

        // Redibuja las filas sin perder lo que el cliente ya habia marcado ni el scroll.
        private void Reconstruir()
        {
            if (IsDisposed) return;

            foreach (var kv in _cantidades) _guardadas[kv.Key] = (int)kv.Value.Value;
            int scroll = -_lista.AutoScrollPosition.Y;

            List<Producto> productos = CatalogoService.DeCategoria(_categoria);

            // Olvidar cantidades de productos que ya no estan en esta categoria.
            var ids = new HashSet<int>(productos.Select(p => p.IdProducto));
            foreach (int id in _guardadas.Keys.Where(k => !ids.Contains(k)).ToList())
                _guardadas.Remove(id);

            _lista.SuspendLayout();
            var viejas = _lista.Controls.Cast<Control>().ToList();
            _lista.Controls.Clear();
            foreach (Control c in viejas) c.Dispose();
            _cantidades.Clear();

            foreach (Producto p in productos)
            {
                int cantidad;
                _guardadas.TryGetValue(p.IdProducto, out cantidad);
                _lista.Controls.Add(CrearFila(p, cantidad));
            }

            _lblVacio.Visible = productos.Count == 0;
            _btnAgregar.Enabled = productos.Count > 0;
            _lista.ResumeLayout(true);
            _lista.AutoScrollPosition = new Point(0, scroll);
        }

        private Panel CrearFila(Producto p, int cantidadInicial)
        {
            var fila = new Panel
            {
                Height = 64,
                Width = AnchoFila(),
                Margin = new Padding(0, 0, 0, 6),
                BackColor = Color.FromArgb(34, 34, 34)
            };

            var tabla = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 1 };
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17F));
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23F));
            tabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var lblNombre = new Label
            {
                Text = p.Nombre,
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 0, 0, 0)
            };
            var lblPrecio = new Label
            {
                Text = Moneda.Texto(p.Precio),
                ForeColor = Color.Peru,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            var num = new NumericUpDown
            {
                Minimum = 0,
                Maximum = 99,
                Anchor = AnchorStyles.None,
                Width = 70,
                BackColor = Color.Black,
                ForeColor = Color.Peru,
                TextAlign = HorizontalAlignment.Center,
                Value = Math.Min(99, Math.Max(0, cantidadInicial))
            };
            var lblSubtotal = new Label
            {
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(0, 0, 12, 0)
            };

            Action actualizar = () => lblSubtotal.Text = Moneda.Texto(p.Precio * num.Value);
            num.ValueChanged += (s, e) => actualizar();
            actualizar();

            tabla.Controls.Add(lblNombre, 0, 0);
            tabla.Controls.Add(lblPrecio, 1, 0);
            tabla.Controls.Add(num, 2, 0);
            tabla.Controls.Add(lblSubtotal, 3, 0);
            fila.Controls.Add(tabla);

            _cantidades[p.IdProducto] = num;
            return fila;
        }

        // Boton "Agregar al Pedido": pasa lo marcado al carrito de la mesa (uno solo para todas las categorias).
        private void AgregarAlPedido()
        {
            decimal subtotal = 0;
            foreach (Producto p in CatalogoService.DeCategoria(_categoria))
            {
                NumericUpDown num;
                if (!_cantidades.TryGetValue(p.IdProducto, out num) || num.Value <= 0) continue;

                int cantidad = (int)num.Value;
                pedido.Agregar(p, cantidad);
                subtotal += p.Precio * cantidad;
                num.Value = 0;
            }
            _guardadas.Clear();

            if (subtotal > 0)
                MessageBox.Show("Agregado al pedido. Subtotal: " + Moneda.Texto(subtotal));
            else
                MessageBox.Show("Marca la cantidad de al menos un producto.");
        }
    }
}
