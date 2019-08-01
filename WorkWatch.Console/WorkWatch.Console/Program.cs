using System;

namespace WorkWatch.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            System.Console.WriteLine("Listening started");

            var inputStateManager = new InputStateManager(500);
            inputStateManager.StateUpdated += OnStateUpdated;
            do
            {
                // spin
            } while (System.Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static void OnStateUpdated(object sender, DateTime lastInputTime)
        {
            System.Console.Write($"\rLast Input: {lastInputTime:hh:mm:ss fff}");
        }
    }
}