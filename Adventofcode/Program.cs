using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Adventofcode.Task;
using Adventofcode.Utils;

public class Example
{
    public static void Main()
    {
        
        Example.Start();
        Example.Day1();
        Example.Day2();
        Example.Day3();
        Example.Stop();

    }

    public static void Start()
    {
        Console.Title = "Advent of Code";
        Console.WriteLine("#################  Advent of Code  #################");
        Console.WriteLine(" ");
    }
    public static void Stop()
    {       
        Console.WriteLine("Press Enter to close...");
        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
    }


    public static void Day1() {

        const string fileName = "day1.txt";
        var day1 = new TaskDay1(fileName);
        day1.ExecutePart1();
        day1.ExecutePart2();
    }

    public static void Day2()
    {

        const string fileName = "day2.txt";
        var day1 = new TaskDay2(fileName);
        day1.ExecutePart1();
        day1.ExecutePart2();
    }

    public static void Day3()
    {

        const string fileName = "day3.txt";
        var day1 = new TaskDay3(fileName);
        day1.ExecutePart1();
        day1.ExecutePart2();
    }

}

