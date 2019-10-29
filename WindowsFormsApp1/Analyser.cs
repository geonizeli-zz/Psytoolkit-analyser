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
                Stroop(dt.Rows[i].Field<string>(3));
                Fitts(dt.Rows[i].Field<string>(4));
                Nback(dt.Rows[i].Field<string>(5));
            }
        }
        private string[] Stroop(string file)
        {
            StreamReader sr = OpenFile(file);

            string[] results = new string[2];

            results[0] = "test";
            results[1] = "test";

            return results;
        }
        private string[] Fitts(string file)
        {
            StreamReader sr = OpenFile(file);

            string[] results = new string[2];

            results[0] = "test";
            results[1] = "test";

            return results;
        }
        private string[] Nback(string file)
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
