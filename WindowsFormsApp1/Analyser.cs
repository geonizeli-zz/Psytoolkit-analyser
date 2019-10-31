using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
namespace WindowsFormsApp1
{
    class Analyser
    {
        public Analyser(){}
        public string path = "";
        public System.Data.DataTable ReadDt(System.Data.DataTable dt, string diretory)
        {
            // Capture path of files
            string[] temp = diretory.Split('\\');
            for (int i = 0; i < temp.Length - 1; i++)
            {
                path = path + temp[i] + "\\";
            }

            string[] txt_datas;
            string[] txt_dataf;
            string[] txt_datan;

            // Performs analysis on each 
            System.Data.DataTable final = new System.Data.DataTable();

            DataColumn column;
            // Create name column
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "name";
            final.Columns.Add(column);
            // Create data columns.
            for (int j = 0; j < 160; j++)
            {
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = j.ToString();
                final.Columns.Add(column);

            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                txt_datas = Stroop(dt.Rows[i].Field<string>(3));
                txt_dataf = Fitts(dt.Rows[i].Field<string>(4));
                txt_datan = Nback(dt.Rows[i].Field<string>(5));
                final = AddToFinal(final, dt.Rows[i].Field<string>(1), txt_datas, txt_dataf, txt_datan);
            }

            return final;
        }
        private string[] Stroop(string file)
        {
            StreamReader sr = OpenFile(file);

            int[] colunas = new int[2];
            colunas[0] = 6;
            colunas[1] = 7;

            string[] dt_s = TxtDt(sr, colunas);

            return dt_s;
        }
        private string[] Fitts(string file)
        {
            StreamReader sr = OpenFile(file);

            int[] colunas = new int[2];
            colunas[0] = 4;
            colunas[1] = 5;

            string[] dt_s = TxtDt(sr, colunas);

            return dt_s;
        }
        private string[] Nback(string file)
        {
            StreamReader sr = OpenFile(file);

            int[] colunas = new int[2];
            colunas[0] = 0;
            colunas[1] = 2;

            string[] dt_s = TxtDt(sr, colunas);

            return dt_s;
        }
        private StreamReader OpenFile(string file)
        {
            string _path = path + file;
            // MessageBox.Show(_path);
            if (!File.Exists(_path))
            {
                //MessageBox.Show("Parece que estão faltando alguns arquivos.");
                return null;
            }
            else
            {
                StreamReader sr = File.OpenText(_path);
                return sr;
            }
        }
        private string[] TxtDt(StreamReader sr, int[] colunas)
        {
            string data = sr.ReadToEnd();

            // Separa os elementos por quebra de linha
            string[] lines = data.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            );

            // Pega as colunas interessantes das linhas
            string[] out1 = new string[lines.Length];
            string[] out2 = new string[lines.Length];

            for (int i = 0; i < lines.Length -1; i++)
            {
                string[] col = lines[i].Split(' ');
                out1[i] = col[colunas[0]];
                out2[i] = col[colunas[1]];
            }
            string[] outf = new string[lines.Length*2];

            // Merge out1 and out2 in outf
            int outf_count = 0;
            for(int i = 0; i < out1.Length; i++)
            {
                outf[outf_count] = out1[i];
                outf_count++;
            }
            for (int i = 0; i < out2.Length; i++)
            {
                outf[outf_count] = out2[i];
                outf_count++;
            }

            return outf;
        }

        private System.Data.DataTable AddToFinal(System.Data.DataTable dt, string name, string[] stroop, string[] fitts, string[] nback)
        {
            System.Data.DataTable table = new System.Data.DataTable();

            DataRow row;
            // DataView view;

            row = table.NewRow();
            row["name"] = name;

            int col_count = 0;

            // MessageBox.Show("Todas as colunas foram geradas", "Log"); 

            for (int i = 0; i < stroop.Length; i++)
            {
                row[(col_count).ToString()] = stroop[i];
                col_count++;
            }
            // MessageBox.Show("Stroop loop finalizado", "Log");

            for (int i = 0; i < fitts.Length; i++)
            {
                row[(col_count).ToString()] = fitts[i];
                col_count++;
            }
            // MessageBox.Show("Fitts loop finalizado", "Log");

            for (int i = 0; i < nback.Length; i++)
            {
                row[(col_count).ToString()] = nback[i];
                col_count++;
            }
            // MessageBox.Show("Nback loop finalizado", "Log");

            dt.Rows.Add(row);

            // view = new DataView(table);
            MessageBox.Show("Retornando 'table'", "log");
            return table;
        }

    }
}

// txt analyse instructions

/* Stroop
 * Colum	Meaning
 * 1        name of block
 * 2        name of the word (e.g., "yellow")
 * 3        the color the word is printed in (e.g., "red")
 * 4        Stroop color match (1=compatible, 0=incompatible)
 * 5        tablerow number
 * 6        the pressed key number
 * 7        Status (1=correct, 2=wrong, 3=timeout)
 * 8        Response time (milliseconds)
*/

/* Fitts
 * Colum    Meaning
 * 1        x-position of stimulus
 * 2        y-position of stimulus
 * 3        size of stimulus
 * 4        distance of stimulus from start position
 * 5        the Fitts' calculation (predicted RT based on distance and size)
 * 6        the response time(ms)
 * 7        status(1=correct, 2=error, 3=too slow)
*/

/* Nback
 * Colum	Meaning
 * 1        correct (1=correct, 2=wrong, 3=too slow)
 * 2        which key was pressed
 * 3        reaction time (ms)
 * 4        random number used for conditions (1=same as 3-back, 2-5 other letter)
 * 5        trial number
 * 6        the current letter
 * 7        the letter of the previous trial
 * 8        the letter of the trial before
*/
