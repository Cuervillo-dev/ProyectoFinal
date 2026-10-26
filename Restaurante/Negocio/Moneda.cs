using System.Globalization;

namespace Restaurante.Negocio
{
    // Formato fijo en cordobas ("C$ 1,500.00"). Antes se usaba ToString("C"), que depende de la
    // configuracion regional de Windows y en algunas computadoras mostraba euros.
    public static class Moneda
    {
        public static string Texto(decimal monto)
        {
            return "C$ " + monto.ToString("N2", CultureInfo.InvariantCulture);
        }
    }
}
