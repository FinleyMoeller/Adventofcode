using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Adventofcode.Utils;
using Microsoft.VisualBasic;

namespace Adventofcode.Task
{
    public class TaskDay3
    {
        private readonly string _filename;
        public TaskDay3(string filename)
        {
            this._filename = filename;
        }

        public int ExecutePart1()
        {
            List<string> data = ReadFileDataDay3(FilePath.GetFilePath(this._filename));  // Datei auslesen und verarbeiten zu data
            List<string> filteredWords = FilterForWord(data);
            return 0;
        }

        public int ExecutePart2()
        {
            return 0;
        }

        private List<string> ReadFileDataDay3(string filename)
        {

            const Int32 BufferSize = 128;
            List<string> filteredList = new List<string>();

            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)  // Datei lesen
                {
                    string pattern = $@"\b\w+\(\d+,\s*\d+\)";

                    MatchCollection matches = Regex.Matches(line, pattern);

                    foreach (Match match in matches)
                    {
                        filteredList.Add(match.Value);
                    }
                }

                return filteredList;
            }
        }

        private List<string> FilterForWord(List<string> data)
        {
            List<string> filteredWords = new List<string>();
            string filterWort = "mul";

            foreach (string word in data)
            {
                if (word.Contains(filterWort))
                {
                    string neuerString = word.Replace("mul", "").Replace("(", "").Replace(")", "").Replace(",", " ");
                    filteredWords.Add(neuerString);
                }
            }
            return filteredWords;
        }
    } 
}
