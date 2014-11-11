using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elQuizer_CS
{
    class Elfile
    {
        static public string[] load()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Question Bank|*.qbank";
            openFileDialog.Title = "Open Question Bank";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                return File.ReadAllLines(openFileDialog.FileName);                
            }
            return null;
        }

        static public string[] load(string path)
        {
            if (File.Exists(path)) {
                return File.ReadAllLines(path);
            }
            return null;
        }
    }
}
