using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Restaurante.Modelo;
using Restaurante.Negocio;

namespace Restaurante.Presentacion
{
    // Una linea de un formulario de menu: control de cantidad + etiqueta de subtotal + producto de la BD.
    public class LineaMenu
    {
        public NumericUpDown Cantidad;
        public Label Subtotal;
        public string Producto;   // nombre tal como esta en la tabla Productos
    }

    // Logica comun de los 4 formularios de menu (Ejecutivo, Naturales, Alcoholicas, Postres).
    // Los precios y el idProducto vienen de la BD; el catalogo se descarga en segundo plano
    // y el formulario sigue respondiendo mientras tanto.
    public class MenuController
    {
        private readonly Form _form;
        private readonly string _categoria;
        private readonly List<LineaMenu> _lineas = new List<LineaMenu>();
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly Button _botonAgregar;

        public MenuController(Form form, string categoria, Button botonAgregar)
        {
            _form = form;
            _categoria = categoria;
            _botonAgregar = botonAgregar;
            _form.FormClosed += (s, e) => _cts.Cancel();   // cancela la descarga si se cierra la ventana
        }

        public void Agregar(NumericUpDown cantidad, Label subtotal, string productoBD)
        {
            var linea = new LineaMenu { Cantidad = cantidad, Subtotal = subtotal, Producto = productoBD };
            _lineas.Add(linea);
            cantidad.ValueChanged += (s, e) => Recalcular(linea);
        }

        // Se llama desde el evento Load del formulario.
        public async Task IniciarAsync()
        {
            _botonAgregar.Enabled = false;
            string tituloOriginal = _form.Text;
            _form.Text = tituloOriginal + " - cargando precios...";
            try
            {
                await CatalogoService.CargarAsync(_cts.Token);

                string faltan = "";
                foreach (var l in _lineas)
                    if (CatalogoService.Buscar(l.Producto) == null)
                        faltan += "\n - " + l.Producto;

                if (faltan.Length > 0)
                    MessageBox.Show("Estos productos no estan en la tabla Productos de la base de datos:" + faltan +
                                    "\n\nEjecuta productos.sql en DataGrip.", "Catalogo incompleto");

                foreach (var l in _lineas) Recalcular(l);
                _botonAgregar.Enabled = true;
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar el menu desde la base de datos:\n" + ex.Message);
            }
            finally
            {
                if (!_form.IsDisposed) _form.Text = tituloOriginal;
            }
        }

        private void Recalcular(LineaMenu l)
        {
            var p = CatalogoService.Buscar(l.Producto);
            decimal precio = p != null ? p.Precio : 0m;
            l.Subtotal.Text = (precio * l.Cantidad.Value).ToString("C");
        }

        // Boton "Agregar al Pedido": pasa lo seleccionado al carrito.
        public void AgregarAlPedido()
        {
            decimal subtotal = 0;
            foreach (var l in _lineas)
            {
                if (l.Cantidad.Value <= 0) continue;

                var p = CatalogoService.Buscar(l.Producto);
                if (p == null)
                {
                    MessageBox.Show("El producto \"" + l.Producto + "\" no existe en la base de datos.");
                    continue;
                }

                pedido.Items.Add(new ItemPedido
                {
                    IdProducto = p.IdProducto,
                    Nombre = p.Nombre,
                    Categoria = _categoria,
                    Precio = p.Precio,
                    Cantidad = (int)l.Cantidad.Value
                });
                subtotal += p.Precio * l.Cantidad.Value;
            }

            if (subtotal > 0)
                MessageBox.Show("Agregado al pedido. Subtotal: " + subtotal.ToString("C"));

            foreach (var l in _lineas) l.Cantidad.Value = 0;
        }
    }
}
