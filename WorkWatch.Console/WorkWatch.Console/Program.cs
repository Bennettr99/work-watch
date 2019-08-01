using System;
using WorkWatch.Console.Helpers;

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
            WindowHelper windowHelper = new WindowHelper();
            System.Console.WriteLine($"\rLast Input: {lastInputTime:hh:mm:ss fff} {System.Security.Principal.WindowsIdentity.GetCurrent().Name} {windowHelper.GetActiveWindowApplication()}");
        }
    }
}