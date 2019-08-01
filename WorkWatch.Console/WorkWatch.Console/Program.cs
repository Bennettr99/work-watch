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
            inputStateManager.ApplicationChanged += OnApplicationChanged;
            do
            {
                // spin
            } while (System.Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static void OnApplicationChanged(object sender, string applicationName)
        {
            System.Console.WriteLine($"Application Name: {applicationName}");
        }

        private static void OnStateUpdated(object sender, DateTime lastInputTime)
        {
            System.Console.WriteLine($"\rLast Input: {lastInputTime:hh:mm:ss fff} {System.Security.Principal.WindowsIdentity.GetCurrent().Name}");
        }
        

        
    }
}