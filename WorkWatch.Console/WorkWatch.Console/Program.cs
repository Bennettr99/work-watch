using System;
using System.Linq;
using System.Threading.Tasks;
using WorkWatch.Console.Helpers;
using WorkWatch.Services;

namespace WorkWatch.Console
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            if (args.Length < 1)
            {
                System.Console.WriteLine("Must enter api url");
                return;
            }

            EventsService eventsService = new EventsService(args[0]);
            var userId = await eventsService.GetUserId(System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                System.Environment.MachineName);
            System.Console.WriteLine("Listening started");

            var inputStateManager = new InputStateManager(500, 5000);
            inputStateManager.InputStarted += OnInputStarted;
            inputStateManager.InputUpdated += OnInputUpdated;
            inputStateManager.ApplicationChanged += OnApplicationChanged;

            do
            {
                // spin
            } while (System.Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static void OnInputStarted(object sender, DateTime startTime)
        {
            System.Console.WriteLine($"Input Started: {startTime:hh:mm:ss fff} {System.Security.Principal.WindowsIdentity.GetCurrent().Name}");
        }

        private static void OnInputUpdated(object sender, DateTime updateTime)
        {
            System.Console.WriteLine($"Input Updated: {updateTime:hh:mm:ss fff} {System.Security.Principal.WindowsIdentity.GetCurrent().Name}");
        }

        private static void OnApplicationChanged(object sender, string applicationName)
        {
            System.Console.WriteLine($"Application Name: {applicationName}");
        }

    }
}