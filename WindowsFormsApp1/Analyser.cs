using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace WindowsFormsApp1
{
    class Analyser
    {
        public Analyser()
        {
        }

        public string path = "";

        public DataTable datatable;

        public System.Data.DataTable ReadDt(System.Data.DataTable dt, string diretory)
        {
            datatable = new DataTable();
                        
            for (int j = 0; j < 161; j++)
            {
                DataColumn column;
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = j.ToString();
                datatable.Columns.Add(column);
            }

            // Capture path of files
            string[] temp = diretory.Split('\\');
            for (int i = 0; i < temp.Length - 1; i++)
            {
                path = path + temp[i] + "\\";
            }

            // temp vars
            string[] txt_datas;
            string[] txt_dataf;
            string[] txt_datan;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                txt_datas = Stroop(dt.Rows[i].Field<string>(3));
                txt_dataf = Fitts(dt.Rows[i].Field<string>(4));
                txt_datan = Nback(dt.Rows[i].Field<string>(5));
                AddToFinal(dt.Rows[i].Field<string>(1), txt_datas, txt_dataf, txt_datan);
            }

            return datatable;
        }
        private string[] Stroop(string file)
        {
            StreamReader sr = OpenFile(file);

            int[] coluna = new int[2];
            coluna[0] = 6;
            coluna[1] = 7;
            int colunas = 8;

            string[] dt_s = TxtDt(sr, coluna, colunas);

            return dt_s;
        }
        private string[] Fitts(string file)
        {
            StreamReader sr = OpenFile(file);

            int[] coluna = new int[2];
            coluna[0] = 4;
            coluna[1] = 5;
            int colunas = 7;

            string[] dt_s = TxtDt(sr, coluna, colunas);

            return dt_s;
        }
        private string[] Nback(string file)
        {
            StreamReader sr = OpenFile(file);

            int[] coluna = new int[2];
            coluna[0] = 0;
            coluna[1] = 2;
            int colunas = 8;

            string[] dt_s = TxtDt(sr, coluna, colunas);

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
        private string[] TxtDt(StreamReader sr, int[] cols, int colx)
        {
            string data = sr.ReadToEnd();

            string temp = Regex.Replace(data, "\\n", "$");
            temp = Regex.Replace(temp, " ", ",");
            string[] lines = temp.Split('$');

            //MessageBox.Show(lines[0]);

            string[] out1 = new string[lines.Length - 1];
            string[] out2 = new string[lines.Length - 1];
            string[] outf = new string[(lines.Length - 1)*2];

            for (int i = 0; i < lines.Length - 1; i++)
            {
                string filter = "([^rs])(?=\\1+)|(rr)(?=r+)|(ss)(?=s+)";
                string not_empy = Regex.Replace(lines[i], filter, String.Empty);

                string[] rows = not_empy.Split(',');

                out1[i] = rows[cols[0]];
                out2[i] = rows[cols[1]];
            }

            // join arrays

            int outf_count = 0;
            for (int i = 0; i < out1.Length; i++)
            {
                outf[outf_count] = out1[i];
                outf_count++;
            }
            for (int i = 0; i < out1.Length; i++)
            {
                outf[outf_count] = out2[i];
                outf_count++;
            }

            return outf;
        }

        private void AddToFinal(string name, string[] stroop, string[] fitts, string[] nback)
        {
            System.Data.DataTable table = new System.Data.DataTable();

            DataRow workRow = datatable.NewRow();
            workRow = datatable.NewRow();

            workRow["0"] = name;

            int col_count = 1;
            // MessageBox.Show(stroop.Length.ToString(), "stroop");
            for (int i = 0; i < stroop.Length; i++)
            {
                
                workRow[(col_count).ToString()] = stroop[i];

                col_count++;
            }

            for (int i = 0; i < fitts.Length; i++)
            {
                workRow[(col_count).ToString()] = fitts[i];

                col_count++;
            }

            for (int i = 0; i < nback.Length; i++)
            {
                workRow[(col_count).ToString()] = nback[i];

                col_count++;
            }

            datatable.Rows.Add(workRow);
        }
    }
}