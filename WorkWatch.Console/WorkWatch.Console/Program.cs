using System;
using System.Threading;
using System.Timers;
using System.Windows.Threading;
using WorkWatch.Console.Helpers;
using Timer = System.Timers.Timer;

namespace WorkWatch.Console
{
    internal class Program
    {
        private static DateTime? _lastMouseInput;
        private static DateTime? _lastKeyboardInput;
        private static DateTime _lastInput = DateTime.MinValue;

        private static MouseInput mouseInput;
        private static KeyboardInput keyboardInput;
        private static Timer threadTimer;
        private static AllInputSources allInputSources;

        private static void Main(string[] args)
        {
            System.Console.WriteLine("Listening started");

            //var workThread = new Thread(Run);
            //workThread.Start();

            allInputSources = new AllInputSources();
            threadTimer = new System.Timers.Timer(100);
            threadTimer.Elapsed += ThreadTimerOnElapsed;
            threadTimer.AutoReset = true;
            threadTimer.Enabled = true;

            do
            {
                // spin
            } while (System.Console.ReadKey(true).Key != ConsoleKey.Escape);

            threadTimer.Dispose();
            mouseInput.Dispose();
            keyboardInput.Dispose();

            //workThread.Abort();
        }

        private static void Run()
        {
            System.Console.WriteLine("Thread running");

            //mouseInput = new MouseInput();
            //keyboardInput = new KeyboardInput();
            threadTimer = new Timer(100);

            allInputSources = new AllInputSources();
            //mouseInput.MouseMoved += OnMouseInput;
            //keyboardInput.KeyBoardKeyPressed += OnKeyboardInput;

            threadTimer.Elapsed += ThreadTimerOnElapsed;
            threadTimer.AutoReset = true;
            threadTimer.Enabled = true;

            System.Console.WriteLine("Thread still running");

            //using (var )
            //using (var )
            //using (var)
            //{


            //    do
            //    {

            //    } while (true);

            //    //do
            //    //{
            //    //    // spin
            //    //} while (System.Console.ReadKey(true).Key != ConsoleKey.Escape);
            //}
        }

        private static void ThreadTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            var newInputTime = allInputSources.GetLastInputTime();
            _lastInput = newInputTime > _lastInput ? newInputTime : _lastInput;
            PrintOutput();
        }

        private static void PrintOutput()
        {
            System.Console.Write("\r");
            System.Console.Write($"Last Input: {_lastInput.ToLongTimeString()}");
            System.Console.Write($" Mouse: {_lastMouseInput?.ToLongTimeString() ?? "Never"}");
            System.Console.Write($" Keyboard: {_lastKeyboardInput?.ToLongTimeString() ?? "Never"}");

        }

        //private static void OnMouseInput(object sender, EventArgs e)
        //{
        //    System.Console.WriteLine("Mouse input");
        //    _lastMouseInput = DateTime.Now;
        //    //PrintOutput();
        //}

        //private static void OnKeyboardInput(object sender, EventArgs e)
        //{
        //    System.Console.WriteLine("Keyboard input");
        //    _lastKeyboardInput = DateTime.Now;
        //    //PrintOutput();
        //}
    }
}