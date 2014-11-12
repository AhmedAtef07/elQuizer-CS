using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elQuizer_CS
{
    class ElFile
    {
        public static string curruntFile;
        public static List<string> savedPaths = new List<string>();
        private static string dataDir = Directory.GetCurrentDirectory() + @"\eldata";
        static public string[] load()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Question Bank|*.qbank";
            openFileDialog.Title = "Open Question Bank";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                curruntFile = extractFileName(openFileDialog.FileName);
                return File.ReadAllLines(openFileDialog.FileName);                
            }
            return null;
        }

        static public string[] load(string path)
        {
            if (File.Exists(path)) {
                curruntFile = extractFileName(path);
                return File.ReadAllLines(path);
            }
            return null;
        }

        static public void updateCurruntFile()
        {
            ElFile.save(curruntFile + ".qbank");
        }
        static public void tryLoadLastAccessedFile()
        {
            string file = getLastAccessedFile();
            if (file == null)
            {
                return;
            }
            ElTools.parseQuestions(ElFile.load(file));
        }

        static public string getLastAccessedFile()
        {
            if (Directory.Exists(dataDir))
            {
                string[] files = Directory.GetFiles(dataDir);
                DateTime dt = new DateTime(1, 1, 1);
                string lastAccessedPath = null;
                foreach (string path in files)
	            {
                    DateTime currDT = File.GetLastAccessTime(path);
                    if (DateTime.Compare(dt, currDT) < 0) 
	                {
                        dt = currDT;
                        lastAccessedPath = path;
	                } 
		 
	            }
                return lastAccessedPath;
            }
            return null;
        }

        public static void duplicate(string filename)
        {

            File.Copy(dataDir + "\\" + filename + ".qbank",
                      dataDir + "\\" + filename + " - Copy.qbank", 
                      true);
        }
        public static void delete(string filename)
        {
            File.Delete(dataDir + "\\" + filename + ".qbank");
        }
        public static void rename(string oldFilename, string newFilename)
        {
            File.Copy(dataDir + "\\" + oldFilename,
                      dataDir + "\\" + newFilename);
            delete(oldFilename);
        }

        static public bool saveAsText(string content)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File|*.txt";
            saveFileDialog.Title = "Save your data";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                File.WriteAllText(saveFileDialog.FileName, content);
                return true;
            }
            return false;
        }

        public static void save(string filename)
        {
            // Check if user removed directory while the program is running.
            File.WriteAllText(dataDir + "\\" + filename,
                string.Join("\n", ElTools.getQuestionFileLines().ToArray()));            
        }

        public static string extractFileName(string fullPath)
        {
            string[] token = fullPath.Split('\\');
            string fileName = token[token.Length - 1];
            string[] fileNameTokens = fileName.Split('.');
            if (fileNameTokens[fileNameTokens.Length - 1] != "qbank" ||
                fileNameTokens.Length == 1)
            {
                return null;
            }
            return fileName.Replace(".qbank", "");
        }
        public static bool export()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Question Bank|*.qbank";
            saveFileDialog.Title = "Save Question Bank";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                File.WriteAllText(saveFileDialog.FileName,
                    string.Join(
                    "\n", ElTools.getQuestionFileLines().ToArray()));
                return true;
            }
            return false;
        }
        public static void getLocalPaths()
        {
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
