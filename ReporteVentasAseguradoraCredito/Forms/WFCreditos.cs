using Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReporteVentasAseguradoraCredito.Forms
{
    public partial class WFCreditos : Form
    {
        DataTable dt = new DataTable();
        DataTable temporal = new DataTable();

        public WFCreditos()
        {
            InitializeComponent();
        }

        private void WFCreditos_Load(object sender, EventArgs e)
        {
            lblFechaIni.Text = dtpStartDate.Text;
            lblFechaFinal.Text = dtpEndDate.Text;
        }

        private void lblFechaIni_Click(object sender, EventArgs e)
        {
            dtpStartDate.Select();
            SendKeys.Send("%{DOWN}");
        }

        private void lblFechaFinal_Click(object sender, EventArgs e)
        {
            dtpEndDate.Select();
            SendKeys.Send("%{DOWN}");
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            lblFechaIni.Text = dtpStartDate.Text;
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            lblFechaFinal.Text = dtpEndDate.Text;
        }

        private async Task getVentasCredito()
        {
            dt = new DataTable();
            temporal = new DataTable();

            var fechaInicio = dtpStartDate.Value.Date.ToString("yyyyMMdd");
            var fechaFinal = dtpEndDate.Value.Date.ToString("yyyyMMdd");
            var nit = txtNitCredito.Text.Trim();

            Creditos creditos = new Creditos();
            DataSet data = await creditos.getVentasCredito("USP_CAC_GET_VENTAS_CREDITOS", fechaInicio, fechaFinal, nit);

            dt = data.Tables[0];
            temporal = data.Tables[0];

            if (data.Tables[0].Rows.Count > 0)
            {
                dgvDetalleCreditos.DataSource = data.Tables[0];

                int c = dgvDetalleCreditos.Rows.Count;
                for (int i = 0; i < c; i++)
                {
                    var nc = dgvDetalleCreditos.Rows[i].Cells[11].Value.ToString();
                    var na = dgvDetalleCreditos.Rows[i].Cells[11].Value.ToString();
                    var fcAn = dgvDetalleCreditos.Rows[i].Cells[15].Value.ToString();

                    if (nc == "4" || na == "5" || fcAn == "0")
                    {
                        dgvDetalleCreditos.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        dgvDetalleCreditos.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(236, 72, 36);
                    }
                }

                //total creditos
                var totalVentasCredito = data.Tables[1];
                lblTotalCreditos.Text = totalVentasCredito.Rows[0]["Total"].ToString();

                //consolidado creditos
                var consolidadoCreditos = data.Tables[2];
                dgvConsolidado.DataSource = consolidadoCreditos;
                var sumaConsolidadoCreditos = consolidadoCreditos.Compute("Sum(Total)", "Total is not null");
                consolidadoCreditos.Rows.Add(new object[] { 0, "TOTAL", sumaConsolidadoCreditos });
            }
            else
                MessageBox.Show("No se obtuvieron datos", "SIN DATOS");
            
        }

        private async Task ExportarReporteCreditos(DataGridView dataGridView)
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
            int c = dgvDetalleCreditos.Rows.Count;
            for (int i = 0; i < c; i++)
            {
                var nc = dgvDetalleCreditos.Rows[i].Cells[11].Value.ToString();
                var na = dgvDetalleCreditos.Rows[i].Cells[11].Value.ToString();
                var fcAn = dgvDetalleCreditos.Rows[i].Cells[15].Value.ToString();

                if (nc == "4" || na == "5" || fcAn == "0")
                {
                    dgvDetalleCreditos.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                    dgvDetalleCreditos.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(236, 72, 36);
                }
            }
        }

        ErrorProvider error = new ErrorProvider();

        #region TextBox Validation
        private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 44 || e.KeyChar >= 46 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 64 || e.KeyChar == 66) || (e.KeyChar >= 68 && e.KeyChar <= 69)
                || (e.KeyChar >= 72 && e.KeyChar <= 77) || (e.KeyChar >= 79 && e.KeyChar <= 81 || e.KeyChar >= 83 && e.KeyChar <= 96 || e.KeyChar == 98) || (e.KeyChar >= 100 && e.KeyChar <= 101)
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

        private async void txtNitCredito_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 44) || (e.KeyChar >= 46 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 106) || (e.KeyChar >= 108 && e.KeyChar <= 255))
            {
                error.SetError(txtNitCredito, "Ingrese un nit valido");
                e.Handled = true;
            }
            else if (e.KeyChar == 13)
            {
                loadDetalle.Visible = true;
                await getVentasCredito();
                loadDetalle.Visible = false;
            }
            else
                error.Clear();
        }
        #endregion

        #region Filter
        private void txtSerie_TextChanged(object sender, EventArgs e)
        {
            if (temporal.Rows.Count > 0)
            {
                dt.DefaultView.RowFilter = string.Format(@"[Serie] LIKE '%{0}%' AND [Numero] LIKE '%{1}%' 
                                                                                AND [Numero Fel] LIKE '%{2}%'
                                                                                AND [Nombre] LIKE '%{3}%'
                                                                                AND [Convenio] LIKE '%{4}%'
                                                                                AND [Serie Origen] LIKE '%{5}%'
                                                                                AND [Numero Origen] LIKE '%{6}%'
                                                                                AND [Total] LIKE '%{7}%'", txtSerie.Text.ToUpper().Trim()
                                                                                , txtRefencia.Text.Trim()
                                                                                , txtNoFel.Text.Trim()
                                                                                , txtNombreCliente.Text.ToUpper().Trim()
                                                                                , txtConvenio.Text.Trim()
                                                                                , txtSerieOrigen.Text.ToUpper().Trim()
                                                                                , txtNumeroOrigen.Text.Trim()
                                                                                , txtMontoVenta.Text.Trim());
                dgvDetalleCreditos.DataSource = dt;
                paintRows();
            }
            else
            {
                dgvDetalleCreditos.DataSource = temporal;
            }
        }

        private void txtRefencia_TextChanged(object sender, EventArgs e)
        {
            if (temporal.Rows.Count > 0)
            {
                dt.DefaultView.RowFilter = string.Format(@"[Serie] LIKE '%{0}%' AND [Numero] LIKE '%{1}%' 
                                                                                AND [Numero Fel] LIKE '%{2}%'
                                                                                AND [Nombre] LIKE '%{3}%'
                                                                                AND [Convenio] LIKE '%{4}%'
                                                                                AND [Serie Origen] LIKE '%{5}%'
                                                                                AND [Numero Origen] LIKE '%{6}%'
                                                                                AND [Total] LIKE '%{7}%'", txtSerie.Text.ToUpper().Trim()
                                                                                , txtRefencia.Text.Trim()
                                                                                , txtNoFel.Text.Trim()
                                                                                , txtNombreCliente.Text.ToUpper().Trim()
                                                                                , txtConvenio.Text.Trim()
                                                                                , txtSerieOrigen.Text.ToUpper().Trim()
                                                                                , txtNumeroOrigen.Text.Trim()
                                                                                , txtMontoVenta.Text.Trim());
                dgvDetalleCreditos.DataSource = dt;
                paintRows();
            }
            else
            {
                dgvDetalleCreditos.DataSource = temporal;
            }
        }

        private void txtNoFel_TextChanged(object sender, EventArgs e)
        {
            if (temporal.Rows.Count > 0)
            {
                dt.DefaultView.RowFilter = string.Format(@"[Serie] LIKE '%{0}%' AND [Numero] LIKE '%{1}%' 
                                                                                AND [Numero Fel] LIKE '%{2}%'
                                                                                AND [Nombre] LIKE '%{3}%'
                                                                                AND [Convenio] LIKE '%{4}%'
                                                                                AND [Serie Origen] LIKE '%{5}%'
                                                                                AND [Numero Origen] LIKE '%{6}%'
                                                                                AND [Total] LIKE '%{7}%'", txtSerie.Text.ToUpper().Trim()
                                                                                , txtRefencia.Text.Trim()
                                                                                , txtNoFel.Text.Trim()
                                                                                , txtNombreCliente.Text.ToUpper().Trim()
                                                                                , txtConvenio.Text.Trim()
                                                                                , txtSerieOrigen.Text.ToUpper().Trim()
                                                                                , txtNumeroOrigen.Text.Trim()
                                                                                , txtMontoVenta.Text.Trim());
                dgvDetalleCreditos.DataSource = dt;
                paintRows();
            }
            else
            {
                dgvDetalleCreditos.DataSource = temporal;
            }
        }

        private void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {
            if (temporal.Rows.Count > 0)
            {
                dt.DefaultView.RowFilter = string.Format(@"[Serie] LIKE '%{0}%' AND [Numero] LIKE '%{1}%' 
                                                                                AND [Numero Fel] LIKE '%{2}%'
                                                                                AND [Nombre] LIKE '%{3}%'
                                                                                AND [Convenio] LIKE '%{4}%'
                                                                                AND [Serie Origen] LIKE '%{5}%'
                                                                                AND [Numero Origen] LIKE '%{6}%'
                                                                                AND [Total] LIKE '%{7}%'", txtSerie.Text.ToUpper().Trim()
                                                                                , txtRefencia.Text.Trim()
                                                                                , txtNoFel.Text.Trim()
                                                                                , txtNombreCliente.Text.ToUpper().Trim()
                                                                                , txtConvenio.Text.Trim()
                                                                                , txtSerieOrigen.Text.ToUpper().Trim()
                                                                                , txtNumeroOrigen.Text.Trim()
                                                                                , txtMontoVenta.Text.Trim());
                dgvDetalleCreditos.DataSource = dt;
                paintRows();
            }
            else
            {
                dgvDetalleCreditos.DataSource = temporal;
            }
        }

        private void txtConvenio_TextChanged(object sender, EventArgs e)
        {
            if (temporal.Rows.Count > 0)
            {
                dt.DefaultView.RowFilter = string.Format(@"[Serie] LIKE '%{0}%' AND [Numero] LIKE '%{1}%' 
                                                                                AND [Numero Fel] LIKE '%{2}%'
                                                                                AND [Nombre] LIKE '%{3}%'
                                                                                AND [Convenio] LIKE '%{4}%'
                                                                                AND [Serie Origen] LIKE '%{5}%'
                                                                                AND [Numero Origen] LIKE '%{6}%'
                                                                                AND [Total] LIKE '%{7}%'", txtSerie.Text.ToUpper().Trim()
                                                                                , txtRefencia.Text.Trim()
                                                                                , txtNoFel.Text.Trim()
                                                                                , txtNombreCliente.Text.ToUpper().Trim()
                                                                                , txtConvenio.Text.Trim()
                                                                                , txtSerieOrigen.Text.ToUpper().Trim()
                                                                                , txtNumeroOrigen.Text.Trim()
                                                                                , txtMontoVenta.Text.Trim());
                dgvDetalleCreditos.DataSource = dt;
                paintRows();
            }
            else
            {
                dgvDetalleCreditos.DataSource = temporal;
            }
        }

        private void txtSerieOrigen_TextChanged(object sender, EventArgs e)
        {
            if (temporal.Rows.Count > 0)
            {
                dt.DefaultView.RowFilter = string.Format(@"[Serie] LIKE '%{0}%' AND [Numero] LIKE '%{1}%' 
                                                                                AND [Numero Fel] LIKE '%{2}%'
                                                                                AND [Nombre] LIKE '%{3}%'
                                                                                AND [Convenio] LIKE '%{4}%'
                                                                                AND [Serie Origen] LIKE '%{5}%'
                                                                                AND [Numero Origen] LIKE '%{6}%'
                                                                                AND [Total] LIKE '%{7}%'", txtSerie.Text.ToUpper().Trim()
                                                                                , txtRefencia.Text.Trim()
                                                                                , txtNoFel.Text.Trim()
                                                                                , txtNombreCliente.Text.ToUpper().Trim()
                                                                                , txtConvenio.Text.Trim()
                                                                                , txtSerieOrigen.Text.ToUpper().Trim()
                                                                                , txtNumeroOrigen.Text.Trim()
                                                                                , txtMontoVenta.Text.Trim());
                dgvDetalleCreditos.DataSource = dt;
                paintRows();
            }
            else
            {
                dgvDetalleCreditos.DataSource = temporal;
            }
        }

        private void txtNumeroOrigen_TextChanged(object sender, EventArgs e)
        {
            if (temporal.Rows.Count > 0)
            {
                dt.DefaultView.RowFilter = string.Format(@"[Serie] LIKE '%{0}%' AND [Numero] LIKE '%{1}%' 
                                                                                AND [Numero Fel] LIKE '%{2}%'
                                                                                AND [Nombre] LIKE '%{3}%'
                                                                                AND [Convenio] LIKE '%{4}%'
                                                                                AND [Serie Origen] LIKE '%{5}%'
                                                                                AND [Numero Origen] LIKE '%{6}%'
                                                                                AND [Total] LIKE '%{7}%'", txtSerie.Text.ToUpper().Trim()
                                                                                , txtRefencia.Text.Trim()
                                                                                , txtNoFel.Text.Trim()
                                                                                , txtNombreCliente.Text.ToUpper().Trim()
                                                                                , txtConvenio.Text.Trim()
                                                                                , txtSerieOrigen.Text.ToUpper().Trim()
                                                                                , txtNumeroOrigen.Text.Trim()
                                                                                , txtMontoVenta.Text.Trim());
                dgvDetalleCreditos.DataSource = dt;
                paintRows();
            }
            else
            {
                dgvDetalleCreditos.DataSource = temporal;
            }
        }

        private void txtMontoVenta_TextChanged(object sender, EventArgs e)
        {
            if (temporal.Rows.Count > 0)
            {
                dt.DefaultView.RowFilter = string.Format(@"[Serie] LIKE '%{0}%' AND [Numero] LIKE '%{1}%' 
                                                                                AND [Numero Fel] LIKE '%{2}%'
                                                                                AND [Nombre] LIKE '%{3}%'
                                                                                AND [Convenio] LIKE '%{4}%'
                                                                                AND [Serie Origen] LIKE '%{5}%'
                                                                                AND [Numero Origen] LIKE '%{6}%'
                                                                                AND [Total] LIKE '%{7}%'", txtSerie.Text.ToUpper().Trim()
                                                                                , txtRefencia.Text.Trim()
                                                                                , txtNoFel.Text.Trim()
                                                                                , txtNombreCliente.Text.ToUpper().Trim()
                                                                                , txtConvenio.Text.Trim()
                                                                                , txtSerieOrigen.Text.ToUpper().Trim()
                                                                                , txtNumeroOrigen.Text.Trim()
                                                                                , txtMontoVenta.Text.Trim());
                dgvDetalleCreditos.DataSource = dt;
                paintRows();
            }
            else
            {
                dgvDetalleCreditos.DataSource = temporal;
            }
        }
        #endregion

        private async void btnGenerar_Click(object sender, EventArgs e)
        {
            if (txtNitCredito.Text != "")
            {
                loadDetalle.Visible = true;
                await getVentasCredito();
                loadDetalle.Visible = false;
            }
            else
                error.SetError(txtNitCredito, "ingrese Nit");
            
        }

        private async void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleCreditos.Rows.Count > 0)
            {
                error.Clear();
                loadExport.Visible= true;
                await ExportarReporteCreditos(dgvDetalleCreditos);
                loadExport.Visible = false;
            }
            else
                error.SetError(btnExportar, "Genere un Reporte");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
