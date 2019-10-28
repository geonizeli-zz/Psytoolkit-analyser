using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public class LerExperimento
        {
            public LerExperimento()
            {

            }
            public string Experimento1(string file)
            {
                return file;
            }
            public string Experimento2(string file)
            {
                return file;
            }
            public string Experimento3(string file)
            {
                return file;
            }
        }

        private void LerUsuario(DataTable dt, string[] list)
        {
            label1.Text = "Colunas: ";

            LerExperimento read = new LerExperimento();

            label1.Text = read.Experimento1(dt.Rows[0].Field<string>(5));

            /* Adicionar depois desse comentario:
             * Função para for que vai executar as funções dentro de LerExperimento,
             * entrando com as colunas: iniciais:1	sexo:1	Experimento1:1	Experimento2:1	Experimento3:1
            */

        }

        private void CarregaDadosExcel()
        {
            try
            {
                DataTable dt = GetTabelaExcel(txtArquivoExcel.Text);
                dgvDados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvDados.DataSource = dt;
                lblRegistros.Text = "Participantes: " + (dgvDados.Rows.Count - 1).ToString();
                string[] listaNomeColunas = dt.Columns.OfType<DataColumn>().Select(x => x.ColumnName).ToArray();
                LerUsuario(dt, listaNomeColunas);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.Message);
            }
        }

        private DataTable GetTabelaExcel(string arquivoExcel)
        {
            DataTable dt = new DataTable();
            try
            {
                string Ext = Path.GetExtension(arquivoExcel);
                string connectionString = "";
                if (Ext == ".xls")
                {
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source ="
                        + arquivoExcel + "; Extended Properties = 'Excel 8.0;HDR=YES'";
                }
                else if (Ext == ".xlsx")
                {
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source ="
                        + arquivoExcel + "; Extended Properties = 'Excel 8.0;HDR=YES'";
                }
                OleDbConnection conn = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                cmd.Connection = conn;
                conn.Open();
                DataTable dtSchema;
                dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string nomePlanilha = dtSchema.Rows[0]["TABLE_NAME"].ToString();
                conn.Close();
                conn.Open();
                cmd.CommandText = "SELECT * From [" + nomePlanilha + "]";
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult drResult = ofd1.ShowDialog();


            if (drResult == System.Windows.Forms.DialogResult.OK)
                txtArquivoExcel.Text = ofd1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtArquivoExcel.Text) && File.Exists(txtArquivoExcel.Text))
            {
                CarregaDadosExcel();
            }
            else
            {
                lblRegistros.Text = "Nenhum arquivo selecionado!";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
