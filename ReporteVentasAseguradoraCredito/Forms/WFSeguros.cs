using Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ReporteVentasAseguradoraCredito.Forms
{
    public partial class WFSeguros : Form
    {
        DataTable dt = new DataTable();
        DataTable temporal = new DataTable();

        public WFSeguros()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            dtpStartDate.Select();
            SendKeys.Send("%{DOWN}");
        }

        private void WFSeguros_Load(object sender, EventArgs e)
        {
            lblFechaIni.Text = dtpStartDate.Text;
            lblFechaFinal.Text = dtpEndDate.Text;
        }

        private void lblFechaFinal_Click(object sender, EventArgs e)
        {
            dtpEndDate.Select();
            SendKeys.Send("%{DOWN}");
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            lblFechaFinal.Text = dtpEndDate.Text;
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            lblFechaIni.Text = dtpStartDate.Text;
        }

        private async Task GetVentasAseguradora()
        {
            dt = new DataTable();
            temporal = new DataTable();

            var fechaI = dtpStartDate.Value.Date.ToString("yyyyMMdd");
            var fechaF = dtpEndDate.Value.Date.ToString("yyyyMMdd");
            var nitAseguradora = textBox1.Text.Trim();

            Seguros seguros = new Seguros();
            DataSet data = await seguros.getSeguros("USP_CAC_GET_VENTAS_ASEGURADORAS", fechaI,fechaF,nitAseguradora);
            dt = data.Tables[0];
            temporal = data.Tables[0];

            if (data.Tables[0].Rows.Count > 0)
            {
                dgvDetalle.DataSource = data.Tables[0];

                int c = dgvDetalle.Rows.Count;
                for (int i = 0; i < c; i++)
                {
                    var nc = dgvDetalle.Rows[i].Cells[4].Value.ToString();
                    var na = dgvDetalle.Rows[i].Cells[4].Value.ToString();
                    var fc = dgvDetalle.Rows[i].Cells[33].Value.ToString();

                    if (nc == "4" || na == "5" || fc == "0")
                    {
                        dgvDetalle.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        dgvDetalle.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(236, 72, 36);
                    }

                }

                //total aseguradora
                var totalAseguradora = data.Tables[1];
                lblTotalAsegu.Text = totalAseguradora.Rows[0]["Total"].ToString();

                //Consolidado Aseguradora
                var consolidadoAseguradora = data.Tables[2];
                dgvConsolidadoAseguradora.DataSource = consolidadoAseguradora;
                var sumaConsolidadoAseguradora = consolidadoAseguradora.Compute("Sum(Total)", "Total is not null");
                consolidadoAseguradora.Rows.Add(new object[] { 0, "TOTAL", sumaConsolidadoAseguradora });

                //total cliente
                var totalCliente = data.Tables[3];
                lblTotalClie.Text = totalCliente.Rows[0]["Total"].ToString();

                //consolidado cliente
                var consolidadoCliente = data.Tables[4];
                dgvConsolidadoCliente.DataSource = consolidadoCliente;
                var sumaConsolidadoCliente = consolidadoCliente.Compute("Sum(Total)", "Total is not null");
                consolidadoCliente.Rows.Add(new object[] { 0, "TOTAL", sumaConsolidadoCliente });
            }
            else
                MessageBox.Show("No se obtuvieron datos", "SIN DATOS");

            
        }

        private async Task ExportarReporteSeguros(DataGridView dataGridView)
        {
            await Task.Run(() => {
                Microsoft.Office.Interop.Excel.Application exportExcel = new Microsoft.Office.Interop.Excel.Application();

                exportExcel.Application.Workbooks.Add(true);

                int indexColumn = 0;
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    indexColumn++;
                    exportExcel.Cells[1, indexColumn] = column.Name;
                }

                int indexFila = 0;
                foreach (DataGridViewRow fila in dataGridView.Rows)
                {
                    indexFila++;
                    indexColumn = 0;

                    foreach (DataGridViewColumn column in dataGridView.Columns)
                    {

                        indexColumn++;
                        exportExcel.Cells[indexFila + 1, indexColumn] = fila.Cells[column.Name].Value;
                    }
                }
                 exportExcel.Visible = true;
            });
        }
        
        private void paintRows()
        {
            int c = dgvDetalle.Rows.Count;
            for (int i = 0; i < c; i++)
            {
                var nc = dgvDetalle.Rows[i].Cells[5].Value.ToString();
                var na = dgvDetalle.Rows[i].Cells[5].Value.ToString();
                var fc = dgvDetalle.Rows[i].Cells[33].Value.ToString();

                if (nc == "4" || na == "5" || fc == "0")
                {
                    dgvDetalle.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    dgvDetalle.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(236, 72, 36);
                }

            }
        }

        ErrorProvider error = new ErrorProvider();

        #region Validate textbox
        private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 44 || e.KeyChar >= 46 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 64 || e.KeyChar == 66)  || (e.KeyChar >= 68 && e.KeyChar <= 69)
                || (e.KeyChar >= 72 && e.KeyChar <= 77) || (e.KeyChar >= 79 && e.KeyChar <= 81 || e.KeyChar >= 83 && e.KeyChar<=96 || e.KeyChar == 98) || (e.KeyChar >= 100 && e.KeyChar <= 101)
                || (e.KeyChar >= 104 && e.KeyChar <= 109) || (e.KeyChar >= 111 && e.KeyChar <= 113) || (e.KeyChar >= 115 && e.KeyChar <= 237))
            {
                error.SetError(txtSerie, "Ingrese serie valida");
                e.Handled = true;
            }
            else
            {
                error.Clear();
                
            }
        }

        private void txtRefencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                error.SetError(txtRefencia, "Numero Invalido");
                e.Handled = true;
            }
            else
            {
                error.Clear();
            }
        }

        private void txtNoFel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                error.SetError(txtNoFel, "Numero Invalido");
                e.Handled = true;
            }
            else
            {
                error.Clear();
            }
        }

        private void txtNombreCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                error.SetError(txtNombreCliente, "Solo letras");
                e.Handled = true;
            }
            else
            {
                error.Clear();
            }
        }

        private void txtConvenio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                error.SetError(txtConvenio, "Numero Invalido");
                e.Handled = true;
            }
            else
            {
                error.Clear();
            }
        }

        private void txtSerieOrigen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                error.SetError(txtSerieOrigen, "Solo letras");
                e.Handled = true;
            }
            else
            {
                error.Clear();
            }
        }

        private void txtNumeroOrigen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                error.SetError(txtNumeroOrigen, "Numero Invalido");
                e.Handled = true;
            }
            else
            {
                error.Clear();
            }
        }

        private void txtMontoVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 45 || e.KeyChar == 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                error.SetError(txtMontoVenta, "Invalido");
                e.Handled = true;
            }
            else
            {
                error.Clear();
            }
        }

        private async void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 44) || (e.KeyChar >= 46 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 106) || (e.KeyChar >= 108 && e.KeyChar <= 255))
            {
                error.SetError(textBox1, "Ingrese un nit valido");
                e.Handled = true;
            }
            else if (e.KeyChar == 13)
            {
                loadDetalle.Visible = true;
                await GetVentasAseguradora();
                loadDetalle.Visible = false;
            }
            else
                error.Clear();
        }



        #endregion


        #region Filtrado
        private void txtSerie_TextChanged(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                dt.DefaultView.RowFilter = string.Format(@"[Serie Cliente] LIKE '%{0}%' AND [Numero Cliente] LIKE '%{1}%' 
                                                                                AND [Numero Fel Cliente] LIKE '%{2}%'
                                                                                AND [Nombre Cliente] LIKE '%{3}%'
                                                                                AND [Convenio] LIKE '%{4}%'
                                                                                AND [Serie Org Cliente] LIKE '%{5}%'
                                                                                AND [Numero Org Cliente] LIKE '%{6}%'
                                                                                AND [Total Cliente] LIKE '%{7}%'", txtSerie.Text.ToUpper().Trim()
                                                                                , txtRefencia.Text.Trim()
                                                                                , txtNoFel.Text.Trim()
                                                                                , txtNombreCliente.Text.ToUpper().Trim()
                                                                                , txtConvenio.Text.Trim()
                                                                                , txtSerieOrigen.Text.ToUpper().Trim()
                                                                                , txtNumeroOrigen.Text.Trim()
                                                                                , txtMontoVenta.Text.Trim());
                dgvDetalle.DataSource = dt;
                paintRows();
            }
            else
            {
                dgvDetalle.DataSource = dt.DefaultView;
            }
        }

        private void txtRefencia_TextChanged(object sender, EventArgs e)
        {
            if (temporal.Rows.Count > 0)
            {
                dt.DefaultView.RowFilter = string.Format(@"[Serie Cliente] LIKE '%{0}%' AND [Numero Cliente] LIKE '%{1}%' 
                                                                                AND [Numero Fel Cliente] LIKE '%{2}%'
                                                                                AND [Nombre Cliente] LIKE '%{3}%'
                                                                                AND [Convenio] LIKE '%{4}%'
                                                                                AND [Serie Org Cliente] LIKE '%{5}%'
                                                                                AND [Numero Org Cliente] LIKE '%{6}%'
                                                                                AND [Total Cliente] LIKE '%{7}%'", txtSerie.Text.ToUpper().Trim()
                                                                                , txtRefencia.Text.Trim()
                                                                                , txtNoFel.Text.Trim()
                                                                                , txtNombreCliente.Text.ToUpper().Trim()
                                                                                , txtConvenio.Text.Trim()
                                                                                , txtSerieOrigen.Text.ToUpper().Trim()
                                                                                , txtNumeroOrigen.Text.Trim()
                                                                                , txtMontoVenta.Text.Trim());
                dgvDetalle.DataSource = dt;
                paintRows();
            }
            else
            {
                dgvDetalle.DataSource = temporal;
            }
        }

        private void txtNoFel_TextChanged(object sender, EventArgs e)
        {
            if (temporal.Rows.Count > 0)
            {
                dt.DefaultView.RowFilter = string.Format(@"[Serie Cliente] LIKE '%{0}%' AND [Numero Cliente] LIKE '%{1}%' 
                                                                                AND [Numero Fel Cliente] LIKE '%{2}%'
                                                                                AND [Nombre Cliente] LIKE '%{3}%'
                                                                                AND [Convenio] LIKE '%{4}%'
                                                                                AND [Serie Org Cliente] LIKE '%{5}%'
                                                                                AND [Numero Org Cliente] LIKE '%{6}%'
                                                                                AND [Total Cliente] LIKE '%{7}%'", txtSerie.Text.ToUpper().Trim()
                                                                                , txtRefencia.Text.Trim()
                                                                                , txtNoFel.Text.Trim()
                                                                                , txtNombreCliente.Text.ToUpper().Trim()
                                                                                , txtConvenio.Text.Trim()
                                                                                , txtSerieOrigen.Text.ToUpper().Trim()
                                                                                , txtNumeroOrigen.Text.Trim()
                                                                                , txtMontoVenta.Text.Trim());
                dgvDetalle.DataSource = dt;
                paintRows();
            }
            else
            {
                dgvDetalle.DataSource = temporal;
            }
        }

        private void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {
            if (temporal.Rows.Count > 0)
            {
                dt.DefaultView.RowFilter = string.Format(@"[Serie Cliente] LIKE '%{0}%' AND [Numero Cliente] LIKE '%{1}%' 
                                                                                AND [Numero Fel Cliente] LIKE '%{2}%'
                                                                                AND [Nombre Cliente] LIKE '%{3}%'
                                                                                AND [Convenio] LIKE '%{4}%'
                                                                                AND [Serie Org Cliente] LIKE '%{5}%'
                                                                                AND [Numero Org Cliente] LIKE '%{6}%'
                                                                                AND [Total Cliente] LIKE '%{7}%'", txtSerie.Text.ToUpper().Trim()
                                                                                , txtRefencia.Text.Trim()
                                                                                , txtNoFel.Text.Trim()
                                                                                , txtNombreCliente.Text.ToUpper().Trim()
                                                                                , txtConvenio.Text.Trim()
                                                                                , txtSerieOrigen.Text.ToUpper().Trim()
                                                                                , txtNumeroOrigen.Text.Trim()
                                                                                , txtMontoVenta.Text.Trim());
                dgvDetalle.DataSource = dt;
                paintRows();
            }
            else
            {
                dgvDetalle.DataSource = temporal;
            }
        }

        private void txtConvenio_TextChanged(object sender, EventArgs e)
        {
            if (temporal.Rows.Count > 0)
            {
                dt.DefaultView.RowFilter = string.Format(@"[Serie Cliente] LIKE '%{0}%' AND [Numero Cliente] LIKE '%{1}%' 
                                                                                AND [Numero Fel Cliente] LIKE '%{2}%'
                                                                                AND [Nombre Cliente] LIKE '%{3}%'
                                                                                AND [Convenio] LIKE '%{4}%'
                                                                                AND [Serie Org Cliente] LIKE '%{5}%'
                                                                                AND [Numero Org Cliente] LIKE '%{6}%'
                                                                                AND [Total Cliente] LIKE '%{7}%'", txtSerie.Text.ToUpper().Trim()
                                                                                , txtRefencia.Text.Trim()
                                                                                , txtNoFel.Text.Trim()
                                                                                , txtNombreCliente.Text.ToUpper().Trim()
                                                                                , txtConvenio.Text.Trim()
                                                                                , txtSerieOrigen.Text.ToUpper().Trim()
                                                                                , txtNumeroOrigen.Text.Trim()
                                                                                , txtMontoVenta.Text.Trim());
                dgvDetalle.DataSource = dt;
                paintRows();
            }
            else
            {
                dgvDetalle.DataSource = temporal;
            }
        }

        private void txtSerieOrigen_TextChanged(object sender, EventArgs e)
        {
            if (temporal.Rows.Count > 0)
            {
                dt.DefaultView.RowFilter = string.Format(@"[Serie Cliente] LIKE '%{0}%' AND [Numero Cliente] LIKE '%{1}%' 
                                                                                AND [Numero Fel Cliente] LIKE '%{2}%'
                                                                                AND [Nombre Cliente] LIKE '%{3}%'
                                                                                AND [Convenio] LIKE '%{4}%'
                                                                                AND [Serie Org Cliente] LIKE '%{5}%'
                                                                                AND [Numero Org Cliente] LIKE '%{6}%'
                                                                                AND [Total Cliente] LIKE '%{7}%'", txtSerie.Text.ToUpper().Trim()
                                                                                , txtRefencia.Text.Trim()
                                                                                , txtNoFel.Text.Trim()
                                                                                , txtNombreCliente.Text.ToUpper().Trim()
                                                                                , txtConvenio.Text.Trim()
                                                                                , txtSerieOrigen.Text.ToUpper().Trim()
                                                                                , txtNumeroOrigen.Text.Trim()
                                                                                , txtMontoVenta.Text.Trim());
                dgvDetalle.DataSource = dt;
                paintRows();
            }
            else
            {
                dgvDetalle.DataSource = temporal;
            }
        }

        private void txtNumeroOrigen_TextChanged(object sender, EventArgs e)
        {
            if (temporal.Rows.Count > 0)
            {
                dt.DefaultView.RowFilter = string.Format(@"[Serie Cliente] LIKE '%{0}%' AND [Numero Cliente] LIKE '%{1}%' 
                                                                                AND [Numero Fel Cliente] LIKE '%{2}%'
                                                                                AND [Nombre Cliente] LIKE '%{3}%'
                                                                                AND [Convenio] LIKE '%{4}%'
                                                                                AND [Serie Org Cliente] LIKE '%{5}%'
                                                                                AND [Numero Org Cliente] LIKE '%{6}%'
                                                                                AND [Total Cliente] LIKE '%{7}%'", txtSerie.Text.ToUpper().Trim()
                                                                                , txtRefencia.Text.Trim()
                                                                                , txtNoFel.Text.Trim()
                                                                                , txtNombreCliente.Text.ToUpper().Trim()
                                                                                , txtConvenio.Text.Trim()
                                                                                , txtSerieOrigen.Text.ToUpper().Trim()
                                                                                , txtNumeroOrigen.Text.Trim()
                                                                                , txtMontoVenta.Text.Trim());
                dgvDetalle.DataSource = dt;
                paintRows();
            }
            else
            {
                dgvDetalle.DataSource = temporal;
            }
        }

        private void txtMontoVenta_TextChanged(object sender, EventArgs e)
        {
            if (temporal.Rows.Count > 0)
            {
                dt.DefaultView.RowFilter = string.Format(@"[Serie Cliente] LIKE '%{0}%' AND [Numero Cliente] LIKE '%{1}%' 
                                                                                AND [Numero Fel Cliente] LIKE '%{2}%'
                                                                                AND [Nombre Cliente] LIKE '%{3}%'
                                                                                AND [Convenio] LIKE '%{4}%'
                                                                                AND [Serie Org Cliente] LIKE '%{5}%'
                                                                                AND [Numero Org Cliente] LIKE '%{6}%'
                                                                                AND [Total Cliente] LIKE '%{7}%'", txtSerie.Text.ToUpper().Trim()
                                                                                , txtRefencia.Text.Trim()
                                                                                , txtNoFel.Text.Trim()
                                                                                , txtNombreCliente.Text.ToUpper().Trim()
                                                                                , txtConvenio.Text.Trim()
                                                                                , txtSerieOrigen.Text.ToUpper().Trim()
                                                                                , txtNumeroOrigen.Text.Trim()
                                                                                , txtMontoVenta.Text.Trim());
                dgvDetalle.DataSource = dt;
                paintRows();
            }
            else
            {
                dgvDetalle.DataSource = temporal;
            }
        }
        #endregion

        private async void btnGenerar_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                error.Clear();
                loadDetalle.Visible = true;
                await GetVentasAseguradora();
                loadDetalle.Visible = false;
            }
            else
                error.SetError(textBox1, "Ingrese Nit");
            
        }

        private async void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.Rows.Count > 0)
            {
                error.Clear();
                loadExport.Visible = true;
                await ExportarReporteSeguros(dgvDetalle);
                loadExport.Visible = false;
            }
            else
                error.SetError(btnGenerar, "Genera un Reporte");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
