using System;
using System.Linq;
using System.Threading.Tasks;
using WorkWatch.Console.Helpers;
using WorkWatch.Services;

namespace WorkWatch.Console
{
    internal class Program
    {
        private static int _userId;
        private static int _applicationId;
        private static int _inputId;
        private static EventsService _eventsService;
        private static WindowHelper _windowHelper;
        private static async Task Main(string[] args)
        {
            if (args.Length < 1)
            {
                System.Console.WriteLine("Must enter api url");
                return;
            }

            _eventsService = new EventsService(args[0]);
            _userId = await _eventsService.GetUserId(System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                System.Environment.MachineName);
            System.Console.WriteLine($"Listening started: {_userId}");

            _windowHelper = new WindowHelper();
            var startingApplicationName = _windowHelper.GetActiveWindowApplication();
            _applicationId = await GetApplicationId(startingApplicationName);

            var inputStateManager = new InputStateManager(startingApplicationName, 500, 5000);
            inputStateManager.InputStarted += OnInputStarted;
            inputStateManager.InputUpdated += OnInputUpdated;
            inputStateManager.ApplicationChanged += OnApplicationChanged;

            do
            {
                // spin
            } while (System.Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static async void OnInputStarted(object sender, DateTime startTime)
        {
            _inputId = await _eventsService.StartInput(_userId, _applicationId);
            System.Console.WriteLine($"Input Started: {startTime:hh:mm:ss fff} {System.Security.Principal.WindowsIdentity.GetCurrent().Name}");
        }

        private static async void OnInputUpdated(object sender, DateTime updateTime)
        {
            await _eventsService.UpdateInput(_inputId);
            System.Console.WriteLine($"Input Updated: {updateTime:hh:mm:ss fff} {System.Security.Principal.WindowsIdentity.GetCurrent().Name}");
        }

        private static async void OnApplicationChanged(object sender, string applicationName)
        {
            _applicationId = await GetApplicationId(applicationName);
            System.Console.WriteLine($"Application Name: {applicationName}");
        }

        private static async Task<int> GetApplicationId(string applicationNam)
        {
            return await _eventsService.GetApplicationId(_userId, applicationNam);
        }

    }
}