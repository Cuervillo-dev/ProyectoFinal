namespace Restaurante.Modelo
{
    // Una linea del pedido que arma el cliente (luego se guarda en DetallePedidos).
    public class ItemPedido
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
