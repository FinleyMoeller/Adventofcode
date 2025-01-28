using System;
using System.Globalization;
using System.Linq;
using System.Text;

public class Example
{
    public static void Main()
    {

        Example.Day1DistanceCalculate();
        Example.Day1Part2Calculate();
        Example.Stop();

    }

    public static void Stop()
    {
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
        List<int> data = Example.readFileDataDay2(fileName);  // Datei auslesen und verarbeiten zu data
        bool safety = Example.checkSafety(data);
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



        return new Tuple<List<int>, List<int>>( Liste1, Liste2 );

    }

    public static List<int> readFileDataDay2(string fileName)
    {
        const Int32 BufferSize = 128;
        List<int> zahlenblöcke = new();

        using (var fileStream = File.OpenRead(fileName))
        using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
        {
            string line;
            while ((line = streamReader.ReadLine()) != null)  // Datei lesen
            {
                int zahlenblock = Convert.ToInt32(line);
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
        int Produkt = 0;

        foreach (var item in similarities)
        {
            Produkt += Math.Abs(item.Key * item.Value);


        }

        return Produkt;
    }


    public static bool checkSafety(List<int> list)
    {
        foreach (var line in list)
        {
            int vorherigeZahl = int.MinValue;
            bool chronologisch = true;

            foreach (int i in line)
            {
                if (i > vorherigeZahl)
                {
                    chronologisch = true;
                    vorherigeZahl = i;
                }
                else if (i < vorherigeZahl)
                {
                    chronologisch = true;
                    vorherigeZahl = i;
                }
                else
                {
                    return false;
                }
              
            }
          
        }
    }


  
}