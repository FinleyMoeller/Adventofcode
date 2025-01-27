using System;
using System.Globalization;
using System.Linq;
using System.Text;

public class Example
{
    public static void Main()
    {
        
        Example.ReadLine();
        Example.Stop();

    }

    public static void Stop()
    {

        
        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
    }

    public static void ReadLine()
    {
        const Int32 BufferSize = 128;
        const string fileName = "day1.txt";   // Datei finden
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

        // Zahlen sortieren
        zahl1.Sort();
        zahl2.Sort();

        // Jeweils erste Zahl wiedergeben
        Console.Write("Erste Zahl: " + zahl1[0]);
        Console.WriteLine(", ");
        Console.Write("Zweite Zahl: " + zahl2[0]);

        
        int SumOfDistance = 0;


        for (int i = 0; i < zahl1.Count; i++)
        {


            SumOfDistance += (int)Math.Abs(zahl1[i] - zahl2[i]);  // Differenz berechnen
        }

        //Resultat wiedergeben
        Console.WriteLine(", ");
        Console.Write("Das Resultat ist: " + SumOfDistance);

        Console.WriteLine(", ");
        Console.Write("Press <Enter> to exit... ");


        // Gleiche Zahlen in Listen finden
        List<int> similar = new();

        foreach (int i in zahl1)
            
        {
            if (zahl2.Contains(i))
            {
                similar.Add(i);
                Console.Write(similar);
             /*   int Anzahl = similar.Count();
                Console.Write(Anzahl); */
            }
        }

    }
}