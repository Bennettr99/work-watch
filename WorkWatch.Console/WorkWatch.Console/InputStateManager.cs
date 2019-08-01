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

        public InputStateManager(int maxElapsedTimeMilliseconds)
        {
            _maxElapsedTimeMilliseconds = maxElapsedTimeMilliseconds;
            _inputSources = new AllInputSources();
            var threadTimer = new Timer(UpdateFrequencyMilliseconds);
            threadTimer.Elapsed += UpdateState;
            threadTimer.AutoReset = true;
            threadTimer.Enabled = true;
        }

        private readonly int _maxElapsedTimeMilliseconds;
        public event EventHandler<DateTime> StateUpdated;

        private void UpdateState(object sender, ElapsedEventArgs e)
        {
            var previousInputDateTime = _lastInputDateTime;
            _lastInputDateTime = _inputSources.GetLastInputTime();

            if (previousInputDateTime.AddMilliseconds(_maxElapsedTimeMilliseconds) < _lastInputDateTime)
            {
                StateUpdated?.Invoke(this, _lastInputDateTime);
            }
        }
    }
}