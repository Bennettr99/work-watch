using System;
using System.Timers;
using WorkWatch.Console.Helpers;

namespace WorkWatch.Console
{
    public class InputStateManager
    {
        private const int UpdateFrequencyMilliseconds = 100;
        private readonly AllInputSources _inputSources;
        private DateTime _lastInputDateTime = DateTime.MinValue;
        private string _lastApplicationName = string.Empty;
        private readonly WindowHelper _windowHelper;

        public InputStateManager(int maxElapsedTimeMilliseconds)
        {
            _maxElapsedTimeMilliseconds = maxElapsedTimeMilliseconds;
            _inputSources = new AllInputSources();
            _windowHelper = new WindowHelper();
            var stateChangeTimer = new Timer(UpdateFrequencyMilliseconds);
            stateChangeTimer.Elapsed += UpdateInputState;
            stateChangeTimer.Elapsed += UpdateApplicationState;
            stateChangeTimer.AutoReset = true;
            stateChangeTimer.Enabled = true;
        }

        private readonly int _maxElapsedTimeMilliseconds;
        public event EventHandler<DateTime> StateUpdated;
        public event EventHandler<string> ApplicationChanged;

        private void UpdateInputState(object sender, ElapsedEventArgs e)
        {
            var newInputDateTime = _inputSources.GetLastInputTime();
            if (_lastInputDateTime.AddMilliseconds(_maxElapsedTimeMilliseconds) < newInputDateTime)
            {
                StateUpdated?.Invoke(this, _lastInputDateTime);
            }
            _lastInputDateTime = newInputDateTime;
        }

        private void UpdateApplicationState(object sender, ElapsedEventArgs e)
        {
            var newApplicationName = _windowHelper.GetActiveWindowApplication();
            if (newApplicationName != null && !_lastApplicationName.Equals(newApplicationName, StringComparison.OrdinalIgnoreCase))
            {
                _lastApplicationName = newApplicationName;
                ApplicationChanged?.Invoke(this, _lastApplicationName);
            }
        }
    }
}