namespace Restaurante
{
    partial class ResumenPedido
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dgvPedido = new System.Windows.Forms.DataGridView();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labGranTotal = new System.Windows.Forms.Label();
            this.btnFinalizarPedido = new System.Windows.Forms.Button();
            this.btnVolverResumen = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedido)).BeginInit();
            this.SuspendLayout();
            //
            // dgvPedido
            //
            this.dgvPedido.AllowUserToAddRows = false;
            this.dgvPedido.AllowUserToDeleteRows = false;
            this.dgvPedido.ReadOnly = true;
            this.dgvPedido.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNombre,
            this.colCategoria,
            this.colPrecio,
            this.colCantidad,
            this.colSubtotal});
            this.dgvPedido.Location = new System.Drawing.Point(30, 30);
            this.dgvPedido.Name = "dgvPedido";
            this.dgvPedido.RowHeadersWidth = 30;
            this.dgvPedido.Size = new System.Drawing.Size(680, 350);
            this.dgvPedido.TabIndex = 0;
            //
            // colNombre
            //
            this.colNombre.HeaderText = "Producto";
            this.colNombre.Name = "colNombre";
            this.colNombre.ReadOnly = true;
            this.colNombre.Width = 160;
            //
            // colCategoria
            //
            this.colCategoria.HeaderText = "Categoria";
            this.colCategoria.Name = "colCategoria";
            this.colCategoria.ReadOnly = true;
            this.colCategoria.Width = 140;
            //
            // colPrecio
            //
            this.colPrecio.HeaderText = "Precio";
            this.colPrecio.Name = "colPrecio";
            this.colPrecio.ReadOnly = true;
            this.colPrecio.Width = 100;
            //
            // colCantidad
            //
            this.colCantidad.HeaderText = "Cantidad";
            this.colCantidad.Name = "colCantidad";
            this.colCantidad.ReadOnly = true;
            this.colCantidad.Width = 90;
            //
            // colSubtotal
            //
            this.colSubtotal.HeaderText = "Subtotal";
            this.colSubtotal.Name = "colSubtotal";
            this.colSubtotal.ReadOnly = true;
            this.colSubtotal.Width = 110;
            //
            // labGranTotal
            //
            this.labGranTotal.AutoSize = true;
            this.labGranTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.labGranTotal.Location = new System.Drawing.Point(30, 400);
            this.labGranTotal.Name = "labGranTotal";
            this.labGranTotal.Size = new System.Drawing.Size(200, 30);
            this.labGranTotal.TabIndex = 1;
            this.labGranTotal.Text = "Total a Pagar:";
            //
            // btnFinalizarPedido
            //
            this.btnFinalizarPedido.Location = new System.Drawing.Point(430, 450);
            this.btnFinalizarPedido.Name = "btnFinalizarPedido";
            this.btnFinalizarPedido.Size = new System.Drawing.Size(160, 50);
            this.btnFinalizarPedido.TabIndex = 2;
            this.btnFinalizarPedido.Text = "Finalizar Pedido";
            this.btnFinalizarPedido.UseVisualStyleBackColor = true;
            this.btnFinalizarPedido.Click += new System.EventHandler(this.btnFinalizarPedido_Click);
            //
            // btnVolverResumen
            //
            this.btnVolverResumen.Location = new System.Drawing.Point(230, 450);
            this.btnVolverResumen.Name = "btnVolverResumen";
            this.btnVolverResumen.Size = new System.Drawing.Size(160, 50);
            this.btnVolverResumen.TabIndex = 3;
            this.btnVolverResumen.Text = "Seguir Ordenando";
            this.btnVolverResumen.UseVisualStyleBackColor = true;
            this.btnVolverResumen.Click += new System.EventHandler(this.btnVolverResumen_Click);
            //
            // ResumenPedido
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 540);
            this.Controls.Add(this.btnVolverResumen);
            this.Controls.Add(this.btnFinalizarPedido);
            this.Controls.Add(this.labGranTotal);
            this.Controls.Add(this.dgvPedido);
            this.Name = "ResumenPedido";
            this.Text = "Resumen del Pedido";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedido)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubtotal;
        private System.Windows.Forms.Label labGranTotal;
        private System.Windows.Forms.Button btnFinalizarPedido;
        private System.Windows.Forms.Button btnVolverResumen;
    }
}
