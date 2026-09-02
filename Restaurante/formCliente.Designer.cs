namespace Restaurante
{
    partial class formCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ejecutivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bebidaNaturalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bebidasAlcoholicasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarPedidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelarPedidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ejecutivoToolStripMenuItem,
            this.bebidaNaturalesToolStripMenuItem,
            this.bebidasAlcoholicasToolStripMenuItem,
            this.pToolStripMenuItem,
            this.extrasToolStripMenuItem,
            this.editarPedidoToolStripMenuItem,
            this.cancelarPedidoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ejecutivoToolStripMenuItem
            // 
            this.ejecutivoToolStripMenuItem.Name = "ejecutivoToolStripMenuItem";
            this.ejecutivoToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.ejecutivoToolStripMenuItem.Text = "Ejecutivo";
            this.ejecutivoToolStripMenuItem.Click += new System.EventHandler(this.ejecutivoToolStripMenuItem_Click);
            // 
            // bebidaNaturalesToolStripMenuItem
            // 
            this.bebidaNaturalesToolStripMenuItem.Name = "bebidaNaturalesToolStripMenuItem";
            this.bebidaNaturalesToolStripMenuItem.Size = new System.Drawing.Size(137, 24);
            this.bebidaNaturalesToolStripMenuItem.Text = "Bebida Naturales";
            this.bebidaNaturalesToolStripMenuItem.Click += new System.EventHandler(this.bebidaNaturalesToolStripMenuItem_Click);
            // 
            // bebidasAlcoholicasToolStripMenuItem
            // 
            this.bebidasAlcoholicasToolStripMenuItem.Name = "bebidasAlcoholicasToolStripMenuItem";
            this.bebidasAlcoholicasToolStripMenuItem.Size = new System.Drawing.Size(156, 24);
            this.bebidasAlcoholicasToolStripMenuItem.Text = "Bebidas Alcoholicas";
            this.bebidasAlcoholicasToolStripMenuItem.Click += new System.EventHandler(this.bebidasAlcoholicasToolStripMenuItem_Click);
            // 
            // pToolStripMenuItem
            // 
            this.pToolStripMenuItem.Name = "pToolStripMenuItem";
            this.pToolStripMenuItem.Size = new System.Drawing.Size(69, 24);
            this.pToolStripMenuItem.Text = "Postres";
            this.pToolStripMenuItem.Click += new System.EventHandler(this.pToolStripMenuItem_Click);
            // 
            // extrasToolStripMenuItem
            // 
            this.extrasToolStripMenuItem.Name = "extrasToolStripMenuItem";
            this.extrasToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.extrasToolStripMenuItem.Text = "Extras";
            // 
            // editarPedidoToolStripMenuItem
            // 
            this.editarPedidoToolStripMenuItem.Name = "editarPedidoToolStripMenuItem";
            this.editarPedidoToolStripMenuItem.Size = new System.Drawing.Size(112, 24);
            this.editarPedidoToolStripMenuItem.Text = "Editar Pedido";
            // 
            // cancelarPedidoToolStripMenuItem
            // 
            this.cancelarPedidoToolStripMenuItem.Name = "cancelarPedidoToolStripMenuItem";
            this.cancelarPedidoToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.cancelarPedidoToolStripMenuItem.Text = "Cancelar Pedido";
            // 
            // formCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "formCliente";
            this.Text = "Form2";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ejecutivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bebidaNaturalesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bebidasAlcoholicasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extrasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarPedidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelarPedidoToolStripMenuItem;
    }
}