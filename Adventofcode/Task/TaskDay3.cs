using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
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
            List<string> data = ReadFileDataDay3(FilePath.GetFilePath(this._filename), false);  // Datei auslesen und verarbeiten zu data
            int result = FilterForWordAndCalculateResult(data);  // Wörter filtern und zusammenrechnen
            ConsoleHelper.PrintResult("Day3 Part1", result);  // Resultat wiedergeben
            return result;
        }

        public int ExecutePart2()
        {
            List<string> data = ReadFileDataDay3(FilePath.GetFilePath(this._filename), true);  // Datei auslesen und verarbeiten zu data
            int result = FilterForWordAndCalculateResult(data);  // Wörter filtern und zusammenrechnen
            ConsoleHelper.PrintResult("Day3 Part2", result);  // Resultat wiedergeben
            return result;
        }

        public List<string> ReadFileDataDay3(string filename, bool löschen)  
        {

            const Int32 BufferSize = 128;
            List<string> filteredList = new List<string>();

            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)  // Datei auslesen
                {
                    
                    if (löschen == true)
                    {
                        string wort1 = "don't()";
                        string wort2 = "do()";
                        string löschFilter = $@"{wort1}.*?{wort2}";

                        line = Regex.Replace(line, löschFilter, $"{wort1}{wort2}");

                        if (!line.Substring(line.LastIndexOf(wort1) + wort1.Length).Contains(wort2))  // Wenn don't() ohne
                        {
                            line = line.Substring(0, line.LastIndexOf(wort1) + wort1.Length);
                        }    
                    }
                    string filter = $@"\b\w+\(\d+,\s*\d+\)";  // Vorlage nach der gefiltert wird "mult(zahl, zahl)"

                    MatchCollection matches = Regex.Matches(line, filter);  // Wenn die Zeile mit der Vorlage matcht dann...

                    foreach (Match match in matches)
                    {
                        filteredList.Add(match.Value);  // ...füge der Liste hinzu
                    }
                }

                return filteredList;
            }
        }

        private int FilterForWordAndCalculateResult(List<string> data)
        {
            List<string> filteredWords = new List<string>();  // Liste für rausgefilterte Wörter
            int result = 0;  // Resultat

            string filterWort = "mul";  // Wort nach dem gefiltert wird

            foreach (string word in data)
            {
                if (word.Contains(filterWort))  // Wenn Wort das Filterwort enthält dann...
                {
                    string neuerString = word.Replace("mul", "").Replace("(", "").Replace(")", "").Replace(",", " ");  // Lösche alle Zeichen außer die zwei Zahlen
                    filteredWords.Add(neuerString);  // Zur Liste hinzufügen
                    string[] numbers = neuerString.Split(" ");  // Zahlen splitten
                    int number1 = Convert.ToInt32(numbers.First()); // Zahl zu int convertieren
                    int number2 = Convert.ToInt32(numbers.Last());  // Zahl zu int convertieren
                    result += Math.Abs(number1 * number2);  // Result ausrechnen
                }
            }
            return result;
        }
    } 
}
