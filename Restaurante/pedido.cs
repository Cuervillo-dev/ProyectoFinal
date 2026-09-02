using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurante
{

    public static class pedido
    {
        public static List<ItemPedido> Items = new List<ItemPedido>();
        public static decimal Total => Items.Sum(i => i.Precio * i.Cantidad);

        
    }

    public class ItemPedido
    {
        public string Nombre { get; set; }
        public string Categoria { get; set; } 
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
    
}
