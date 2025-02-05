using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adventofcode.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Adventofcode.Task
{
    public class TaskDay4
    {
        private readonly string _filename;
        public TaskDay4(string filename)
        {
            this._filename = filename;
        }

        public int ExecutePart1()
        {
            string[] data = ReadFileDataDay4(FilePath.GetFilePath(this._filename));
            char[,] puzzleGrid = CreatePuzzleGrid(data);
            int[][] wortRichtung = RichtungVomWort;
            int anzahl = WortFindenUndZählen(puzzleGrid, data);
            return 0;
        }

        public int ExecutePart2()
        {
            return 0;
        }

        public string[] ReadFileDataDay4(string filename)
        {
            string[] data = File.ReadAllLines(filename);
            return data;
        }

        private char[,] CreatePuzzleGrid(string[] data)
        {
            string[] reihen = data;  // horizontale Reihen
            int reihenAnzahl = reihen.Length;  // Anzahl der horizontalen Reihen
            int vertikaleReihenAnzahl = reihen[0].Length;  // Anzahl der vertikalen Reihen

            char[,] puzzleGrid = new char[reihenAnzahl, vertikaleReihenAnzahl];  // erstelle ein Grid aus den Reihen

            for (int i = 0; i < reihenAnzahl; i++)
            {
                for (int a = 0; a < vertikaleReihenAnzahl; a++)
                {
                    puzzleGrid[i, a] = reihen[i][a];  // füge die Werte dem Grid hinzu
                }
            }

            return puzzleGrid;
        }

        private int[][] RichtungVomWort =
        {
            new int[] {0, 1}, // rechts
            new int[] {0, -1},  // links
            new int[] {1, 0},  // hoch
            new int[] {-1, 0}, // runter
            new int[] {-1, 1},  // rechts runter
            new int[] {-1, -1},  // links runter
            new int[] {1, 1},  // rechts hoch
            new int[] {1, -1},  // links hoch
        };

        private int WortFindenUndZählen(char[,] puzzleGrid, string[] data)
        {
            char[] wort = {'X', 'M', 'A', 'S'};
            int wortLänge = wort.Length;
            int anzahl = 0;
            string[] reihen = data;
            int reihenAnzahl = reihen.Length;
            int vertikaleReihenAnzahl = reihen[0].Length;

            for (int x = 0; x < reihenAnzahl; x++)  // Gehe jede Reihe durch
            {
                for (int y = 0; y < vertikaleReihenAnzahl; y++)  // Gehe jede vertikale Reihe durch
                {
                    if (puzzleGrid[x, y] == wort[0])  // Wenn irgendwo der erste Buchstabe vom Wort erkannt wird dann...
                    {

                        var buchstabe = puzzleGrid[x, y];

                        var start = new int[] {x, y};

                        for (int i = 0; i < wortLänge; i++)  // Loop - für jeden Buchstaben im Wort einmal durchführen
                        {
                            foreach (var richtung in RichtungVomWort)  // Für jede mögliche Richtung ausführen
                            {
                                int searchX = x + richtung[0];  // Richtungswerte den Koordinaten hinzufügen
                                int searchY = y + richtung[1];  // Richtungswerte den Koordinaten hinzufügen
                                int index = 1;

                                if (puzzleGrid[searchX, searchY] == wort[index])  // Wenn Buchstabe in Koordinate vorkommt dann...
                                {
                                    buchstabe = puzzleGrid[searchX, searchY];
                                    index++;
                                    return index;
                                }
                                
                            }
                            
                        }
                    }
                }
            }
            return anzahl;
        }
    }
}
