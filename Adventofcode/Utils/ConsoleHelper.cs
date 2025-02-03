using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventofcode.Utils
{
    public static class ConsoleHelper
    {
        public static void PrintResult(string taskname, int result)
        {
            Console.WriteLine($"===============[ Aufgabe {taskname} ]===============");
            Console.WriteLine($"Das Resultat für die Aufgabe ist: { result }.");
            Console.WriteLine($" ");
        }
    }
}
