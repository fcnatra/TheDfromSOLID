using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TheDfromSOLID.Interfaces;

namespace TheDfromSOLID.Services
{
    internal class MachineProcessesHub : IHub
    {
        public IEnumerable<string> ReadFromHub()
        {
            var processes = Process.GetProcesses();
            return processes.Select(process => process.ProcessName);
        }
    }
}
