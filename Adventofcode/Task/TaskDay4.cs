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
            int[][] wortRichtung = RichtungVomWort;
            int result = WortFindenUndZählenRec(puzzleGrid, data);
            ConsoleHelper.PrintResult("Day4 Part1", result);  // Resultat wiedergeben
            return result;
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
         private int WortFindenUndZählenRec(char[,] puzzleGrid, string[] data)
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

            if(indexWort +1 > wort.Length)  // 
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
            if(puzzleGrid[searchX, searchY] == wort[indexWort])  // Wenn Wort gefunden dann...
            {
                return SucheNachWort(puzzleGrid, data, searchX, searchY, richtung, wort, indexWort);  //...wiederhole
            }
            return false;
            
        }

    }
}
