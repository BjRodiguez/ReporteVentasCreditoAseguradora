namespace ReporteVentasAseguradoraCredito
{
    partial class WFPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WFPrincipal));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnCreditos = new System.Windows.Forms.Button();
            this.btnSeguros = new System.Windows.Forms.Button();
            this.hamburger = new System.Windows.Forms.PictureBox();
            this.logo = new System.Windows.Forms.PictureBox();
            this.panelContenedorForm = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hamburger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(108)))), ((int)(((byte)(108)))));
            this.panelMenu.Controls.Add(this.btnCreditos);
            this.panelMenu.Controls.Add(this.btnSeguros);
            this.panelMenu.Controls.Add(this.hamburger);
            this.panelMenu.Controls.Add(this.logo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(240, 912);
            this.panelMenu.TabIndex = 0;
            // 
            // btnCreditos
            // 
            this.btnCreditos.BackColor = System.Drawing.Color.Transparent;
            this.btnCreditos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreditos.FlatAppearance.BorderSize = 0;
            this.btnCreditos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(13)))), ((int)(((byte)(88)))));
            this.btnCreditos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(13)))), ((int)(((byte)(88)))));
            this.btnCreditos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreditos.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreditos.ForeColor = System.Drawing.Color.White;
            this.btnCreditos.Image = ((System.Drawing.Image)(resources.GetObject("btnCreditos.Image")));
            this.btnCreditos.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCreditos.Location = new System.Drawing.Point(4, 254);
            this.btnCreditos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCreditos.Name = "btnCreditos";
            this.btnCreditos.Size = new System.Drawing.Size(240, 71);
            this.btnCreditos.TabIndex = 3;
            this.btnCreditos.Text = "         CREDITOS";
            this.btnCreditos.UseVisualStyleBackColor = false;
            this.btnCreditos.Click += new System.EventHandler(this.btnCreditos_Click);
            // 
            // btnSeguros
            // 
            this.btnSeguros.BackColor = System.Drawing.Color.Transparent;
            this.btnSeguros.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSeguros.FlatAppearance.BorderSize = 0;
            this.btnSeguros.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(13)))), ((int)(((byte)(88)))));
            this.btnSeguros.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(13)))), ((int)(((byte)(88)))));
            this.btnSeguros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeguros.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeguros.ForeColor = System.Drawing.Color.White;
            this.btnSeguros.Image = ((System.Drawing.Image)(resources.GetObject("btnSeguros.Image")));
            this.btnSeguros.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSeguros.Location = new System.Drawing.Point(0, 175);
            this.btnSeguros.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSeguros.Name = "btnSeguros";
            this.btnSeguros.Size = new System.Drawing.Size(240, 71);
            this.btnSeguros.TabIndex = 2;
            this.btnSeguros.Text = "          SEGUROS";
            this.btnSeguros.UseVisualStyleBackColor = false;
            this.btnSeguros.Click += new System.EventHandler(this.btnSeguros_Click);
            // 
            // hamburger
            // 
            this.hamburger.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hamburger.Image = ((System.Drawing.Image)(resources.GetObject("hamburger.Image")));
            this.hamburger.Location = new System.Drawing.Point(80, 46);
            this.hamburger.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.hamburger.Name = "hamburger";
            this.hamburger.Size = new System.Drawing.Size(80, 62);
            this.hamburger.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.hamburger.TabIndex = 1;
            this.hamburger.TabStop = false;
            this.hamburger.Visible = false;
            this.hamburger.Click += new System.EventHandler(this.hamburger_Click);
            // 
            // logo
            // 
            this.logo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logo.Image = ((System.Drawing.Image)(resources.GetObject("logo.Image")));
            this.logo.Location = new System.Drawing.Point(16, 31);
            this.logo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(205, 76);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logo.TabIndex = 0;
            this.logo.TabStop = false;
            this.logo.Click += new System.EventHandler(this.logo_Click);
            // 
            // panelContenedorForm
            // 
            this.panelContenedorForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedorForm.Location = new System.Drawing.Point(240, 0);
            this.panelContenedorForm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelContenedorForm.Name = "panelContenedorForm";
            this.panelContenedorForm.Size = new System.Drawing.Size(1684, 912);
            this.panelContenedorForm.TabIndex = 1;
            // 
            // WFPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 912);
            this.Controls.Add(this.panelContenedorForm);
            this.Controls.Add(this.panelMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1781, 875);
            this.Name = "WFPrincipal";
            this.Text = "REPORTE VENTAS ASEGURADORA Y CREDITOS 1.0.0";
            this.Load += new System.EventHandler(this.WFPrincipal_Load);
            this.panelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hamburger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.PictureBox hamburger;
        private System.Windows.Forms.Button btnSeguros;
        private System.Windows.Forms.Button btnCreditos;
        private System.Windows.Forms.Panel panelContenedorForm;
    }
}

