using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
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
            int result = WortFindenUndZählen(puzzleGrid, data);
            ConsoleHelper.PrintResult("Day4 Part1", result);  // Resultat wiedergeben
            return result;
        }

        public int ExecutePart2()
        {
            string[] data = ReadFileDataDay4(FilePath.GetFilePath(this._filename));
            char[,] puzzleGrid = CreatePuzzleGrid(data);
            int result = MusterFindenUndZählen(puzzleGrid, data);
            ConsoleHelper.PrintResult("Day4 Part2", result);  // Resultat wiedergeben
            return result;
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
            char[] wort = { 'X', 'M', 'A', 'S' };  // Buchstaben des gesuchten Wortes
            int wortLänge = wort.Length;
            int anzahl = 0;  // Anzahl der gefundenen Wörter
            string[] reihen = data;
            int reihenAnzahl = reihen.Length;
            int vertikaleReihenAnzahl = reihen[0].Length;

            for (int x = 0; x < reihenAnzahl; x++)  // Gehe jede Reihe durch
            {
                for (int y = 0; y < vertikaleReihenAnzahl; y++)  // Gehe jede vertikale Reihe durch
                {
                    if (puzzleGrid[x, y] == wort[0])  // Wenn irgendwo der erste Buchstabe vom Wort erkannt wird dann...
                    {

                        foreach (var richtung in RichtungVomWort)  // Für jeden Richtungswert
                        {
                            if (SucheNachWort(puzzleGrid,data,x,y,richtung,wort,0))
                            {
                                anzahl++;  // Plus 1 Anzahl
                            }
                        }
                    }
                }
            }
            return anzahl;
        }

        private bool SucheNachWort(char[,] puzzleGrid, string[] data, int searchX, int searchY, int[] richtung, char[] wort, int indexWort)
        {
            indexWort++;  // Counter

            if(indexWort +1 > wort.Length)  // Wenn Index des aktuellen Buchstabens gleich oder größer der Wortlänge dann fertig --> Wort gefunden
            {
                return true;
            }

            searchX += richtung[0];  // Addiere Richtungswert zu koordinate x
            searchY += richtung[1];  // Addiere Richtungswert zu koordinate y

            if (searchX <0 || searchX+1 > data.Length)  
            {
                return false;
            }

            if (searchY < 0 || searchY+1 > data[0].Length)
            {
                return false;
            }
            if(puzzleGrid[searchX, searchY] == wort[indexWort])  // Wenn richtiger Buchstabe gefunden dann...
            {
                return SucheNachWort(puzzleGrid, data, searchX, searchY, richtung, wort, indexWort);  //...wiederhole
            }
            return false;
            
        }

        private int[][] SchrägeRichtungen =
        {
            new int[] {-1, 1},  // rechts runter
            new int[] {-1, -1},  // links runter
            new int[] {1, 1},  // rechts hoch
            new int[] {1, -1},  // links hoch
        };

        private int MusterFindenUndZählen(char[,] puzzleGrid, string[] data)
        {
            char[] wort = {'M', 'A', 'S'};  // Buchstaben des gesuchten Wortes
            int wortLänge = wort.Length;
            int anzahl = 0;  // Anzahl der gefundenen Wörter
            string[] reihen = data;
            int reihenAnzahl = reihen.Length;
            int vertikaleReihenAnzahl = reihen[0].Length;

            for (int x = 0; x < reihenAnzahl; x++)  // Gehe jede Reihe durch
            {
                for (int y = 0; y < vertikaleReihenAnzahl; y++)  // Gehe jede vertikale Reihe durch
                {
                    if (puzzleGrid[x, y] == wort[1])  // Wenn irgendwo der erste Buchstabe vom Wort erkannt wird dann...
                    {

                        foreach (var richtung in SchrägeRichtungen)  // Für jeden Richtungswert
                        {
                            if (SucheNachWort2(puzzleGrid, data, x, y, richtung, wort))
                            {
                                anzahl++;  // Plus 1 Anzahl
                                
                            }
                        }
                    }
                }
            }

            return anzahl/4                ;
        }

        private bool SucheNachWort2(char[,] puzzleGrid, string[] data, int searchX, int searchY, int[] richtung, char[] wort)
        {


            var searchXM = searchX + richtung[0];  // Addiere Richtungswert zu koordinate x
            var searchYM = searchY + richtung[1];  // Addiere Richtungswert zu koordinate y


            var searchXS = searchX -richtung[0];  // Addiere Richtungswert zu koordinate x
            var searchYS = searchY -richtung[1];  // Addiere Richtungswert zu koordinate y

            if (searchXM < 0 || searchXM + 1 > data.Length)
            {
                return false;
            }

            if (searchYM < 0 || searchYM + 1 > data[0].Length)
            {
                return false;
            }


            if (searchXS < 0 || searchXS + 1 > data.Length)
            {
                return false;
            }

            if (searchYS < 0 || searchYS + 1 > data[0].Length)
            {
                return false;
            }

            if (puzzleGrid[searchXM, searchYM] == wort[0] && puzzleGrid[searchXS, searchYS] == wort[2])  // Wenn richtiger Buchstabe gefunden dann...
            {
                return true;
            }
            return false;

        }

    }
}
