using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adventofcode.Utils;

namespace Adventofcode.Task
{
    public class TaskDay1 : ITask
    {
        private readonly string _filename;
        public TaskDay1(string filename)
        {
            this._filename = filename;  
        }

        public int ExecutePart1()
        {
            Tuple<List<int>, List<int>> data = ReadFileData(FilePath.GetFilePath(this._filename));  // Datei auslesen und verarbeiten zu data
            int result = CalculateDistance(data);  // Distanz ausrechnen
            ConsoleHelper.PrintResult("Day1 Part1", result);
            return result;
        }

        public int ExecutePart2()
        {
            Tuple<List<int>, List<int>> data = ReadFileData(FilePath.GetFilePath(this._filename));   // Datei auslesen und verarbeiten
            Dictionary<int, int> similarities = FindSimilarNumbers(data);  // Gleiche Zahlen finden und in Dictionaries speichern
            int result = GleicheZahlenMultiplizierenUndAddieren(similarities);
            ConsoleHelper.PrintResult("Day1 Part2", result);
            return result;
        }


        private Tuple<List<int>, List<int>> ReadFileData(string filename)
        {
            const Int32 BufferSize = 128;
            List<int> Liste1 = new();              // neue Liste für erste Zahl aus dem Array erstellen
            List<int> Liste2 = new();              // neue Liste für zweite Zahl aus dem Array erstellen


            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)  // Datei lesen
                {
                    string[] numbers = line.Split("   ");         // Leerzeichen finden und splitten
                    int number1 = Convert.ToInt32(numbers.First());   // Erste Zahl aus der Datei in int umwandeln  
                    int number2 = Convert.ToInt32(numbers.Last());    // Zweite Zahl aus der Datei in int umwandeln
                    Liste1.Add(number1);     // Erste Zahl in Liste 1 hinzufügen
                    Liste2.Add(number2);     // Zweite Zahl in Liste 2 hinzufügen

                }

            }
            return new Tuple<List<int>, List<int>>(Liste1, Liste2);
        }

        private int CalculateDistance(Tuple<List<int>, List<int>> data)
        {
            List<int> Liste1 = data.Item1;
            List<int> Liste2 = data.Item2;

            // Zahlen sortieren
            Liste1.Sort();
            Liste2.Sort();

            int sumOfDistance = 0;

            for (int i = 0; i < Liste1.Count; i++)
            {
                sumOfDistance += (int)Math.Abs(Liste1[i] - Liste2[i]);  // Differenz berechnen
            }

            return sumOfDistance;
        }



        private Dictionary<int, int> FindSimilarNumbers(Tuple<List<int>, List<int>> data)
        {

            Dictionary<int, int> häufigkeitDerZahlenAusListe2 = new();  // Dictionary 1 für alle Zahlen aus Zahl2 die mehrmals vorkommen, Anzahl der Zahlen
            Dictionary<int, int> zahlenAusListe1DieInListe2Vorkommen = new();  // Dictionary 2 für alle Zahlen aus Zahl1 die auch in Dictionary 1 vorkommen
            List<int> Liste1 = data.Item1;
            List<int> Liste2 = data.Item2;

            foreach (int number in Liste2)
            {
                if (häufigkeitDerZahlenAusListe2.ContainsKey(number))  // Wenn Dic 1 eine Zahl aus Zahl2 enthält dann...
                {
                    häufigkeitDerZahlenAusListe2[number]++;  // true = plus 1 Häufigkeit in Dic 1
                }
                else
                {
                    häufigkeitDerZahlenAusListe2[number] = 1;  // false = Häufigkeit auf 1 gesetzt
                }
            }


            foreach (int number in Liste1)
            {
                if (häufigkeitDerZahlenAusListe2.ContainsKey(number))  // Wenn Dic 1 eine Zahl aus Zahl1 enthält dann...
                {
                    if (zahlenAusListe1DieInListe2Vorkommen.ContainsKey(number))
                    {
                        zahlenAusListe1DieInListe2Vorkommen[number] += häufigkeitDerZahlenAusListe2[number];  // Zahl aus Dic 1 in Dic 2 hinzufügen
                    }
                    else
                    {
                        zahlenAusListe1DieInListe2Vorkommen[number] = häufigkeitDerZahlenAusListe2[number];  // Zahl aus Dic 1 in Dic 2 hinzufügen
                    }
                }
            };
            return zahlenAusListe1DieInListe2Vorkommen;
        }

        private int GleicheZahlenMultiplizierenUndAddieren(Dictionary<int, int> similarities)
        {
            int produkt = 0;  // Produkt

            foreach (var item in similarities)
            {
                produkt += Math.Abs(item.Key * item.Value);  // Zahl mit Anzahl multiplizieren
            }
            return produkt;
        }
    }
}
