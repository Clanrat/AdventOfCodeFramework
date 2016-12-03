using System;

namespace AoCFramework
{
    internal static class ConsoleUtils
    {
        internal static void WriteLineInColor(string line, ConsoleColor color)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(line);
            Console.ForegroundColor = currentColor;
        }
    }
}
