namespace Restaurante.Modelo
{
    // Entidad: una fila de la tabla Mesas.
    public class Mesa
    {
        public int IdMesa { get; set; }
        public int Numero { get; set; }
        public string Estado { get; set; }
        public bool Libre { get { return Estado == "LIBRE"; } }
    }
}
