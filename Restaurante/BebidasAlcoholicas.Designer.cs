namespace Restaurante
{
    partial class BebidasAlcoholicas
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toña = new System.Windows.Forms.NumericUpDown();
            this.victoria = new System.Windows.Forms.NumericUpDown();
            this.corona = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.labtoña = new System.Windows.Forms.Label();
            this.labvictoria = new System.Windows.Forms.Label();
            this.labcorona = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.toña)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.victoria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.corona)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Toña";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Victoria";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Corona";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(288, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "C$ 50";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(288, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "C$ 40";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(288, 221);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "C$ 80";
            // 
            // toña
            // 
            this.toña.Location = new System.Drawing.Point(495, 21);
            this.toña.Name = "toña";
            this.toña.ReadOnly = true;
            this.toña.Size = new System.Drawing.Size(47, 22);
            this.toña.TabIndex = 6;
            // 
            // victoria
            // 
            this.victoria.Location = new System.Drawing.Point(495, 121);
            this.victoria.Name = "victoria";
            this.victoria.ReadOnly = true;
            this.victoria.Size = new System.Drawing.Size(47, 22);
            this.victoria.TabIndex = 7;
            // 
            // corona
            // 
            this.corona.Location = new System.Drawing.Point(495, 215);
            this.corona.Name = "corona";
            this.corona.ReadOnly = true;
            this.corona.Size = new System.Drawing.Size(47, 22);
            this.corona.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(684, 221);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 16);
            this.label9.TabIndex = 11;
            // 
            // labtoña
            // 
            this.labtoña.Location = new System.Drawing.Point(684, 21);
            this.labtoña.Name = "labtoña";
            this.labtoña.Size = new System.Drawing.Size(50, 16);
            this.labtoña.TabIndex = 12;
            this.labtoña.Text = "C$ 0.00";
            // 
            // labvictoria
            // 
            this.labvictoria.Location = new System.Drawing.Point(684, 111);
            this.labvictoria.Name = "labvictoria";
            this.labvictoria.Size = new System.Drawing.Size(50, 16);
            this.labvictoria.TabIndex = 13;
            this.labvictoria.Text = "C$ 0.00";
            // 
            // labcorona
            // 
            this.labcorona.Location = new System.Drawing.Point(684, 221);
            this.labcorona.Name = "labcorona";
            this.labcorona.Size = new System.Drawing.Size(50, 16);
            this.labcorona.TabIndex = 14;
            this.labcorona.Text = "C$ 0.00";
            // 
            // BebidasAlcoholicas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labcorona);
            this.Controls.Add(this.labvictoria);
            this.Controls.Add(this.labtoña);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.corona);
            this.Controls.Add(this.victoria);
            this.Controls.Add(this.toña);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "BebidasAlcoholicas";
            this.Text = "BebidasAlcoholicas";
            ((System.ComponentModel.ISupportInitialize)(this.toña)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.victoria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.corona)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown toña;
        private System.Windows.Forms.NumericUpDown victoria;
        private System.Windows.Forms.NumericUpDown corona;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labtoña;
        private System.Windows.Forms.Label labvictoria;
        private System.Windows.Forms.Label labcorona;
    }
}