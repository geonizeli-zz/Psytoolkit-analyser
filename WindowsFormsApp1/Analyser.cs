using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace WindowsFormsApp1
{
    class Analyser
    {
        public Analyser(){}
        public string path = "";
        public void ReadDt(DataTable dt, string diretory)
        {
            // Capture path of files
            string[] temp = diretory.Split('\\');
            for (int i = 0; i < temp.Length - 1; i++)
            {
                path = path + temp[i] + "\\";
            }

            // Performs analysis on each 
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Exp1(dt.Rows[i].Field<string>(3));
                Exp2(dt.Rows[i].Field<string>(4));
                Exp3(dt.Rows[i].Field<string>(5));
            }
        }
        private string[] Exp1(string file)
        {
            StreamReader sr = OpenFile(file);

            string[] results = new string[2];

            results[0] = "test";
            results[1] = "test";

            return results;
        }
        private string[] Exp2(string file)
        {
            StreamReader sr = OpenFile(file);

            string[] results = new string[2];

            results[0] = "test";
            results[1] = "test";

            return results;
        }
        private string[] Exp3(string file)
        {
            StreamReader sr = OpenFile(file);

            string[] results = new string[2];

            results[0] = "test";
            results[1] = "test";

            return results;
        }
        private StreamReader OpenFile(string file)
        {
            string _path = path + file;
            MessageBox.Show(_path);
            if (!File.Exists(_path))
            {
                MessageBox.Show("Parece que estão faltnado alguns arquivos.");
                return null;
            }
            else
            {
                StreamReader sr = File.OpenText(_path);
                return sr;
            }
        }
    }
}