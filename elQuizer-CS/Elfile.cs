﻿using Microsoft.Win32;
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
        public static List<string> savedPaths = new List<string>();
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

        static public bool saveAsText(string s)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File|*.txt";
            saveFileDialog.Title = "Save your data";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                File.WriteAllText(saveFileDialog.FileName, s);
                return true;
            }
            return false;
        }

        public static void save()
        {

        }
        public static void getLocalPaths()
        {
            string dataDir = Directory.GetCurrentDirectory() + @"\eldata";
            if (Directory.Exists(dataDir))
            {
                savedPaths = Directory.GetFiles(dataDir).ToList();                
            }
            else
            {
                Directory.CreateDirectory(dataDir);
            }

        }
    }
}
