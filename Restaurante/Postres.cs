using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurante
{
    public partial class Postres : Form
    {
        public Postres()
        {
            InitializeComponent();
            createBotonManual();
            pastel.ValueChanged += pastel_ValueChanged;
            panquei.ValueChanged += panquei_ValueChanged;
            arrozLeche.ValueChanged += arrozLeche_ValueChanged;
        }

        public void createBotonManual (object sender, EventArgs e)
        {
            btnVolverP = new Button();
            btnVolverP.Name = "btnVolverP";
            btnVolverP.Text = "Volver Al Menu";
            btnVolverP.Size = new System.Drawing.Size(120, 40);
            btnVolverP.Location = new System.Drawing.Point(50, 200); // Coordenadas (X, Y)

            
            btnvolverP.Click += new System.EventHandler(this.btnVolverP_Click);

           
            this.Controls.Add(btnVolverP);
            
        }

        private void btnvolverP (object sender, EventArgs e)
        {
            navegacion.menuPrincipal(this);
             .Close(); 
            
        }

        private void pastel_ValueChanged (object sender, EventArgs e)
        {
            decimal precio = 80m;
            labpastel.Text = (precio * pastel_Value).Tostring ("c");
        }

          private void panquei_ValueChanged (object sender, EventArgs e)
        {
            decimal precio = 55m;
            labpanquei.Text = (precio * panquei_Value).Tostring ("c");
        }
          private void arrozLeche_ValueChanged (object sender, EventArgs e)
        {
            decimal precio = 30m;
            labarrozLeche.Text = (precio * arrozLeche_Value).Tostring ("c");
        }

    }
}
