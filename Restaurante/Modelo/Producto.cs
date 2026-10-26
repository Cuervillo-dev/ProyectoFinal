namespace Restaurante.Modelo
{
    // Entidad: una fila de la tabla Productos (solo los activos).
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }
    }
}
