namespace Restaurante
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btoCliente = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btoCliente
            // 
            this.btoCliente.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btoCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btoCliente.ForeColor = System.Drawing.Color.Peru;
            this.btoCliente.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btoCliente.Location = new System.Drawing.Point(316, 513);
            this.btoCliente.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btoCliente.Name = "btoCliente";
            this.btoCliente.Size = new System.Drawing.Size(265, 59);
            this.btoCliente.TabIndex = 1;
            this.btoCliente.Text = "Menu";
            this.btoCliente.UseVisualStyleBackColor = false;
            this.btoCliente.Click += new System.EventHandler(this.btoCliente_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.BackgroundImage = global::Restaurante.Properties.Resources.WhatsApp_Image_2026_09_02_at_9_09_07_PM;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(893, 598);
            this.Controls.Add(this.btoCliente);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btoCliente;
    }
}

