using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWatch.Console
{
    public class InputStateManager
    {
        private DateTime _lastInputDateTime = DateTime.MinValue;
        public event EventHandler<EventArgs> StateUpdated;
        public int MaxElapsedTime { get; set; }

    }
}
