﻿using System;

public class Example
{
    public static void Main()
    {
        Example.PrintTimeAndWait();
        Example.PrintTimeAndWait();
        Example.PrintTimeAndWait();

    }

    public static void PrintTimeAndWait() {
        DateTime dat = DateTime.Now;
        Console.WriteLine("The time: {0:d} at {0:t}", dat);
        TimeZoneInfo tz = TimeZoneInfo.Local;
        Console.WriteLine("The time zone: {0}\n",
                          tz.IsDaylightSavingTime(dat) ?
                             tz.DaylightName : tz.StandardName);
        Console.Write("Press <Enter> to exit... ");
        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
    }
}
