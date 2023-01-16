using ReporteVentasAseguradoraCredito.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReporteVentasAseguradoraCredito
{
    public partial class WFPrincipal : Form
    {
        public WFPrincipal()
        {
            InitializeComponent();
        }

        private void hamburger_Click(object sender, EventArgs e)
        {
            if(panelMenu.Width == 180)
            {
                panelMenu.Width = 70;
                logo.Visible= false;
                hamburger.Location = new Point(3, 25);
                hamburger.Visible = true;
            }
            else
            {
                panelMenu.Width= 180;
                logo.Visible = true;
                hamburger.Visible = false;
            }
        }

        private void logo_Click(object sender, EventArgs e)
        {
            if (panelMenu.Width == 180)
            {
                panelMenu.Width = 65;
                logo.Visible = false;
                hamburger.Location = new Point(3, 25);
                hamburger.Visible = true;
            }
            else
            {
                panelMenu.Width = 180;
                logo.Visible = true;
                hamburger.Visible = false;
            }
        }

        private void WFPrincipal_Load(object sender, EventArgs e)
        {
            mostrarLogo();
            pantallaCompleta();
        }

        private void mostrarLogo()
        {
            AbrirFormEnPanel(new formLogo());
        }


        private void mostrarLogoAlCerrar(object sender, FormClosedEventArgs e)
        {
            mostrarLogo();
        }

        private void AbrirFormEnPanel(object formHijo)
        {
            if (this.panelContenedorForm.Controls.Count > 0)
                this.panelContenedorForm.Controls.RemoveAt(0);
            Form fh = formHijo as Form;
            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.Fill;
            this.panelContenedorForm.Controls.Add(fh);
            this.panelContenedorForm.Tag = fh;
            fh.Show();
        }

        private void btnSeguros_Click(object sender, EventArgs e)
        {
            WFSeguros seguro = new WFSeguros();
            seguro.FormClosed += new FormClosedEventHandler(mostrarLogoAlCerrar);
            AbrirFormEnPanel(seguro);
        }

        private void btnCreditos_Click(object sender, EventArgs e)
        {
            WFCreditos credito = new WFCreditos();
            credito.FormClosed += new FormClosedEventHandler(mostrarLogoAlCerrar);
            AbrirFormEnPanel((credito));
        }

        int lx, ly;
        int sw, sh;

        public void pantallaCompleta()
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }
    }
}
