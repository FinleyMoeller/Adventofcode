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
        Dictionary<int, int> similarities = Example.FindSimilarNumbers(data);  // Gleiche Zahlen finden und in Dictionaries speichern
        int result = Example.CalculateFrequency(similarities);
        Example.PrintResult(similarities);  // Resultat wiedergeben
        Example.PrintResult(result);  // Resultat wiedergeben
    }


    public static Tuple<List<int>, List<int>> ReadFileData(string fileName)
    {
        const Int32 BufferSize = 128;
        List<int> zahl1 = new();              // neue Liste für erste Zahl aus dem Array erstellen
        List<int> zahl2 = new();              // neue Liste für zweite Zahl aus dem Array erstellen


        using (var fileStream = File.OpenRead(fileName))
        using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
        {
            String line;
            while ((line = streamReader.ReadLine()) != null)  // Datei lesen
            {
                string[] numbers = line.Split("   ");         // Leerzeichen finden und splitten
                int number1 = Convert.ToInt32(numbers.First());   // Erste Zahl aus der Datei in int umwandeln  
                int number2 = Convert.ToInt32(numbers.Last());    // Zweite Zahl aus der Datei in int umwandeln
                zahl1.Add(number1);     // Erste Zahl in Liste 1 hinzufügen
                zahl2.Add(number2);     // Zweite Zahl in Liste 2 hinzufügen

            }

        }



        return new Tuple<List<int>, List<int>>( zahl1, zahl2 );

    }

    public static int CalculateDistance(Tuple<List<int>, List<int>> data)
    {
        List<int> zahl1 = data.Item1;
        List<int> zahl2 = data.Item2;

        // Zahlen sortieren
        zahl1.Sort();
        zahl2.Sort();

        int sumOfDistance = 0;

        for (int i = 0; i < zahl1.Count; i++)
        {

            sumOfDistance += (int)Math.Abs(zahl1[i] - zahl2[i]);  // Differenz berechnen
        }

        return sumOfDistance;
    }


    public static void PrintResult(int result)
    {
        //Resultat wiedergeben
        Console.Write("Das Resultat ist: " + result);
        Console.WriteLine(", ");

    }

    public static void PrintResult(Dictionary<int, int> result)
    {
        //Resultat wiedergeben
        foreach (var item in result)
        { 
            Console.Write($"Zahl: {item.Key} kommt {item.Value} mal vor");  // Key = Zahl aus Dic 2, Value = Anzahl der Zahl in Dic 1
            Console.WriteLine(" ");
        
        }

    }


    public static Dictionary<int, int> FindSimilarNumbers(Tuple<List<int>, List<int>> data)
    {

        Dictionary<int, int> HäufigkeitZahl2 = new();  // Dictionary 1 für alle Zahlen aus Zahl2 die mehrmals vorkommen, Anzahl der Zahlen
        Dictionary<int, int> GleicheZahl = new();  // Dictionary 2 für alle Zahlen aus Zahl1 die auch in Dictionary 1 vorkommen
        List<int> zahl1 = data.Item1;
        List<int> zahl2 = data.Item2;

        foreach (int number in zahl2)
        {
            if (HäufigkeitZahl2.ContainsKey(number))  // Wenn Dic 1 eine Zahl aus Zahl2 enthält dann...
            {
                HäufigkeitZahl2[number]++;  // true = plus 1 Häufigkeit in Dic 1
            }
            else
            {
                HäufigkeitZahl2[number] = 1;  // false = Häufigkeit auf 1 gesetzt
            }
        }
        

        foreach (int number in zahl1)  
        {
            if (HäufigkeitZahl2.ContainsKey(number))  // Wenn Dic 1 eine Zahl aus Zahl1 enthält dann...
            {
                if (GleicheZahl.ContainsKey(number))
                {
                    GleicheZahl[number] += HäufigkeitZahl2[number];  // Zahl aus Dic 1 in Dic 2 hinzufügen
                }
                else {
                    GleicheZahl[number] = HäufigkeitZahl2[number];  // Zahl aus Dic 1 in Dic 2 hinzufügen
                }
            }
        };
        return GleicheZahl;
    }



    public static int CalculateFrequency(Dictionary<int, int> similarities)
    {
        int Produkt = 0;

        foreach (var item in similarities)
        {
            Produkt += Math.Abs(item.Key * item.Value);


        }

        return Produkt;
    }
  
}