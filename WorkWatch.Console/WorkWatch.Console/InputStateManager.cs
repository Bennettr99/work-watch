using System;
using System.Timers;
using WorkWatch.Console.Helpers;

namespace WorkWatch.Console
{
    public class InputStateManager
    {
        private const int UpdateFrequencyMilliseconds = 100;
        private readonly AllInputSources _inputSources;

        private readonly int _maxElapsedTimeMilliseconds;
        private readonly int _inputUpdateFrequencyMilliseconds;
        private readonly WindowHelper _windowHelper;
        private string _lastApplicationName = string.Empty;
        private DateTime _lastInputDateTime = DateTime.MinValue;
        private DateTime _lastInputUpdateDateTime = DateTime.MinValue;

        public InputStateManager(string startingApplicationName, int maxElapsedTimeMilliseconds,
            int inputUpdateFrequencyMilliseconds)
        {
            _lastApplicationName = startingApplicationName;
            _maxElapsedTimeMilliseconds = maxElapsedTimeMilliseconds;
            _inputUpdateFrequencyMilliseconds = inputUpdateFrequencyMilliseconds;
            _inputSources = new AllInputSources();
            _windowHelper = new WindowHelper();
            var stateChangeTimer = new Timer(UpdateFrequencyMilliseconds);
            stateChangeTimer.Elapsed += UpdateInputState;
            stateChangeTimer.Elapsed += UpdateApplicationState;
            stateChangeTimer.AutoReset = true;
            stateChangeTimer.Enabled = true;
        }

        public event EventHandler<DateTime> InputStarted;
        public event EventHandler<DateTime> InputUpdated;
        public event EventHandler<string> ApplicationChanged;

        private void UpdateInputState(object sender, ElapsedEventArgs e)
        {
            var newInputDateTime = _inputSources.GetLastInputTime();

            if (newInputDateTime.Subtract(_lastInputDateTime).TotalMilliseconds > 1000)
            {
                if (_lastInputDateTime.AddMilliseconds(_maxElapsedTimeMilliseconds) < newInputDateTime)
                {
                    _lastInputUpdateDateTime = DateTime.Now;
                    InputStarted?.Invoke(this, newInputDateTime);
                }
                else if (_lastInputUpdateDateTime.AddMilliseconds(_inputUpdateFrequencyMilliseconds) < DateTime.Now)
                {
                    _lastInputUpdateDateTime = DateTime.Now;
                    InputUpdated?.Invoke(this, newInputDateTime);
                }

                _lastInputDateTime = newInputDateTime;
            }
        }

        private void UpdateApplicationState(object sender, ElapsedEventArgs e)
        {
            var newApplicationName = _windowHelper.GetActiveWindowApplication();
            if (newApplicationName != null &&
                !_lastApplicationName.Equals(newApplicationName, StringComparison.OrdinalIgnoreCase))
            {
                _lastApplicationName = newApplicationName;
                ApplicationChanged?.Invoke(this, _lastApplicationName);
            }
        }
    }
}