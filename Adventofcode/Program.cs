﻿using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

public class Example
{
    public static void Main()
    {

        Example.Day1DistanceCalculate();
        Example.Day1Part2Calculate();
        Example.Day2Part1SafetyReport();
        Example.Stop();

    }

    public static void Stop()
    {
        Console.WriteLine("Press Enter to close...");
        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
    }


    public static void Day1DistanceCalculate() {

        const string fileName = "day1.txt";   // Datei finden
        Tuple<List<int>, List<int>> data = Example.ReadFileData(fileName);  // Datei auslesen und verarbeiten zu data
        int reult = Example.CalculateDistance(data);  // Distanz ausrechnen
        Example.PrintResult(reult);  // Resultat wiedergeben

    }


    public static void Day1Part2Calculate()
    {

        const string fileName = "day1.txt";  // Datei finden
        Tuple<List<int>, List<int>> data = Example.ReadFileData(fileName);  // Datei auslesen und verarbeiten
        Dictionary<int, int> similarities = Example.findSimilarNumbers(data);  // Gleiche Zahlen finden und in Dictionaries speichern
        int result = Example.gleicheZahlenMultiplizierenUndAddieren(similarities);
        Example.PrintResult(similarities);  // Resultat wiedergeben
        Example.PrintResult(result);  // Resultat wiedergeben
    }

    public static void Day2Part1SafetyReport()
    {

        const string fileName = "day2.txt";   // Datei finden
        List<int[]> data = Example.readFileDataDay2(fileName);  // Datei auslesen und verarbeiten zu data
        List<int[]> istSicher = Example.checkSafety(data);  // Sichere Reports sammeln
        int anzahlSichererReports = Example.sichereReportsZählen(istSicher);  // Sichere Reports zählen


    }


    public static Tuple<List<int>, List<int>> ReadFileData(string fileName)
    {
        const Int32 BufferSize = 128;
        List<int> Liste1 = new();              // neue Liste für erste Zahl aus dem Array erstellen
        List<int> Liste2 = new();              // neue Liste für zweite Zahl aus dem Array erstellen


        using (var fileStream = File.OpenRead(fileName))
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

    public static List<int[]> readFileDataDay2(string fileName)
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

    public static int CalculateDistance(Tuple<List<int>, List<int>> data)
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


    public static void PrintResult(int result)
    {
        //Resultat wiedergeben
        Console.Write("Das Resultat ist: " + result + ".");
        Console.WriteLine(" ");

    }

    public static void PrintResult(Dictionary<int, int> result)
    {
        Console.WriteLine("Häufigkeit der Zahlen aus Liste 1 in Liste 2...");
        //Resultat wiedergeben
        foreach (var item in result)
        {
            Console.Write($"Zahl: {item.Key} kommt {item.Value} mal vor");  // Key = Zahl aus Dic 2, Value = Anzahl der Zahl in Dic 1
            Console.WriteLine(" ");

        }

    }



    public static Dictionary<int, int> findSimilarNumbers(Tuple<List<int>, List<int>> data)
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
                else {
                    zahlenAusListe1DieInListe2Vorkommen[number] = häufigkeitDerZahlenAusListe2[number];  // Zahl aus Dic 1 in Dic 2 hinzufügen
                }
            }
        };
        return zahlenAusListe1DieInListe2Vorkommen;
    }



    public static int gleicheZahlenMultiplizierenUndAddieren(Dictionary<int, int> similarities)
    {
        int Produkt = 0;  // Produkt

        foreach (var item in similarities)
        {
            Produkt += Math.Abs(item.Key * item.Value);  // Zahl mit Anzahl multiplizieren


        }

        return Produkt;
    }

    public static List<int[]> checkRequirements = new List<int[]> bereitsKorrigiert();
    public static bool IsZeileAufsteigend(int[] zeile)  // Überprüfen ob die Zeilen aufsteigend sind
    {

        for (int i = 0; i < zeile.Length - 1; i++)  // Loop --> checkt wie lang die Zeile ist und macht für jede Zahl in der Zeile einen Durchlauf
        {
            if (zeile[i] >= zeile[i + 1])  // Wenn Zahl größer als nächste Zahl dann falsch, aka nicht aufsteigend
            {
                zeile = zeile.Except(new int[] { zeile[i + 1] }).ToArray();
                for (int a = 0; a < zeile.Length - 1; a++)
                {
                    if (zeile[a] >= zeile[a + 1])
                    {
                        return false;
                    }
                }
            }
        }

        return true; // Ansonstend richtig, aka aufsteigend
    }


    public static bool IsZeileAbsteigend(int[] zeile)  // Überprüfen ob die Zeilen absteigend sind
    {

        for (int i = 0; i < zeile.Length - 1; i++)  // Loop --> checkt wie lang die Zeile ist und macht für jede Zahl in der Zeile einen Durchlauf
        {
            if (zeile[i] <= zeile[i + 1])  // Wenn Zahl kleiner als nächste Zahl dann falsch, aka nicht absteigend
            {
                zeile = zeile.Except(new int[] { zeile[i + 1] }).ToArray();
                for (int a = 0; a < zeile.Length - 1; a++)
                {
                    if (zeile[a] <= zeile[a + 1])
                    {
                        return false;
                    }
                }
            }
        }

        return true;  // Ansonstend richtig, aka absteigend
    }


    public static bool checkDifference(int[] zeile, bool doCleanup)  // Überprüfe die Differenz der einzelnen Zahlen innerhalb einer Reihe
    {
        for (int i = 0; i < zeile.Length - 1; i++)  // Loop --> checkt wie lang die Zeile ist und macht für jede Zahl in der Zeile einen Durchlauf
        {
            int differenz = Math.Abs(zeile[i] - zeile[i + 1]);  // Differenz der Zahl und der nächsten Zahl ausrechnen

            if (differenz > 3 || differenz <= 0)  // Wenn Differenz über drei oder unter/gleich null dann falsch
            {
                if (doCleanup)
                {
                    zeile = zeile.Except(new int[] { zeile[i + 1] }).ToArray();  // Falsche Zahl löschen

                    for (int a = 0; a < zeile.Length - 1; a++)
                    {
                        int differenz2 = Math.Abs(zeile[a] - zeile[a + 1]);  // Nochmal Differenz ausrechnen
                        if (differenz2 > 3 || differenz2 <= 0)  // Wenn Differenz immer noch über drei oder unter/gleich null dann falsch
                        {
                            return false;
                        }
                    }
                }
                else {
                    return false;
                }
            }
        }

        return true;  // Ansonsten richtig
    }




    public static List<int[]> checkSafety(List<int[]> data)  // Überprüft ob die einzelnen Zeilen den Anforderungen entsprechen, aka sicher sind
    {
        List<int[]> sichereZeilen = new();  // Neue Liste für die sicheren Zeilen
        List<int[]> NICHTsichereZeilen = new();  // Neue Liste für die sicheren Zeilen

        foreach (int[] zeile in data)
        {
            var istAufsteigend = IsZeileAufsteigend(zeile);

            if (istAufsteigend)  // Wenn aufsteigend und...
            {

                if (checkDifference(zeile, true))
                // ...wenn Differenz stimmt dann füge Zeile zur Liste hinzu
                {
                    sichereZeilen.Add(zeile);
                }
            }
            else
            {
                var istAbsteigend = IsZeileAbsteigend(zeile);

                if (istAbsteigend)  // Wenn absteigend und...
                {
                    if (checkDifference(zeile, true))  // ...wenn Differenz stimmt dann füge Zeile zur Liste hinzu
                    {
                        sichereZeilen.Add(zeile);
                    }
                }
                else
                {
                    NICHTsichereZeilen.Add(zeile);
                }
            }




        }


        foreach (int[] zeile in NICHTsichereZeilen)
        {
            var istAufsteigend = IsZeileAufsteigend(zeile, true);

            if (istAufsteigend)  // Wenn aufsteigend und...
            {

                if (checkDifference(zeile, false))  // ...wenn Differenz stimmt dann füge Zeile zur Liste hinzu
                {
                    sichereZeilen.Add(zeile);
                }
            }
            else
            {
                var istAbsteigend = IsZeileAbsteigend(zeile, true);

                if (istAbsteigend)  // Wenn absteigend und...
                {
                    if (checkDifference(zeile, false))  // ...wenn Differenz stimmt dann füge Zeile zur Liste hinzu
                    {
                        sichereZeilen.Add(zeile);
                    }
                }
            }
        }
        return sichereZeilen;
    }

    public static int sichereReportsZählen (List<int[]> istSicher)  // Zählt die Anzahl der sicheren Zeilen
    {
        int anzahl = 0 ;  // Anzahl der sicheren Zeilen

        foreach (int[] report in istSicher)  // Bei jedem Zahlen array in der Liste...
        {
            anzahl++;  // ...füge einen Wert der Anzahl hinzu
        }
        Console.WriteLine($"Anzahl sicherer Reports: {anzahl}");  // Gibt die Anzahl wieder
        return anzahl;

    }










}

