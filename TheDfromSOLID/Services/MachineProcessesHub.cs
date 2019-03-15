using System.Collections.Generic;
using System.Diagnostics;
using TheDfromSOLID.Interfaces;

namespace TheDfromSOLID.Services
{
    internal class MachineProcessesHub : IHub
    {
        IEnumerable<string> IHub.ReadFromHub()
        {
            var processes = Process.GetProcesses();
            var processesNames = new List<string>();
            foreach (var process in processes)
                processesNames.Add(process.ProcessName);

            return processesNames;
        }
    }
}
