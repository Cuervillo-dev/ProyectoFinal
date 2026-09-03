using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurante
{
    public partial class BebidasNaturales : Form
    {
        public BebidasNaturales()
        {
            InitializeComponent();
            cacao.ValueChanged += cacao_ValueChanged;
            limonada.ValueChanged += limonada_ValueChanged;
            fresa.ValueChanged += fresa_ValueChanged;
            te.ValueChanged += te_ValueChanged;

        }

      
        private void cacao_ValueChanged(object sender, EventArgs e)
        {
            decimal precio = 50;
            labcacao.Text = (precio * cacao.Value).ToString("C");
        
        }
         private void limonada_ValueChanged(object sender, EventArgs e)
        {
            decimal precio = 35;
            lablimonada.Text = (precio * limonada.Value).ToString("C");
        
        }
         private void fresa_ValueChanged(object sender, EventArgs e)
        {
            decimal precio = 45;
            labfresa.Text = (precio * fresa.Value).ToString("C");
        
        }
         private void te_ValueChanged(object sender, EventArgs e)
        {
            decimal precio = 50;
            labte.Text = (precio * te.Value).ToString("C");
        
        }

        
        private void btnVolveralMenuN_Click(object sender, EventArgs e)
        {
            formCliente formClientes = new formCliente();
            formClientes.Show();
            this.Hide();
        }

        private void btnTotalN_Click(object sender, EventArgs e)
        {
            if(cacao.Value > 0)
            {
                pedido.Items.add(new ItemPedido 
                {
                    Nombre = "Cacao",
                    Categoria = "Bebida",
                    Precio = 50m,
                    Cantidad = (int)cacao.Value

                });


            }

            if(limonada.Value > 0)
            {
                pedido.Items.add(new ItemPedido 
                {
                    Nombre = "Limonada",
                    Categoria = "Bebida",
                    Precio = 35m,
                    Cantidad = (int)limonada.Value

                });


            }

            if(fresa.Value > 0)
            {
                pedido.Items.add(new ItemPedido 
                {
                    Nombre = "Fresa",
                    Categoria = "Bebida",
                    Precio = 45m,
                    Cantidad = (int)fresa.Value

                });


            }

            if(te.Value > 0)
            {
                pedido.Items.add(new ItemPedido 
                {
                    Nombre = "Te Elado",
                    Categoria = "Bebida",
                    Precio = 45m,
                    Cantidad = (int)te.Value

                });


            }

            labatotsl.Text =(cacao_Value + limonada.Value + fresa.Value  );
           
            MessageBox.Show("Bebidas agregadas al pedido ");
            

            toña.Value = 0;
            victoria.Value = 0;
            corona.Value = 0;

            
                





        }

       
       
    }
}
