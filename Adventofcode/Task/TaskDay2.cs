using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adventofcode.Utils;

namespace Adventofcode.Task
{
    public class TaskDay2 : ITask
    {
        private readonly string _filename;
        public TaskDay2(string filename)
        {
            this._filename = filename;
        }

        public int ExecutePart1()
        {
            List<int[]> data = ReadFileDataDay2(FilePath.GetFilePath(this._filename));  // Datei auslesen und verarbeiten zu data
            List<int[]> istSicher = checkSafety(data);  // Sichere Reports sammeln
            int anzahlSichererReports = sichereReportsZählen(istSicher);  // Sichere Reports zählen
            ConsoleHelper.PrintResult("Day2 Part1", anzahlSichererReports);
            return anzahlSichererReports;
        }

        public int ExecutePart2()
        {
           return 0;
        }
        private List<int[]> ReadFileDataDay2(string fileName)
        {
            const Int32 BufferSize = 128;
            List<int[]> zahlenblöcke = new();

            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)  // Datei lesen
                {

                    string[] numbers = line.Split(" ");
                    int[] zahlenblock = Array.ConvertAll(numbers, int.Parse);
                    zahlenblöcke.Add(zahlenblock);
                }
            }
            return zahlenblöcke;
        }

        private bool IsZeileAufsteigend(ref int[] zeile, bool doCleanup)  // Überprüfen ob die Zeilen aufsteigend sind
        {

            for (int i = 0; i < zeile.Length - 1; i++)  // Loop --> checkt wie lang die Zeile ist und macht für jede Zahl in der Zeile einen Durchlauf
            {


                if (zeile[i] >= zeile[i + 1])  // Wenn Zahl größer als nächste Zahl dann falsch, aka nicht aufsteigend
                {
                    if (doCleanup)
                    {
                        if (zeile.Length > (i + 2))
                        {

                            if (zeile[i + 1] < zeile[i + 2])
                            {
                                if (i != 0 && zeile[i - 1] == zeile[i + 1])
                                {
                                    zeile = zeile.Where((val, index) => index != i + 1).ToArray();  // Lösche nächste Zahl

                                    for (int a = 0; a < zeile.Length - 1; a++)
                                    {
                                        if (zeile[a] >= zeile[a + 1])  // Nochmal auf chronologie checken
                                        {
                                            return false;
                                        }
                                    }
                                    return true;
                                }

                                else if (i != 0 && zeile[i - 1] > zeile[i + 1])  // Wenn Zahl größer als nächste Zahl & nächste Zahl kleiner als übernächste Zahl & Zahl nicht index 0 & vorherige Zahl größer als nächste Zahl
                                {
                                    zeile = zeile.Where((val, index) => index != i + 1).ToArray();  // Lösche nächste Zahl

                                    for (int a = 0; a < zeile.Length - 1; a++)
                                    {
                                        if (zeile[a] >= zeile[a + 1])  // Nochmal auf chronologie checken
                                        {
                                            return false;
                                        }
                                    }
                                    return true; // Ansonstend richtig, aka aufsteigend
                                }

                                else if (Math.Abs(zeile[i + 1] - zeile[i + 2]) > 3 || Math.Abs(zeile[i + 1] - zeile[i + 2]) <= 0)
                                {
                                    zeile = zeile.Where((val, index) => index != i + 1).ToArray();  // Lösche Zahl

                                    for (int a = 0; a < zeile.Length - 1; a++)
                                    {
                                        if (zeile[a] >= zeile[a + 1])  // Nochmal auf chronologie checken
                                        {
                                            return false;
                                        }
                                    }
                                    return true; // Ansonstend richtig, aka aufsteigend
                                }

                                else
                                    zeile = zeile.Where((val, index) => index != i).ToArray();  // Lösche Zahl

                                for (int a = 0; a < zeile.Length - 1; a++)
                                {
                                    if (zeile[a] >= zeile[a + 1])  // Nochmal auf chronologie checken
                                    {
                                        return false;
                                    }
                                }
                                return true; // Ansonstend richtig, aka aufsteigend

                            }
                        }
                        // [ Wenn die jetzige Zahl das Problem ist [ 1 3 2 4, i = 1 ]]
                        else if (i != 0 && zeile[i - 1] < zeile[i + 1] && zeile[i - 1] != zeile[i + 1])  // wenn Zahl größer als nächste Zahl & Zahl nicht index 0 & vorherige Zahl kleiner als nächste Zahl
                        {
                            zeile = zeile.Where((val, index) => index != i).ToArray();  // Lösche Zahl

                            for (int a = 0; a < zeile.Length - 1; a++)
                            {
                                if (zeile[a] >= zeile[a + 1])  // Nochmal auf chronologie checken
                                {
                                    return false;
                                }
                            }
                            return true; // Ansonstend richtig, aka aufsteigend
                        }



                        else
                        {
                            zeile = zeile.Where((val, index) => index != i + 1).ToArray();  // Lösche nächste Zahl
                        }

                        for (int a = 0; a < zeile.Length - 1; a++)
                        {
                            if (zeile[a] >= zeile[a + 1])  // Nochmal auf chronologie checken
                            {
                                return false;
                            }
                        }
                        return true; // Ansonstend richtig, aka aufsteigend

                    }
                    return false;
                }


            }

            return true; // Ansonstend richtig, aka aufsteigend
        }


        private bool IsZeileAbsteigend(ref int[] zeile, bool doCleanup)  // Überprüfen ob die Zeilen absteigend sind
        {

            for (int i = 0; i < zeile.Length - 1; i++)  // Loop --> checkt wie lang die Zeile ist und macht für jede Zahl in der Zeile einen Durchlauf
            {
                if (zeile[i] <= zeile[i + 1])  // Wenn Zahl kleiner als nächste Zahl dann falsch, aka nicht absteigend
                {
                    if (doCleanup)
                    {
                        if (zeile.Length > (i + 2))
                        {
                            if (zeile[i + 1] > zeile[i + 2])
                            {
                                if (i != 0 && zeile[i - 1] == zeile[i + 1])
                                {
                                    zeile = zeile.Where((val, index) => index != i + 1).ToArray();  // Lösche nächste Zahl

                                    for (int a = 0; a < zeile.Length - 1; a++)
                                    {
                                        if (zeile[a] <= zeile[a + 1])  // Nochmal auf chronologie checken
                                        {
                                            return false;
                                        }
                                    }
                                    return true;
                                }

                                else if (i != 0 && zeile[i - 1] < zeile[i + 1])  // Wenn Zahl kleiner als nächste Zahl & nächste Zahl größer als übernächste Zahl & Zahl nicht index 0 & vorherige Zahl kleiner als nächste Zahl
                                {
                                    zeile = zeile.Where((val, index) => index != i + 1).ToArray();  // Lösche nächste Zahl

                                    for (int a = 0; a < zeile.Length - 1; a++)
                                    {
                                        if (zeile[a] <= zeile[a + 1])  // Nochmal auf chronologie checken
                                        {
                                            return false;
                                        }
                                    }
                                    return true; // Ansonstend richtig, aka aufsteigend
                                }

                                else if (Math.Abs(zeile[i + 1] - zeile[i + 2]) > 3 || Math.Abs(zeile[i + 1] - zeile[i + 2]) <= 0)
                                {
                                    zeile = zeile.Where((val, index) => index != i + 1).ToArray();  // Lösche Zahl

                                    for (int a = 0; a < zeile.Length - 1; a++)
                                    {
                                        if (zeile[a] <= zeile[a + 1])  // Nochmal auf chronologie checken
                                        {
                                            return false;
                                        }
                                    }
                                    return true; // Ansonstend richtig, aka aufsteigend
                                }

                                else
                                    zeile = zeile.Where((val, index) => index != i).ToArray();  // Lösche Zahl

                                for (int a = 0; a < zeile.Length - 1; a++)
                                {
                                    if (zeile[a] <= zeile[a + 1])  // Nochmal auf chronologie checken
                                    {
                                        return false;
                                    }
                                }
                                return true; // Ansonstend richtig, aka aufsteigend

                            }

                            // [ Wenn die jetzige Zahl das Problem ist ]
                            else if (i != 0 && zeile[i - 1] > zeile[i + 1] && zeile[i - 1] != zeile[i + 1])  // Wenn Zahl kleiner als nächste Zahl & Zahl nicht index 0 & vorherige Zahl größer als nächste Zahl
                            {
                                zeile = zeile.Where((val, index) => index != i).ToArray();  // Lösche Zahl

                                for (int a = 0; a < zeile.Length - 1; a++)
                                {
                                    if (zeile[a] <= zeile[a + 1])  // Nochmal auf chronologie checken
                                    {
                                        return false;
                                    }
                                }
                                return true; // Ansonstend richtig, aka aufsteigend
                            }

                        }
                        else
                        {
                            zeile = zeile.Where((val, index) => index != i + 1).ToArray();  // Lösche nächste Zahl
                        }

                        for (int a = 0; a < zeile.Length - 1; a++)
                        {
                            if (zeile[a] <= zeile[a + 1])  // Nochmal auf chronologie checken
                            {
                                return false;
                            }

                        }
                        return true; // Ansonstend richtig, aka aufsteigend

                    }
                    return false;
                }

            }

            return true;  // Ansonstend richtig, aka absteigend
        }


        private bool checkDifference(ref int[] zeile, bool doCleanup)  // Überprüfe die Differenz der einzelnen Zahlen innerhalb einer Reihe
        {

            for (int i = 0; i < zeile.Length - 1; i++)  // Loop --> checkt wie lang die Zeile ist und macht für jede Zahl in der Zeile einen Durchlauf
            {

                int differenz = Math.Abs(zeile[i] - zeile[i + 1]);  // Differenz der Zahl und der nächsten Zahl ausrechnen

                if (differenz > 3 || differenz <= 0)  // Wenn Differenz über drei oder unter/gleich null
                {
                    if (doCleanup)  // Wenn CleanUp erlaubt
                    {
                        if (zeile.Length > (i + 2))
                        {
                            if (differenz > 3 || differenz <= 0 && Math.Abs(zeile[i + 1] - zeile[i + 2])! > 3 || Math.Abs(zeile[i + 1] - zeile[i + 2])! <= 0)  // Wenn Differenz zwischen ersten beiden Zahlen falsch und zwischen nächsten beiden richtig
                            {
                                zeile = zeile.Where((val, index) => index != i).ToArray();  // lösche jetzige Zahl

                                for (int a = 0; a < zeile.Length - 1; a++)  // Loop
                                {
                                    int differenz2 = Math.Abs(zeile[a] - zeile[a + 1]);  // Nochmal Differenz ausrechnen
                                    if (differenz2 > 3 || differenz2 <= 0)  // Wenn Differenz immer noch über drei oder unter/gleich null dann falsch
                                    {
                                        return false;
                                    }

                                }
                                return true; // Ansonstend richtig, aka aufsteigend
                            }

                        }
                        else
                        {
                            zeile = zeile.Where((val, index) => index != i + 1).ToArray();  // Falsche Zahl löschen
                        }

                        for (int a = 0; a < zeile.Length - 1; a++)
                        {
                            int differenz2 = Math.Abs(zeile[a] - zeile[a + 1]);  // Nochmal Differenz ausrechnen
                            if (differenz2 > 3 || differenz2 <= 0)  // Wenn Differenz immer noch über drei oder unter/gleich null dann falsch
                            {
                                return false;
                            }

                        }

                        return true; // Ansonstend richtig, aka aufsteigend
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;  // Ansonsten richtig
        }
        private List<int[]> checkSafety(List<int[]> data)  // Überprüft ob die einzelnen Zeilen den Anforderungen entsprechen, aka sicher sind
        {
            List<int[]> korrigierteZeilen = new();
            List<int[]> sichereZeilen = new();  // Neue Liste für die sicheren Zeilen
            List<int[]> NICHTsichereZeilen = new();  // Neue Liste für die sicheren Zeilen
            List<int[]> schlechteZeilen = new();  // Neue Liste für die sicheren Zeilen


            foreach (int[] zeile in data)
            {
                int[] neueZeile = zeile;
                var istAufsteigend = IsZeileAufsteigend(ref neueZeile, false);

                if (istAufsteigend)  // Wenn aufsteigend und...
                {

                    if (checkDifference(ref neueZeile, true))  // ...wenn Differenz stimmt dann füge Zeile zur Liste hinzu
                    {
                        sichereZeilen.Add(neueZeile);
                    }
                    else
                    {
                        NICHTsichereZeilen.Add(neueZeile);  // Ansonsten füge Zeilen zur Liste2 hinzu
                    }
                }
                else
                {
                    neueZeile = zeile;
                    var istAbsteigend = IsZeileAbsteigend(ref neueZeile, false);

                    if (istAbsteigend)  // Wenn absteigend und...
                    {
                        if (checkDifference(ref neueZeile, true))  // ...wenn Differenz stimmt dann füge Zeile zur Liste hinzu
                        {
                            sichereZeilen.Add(neueZeile);
                        }
                        else
                        {
                            NICHTsichereZeilen.Add(neueZeile);  // Ansonsten füge Zeilen zur Liste2 hinzu
                        }
                    }
                    else
                    {
                        NICHTsichereZeilen.Add(neueZeile);  // Ansonsten füge Zeilen zur Liste2 hinzu
                    }
                }
            }



            foreach (int[] zeile in NICHTsichereZeilen)
            {
                int[] neueZeile = zeile;
                var istAufsteigend = IsZeileAufsteigend(ref neueZeile, true);

                if (istAufsteigend)  // Wenn aufsteigend und...
                {

                    if (checkDifference(ref neueZeile, false))  // ...wenn Differenz stimmt dann füge Zeile zur Liste hinzu
                    {
                        korrigierteZeilen.Add(neueZeile);
                        sichereZeilen.Add(neueZeile);
                    }
                }
                else
                {
                    neueZeile = zeile;
                    var istAbsteigend = IsZeileAbsteigend(ref neueZeile, true);

                    if (istAbsteigend)  // Wenn absteigend und...
                    {
                        if (checkDifference(ref neueZeile, false))  // ...wenn Differenz stimmt dann füge Zeile zur Liste hinzu
                        {
                            korrigierteZeilen.Add(neueZeile);
                            sichereZeilen.Add(neueZeile);
                        }

                    }
                    else
                    {
                        schlechteZeilen.Add(zeile);  // Ansonsten füge Zeile zu unsicheren Zeilen hinzu
                    }
                }
            }
            return sichereZeilen;
        }


        private int sichereReportsZählen(List<int[]> istSicher)  // Zählt die Anzahl der sicheren Zeilen
        {
            int anzahl = 0;  // Anzahl der sicheren Zeilen

            foreach (int[] report in istSicher)  // Bei jedem Zahlen array in der Liste...
            {
                anzahl++;  // ...füge einen Wert der Anzahl hinzu
            }
            return anzahl;

        }

    }
}