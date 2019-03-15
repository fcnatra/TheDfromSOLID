using System.Diagnostics;
using TheDfromSOLID.Interfaces;

namespace TheDfromSOLID.Services
{
    internal class DumpSystemTraceListener : TraceListener
    {
        public IDumpSystem DumpSystem;
        public IConfiguration Configuration;

        public override void Write(string message)
        {
            WriteLine(message);
        }

        public override void WriteLine(string message)
        {
            DumpSystem.DumpElementName = Configuration.TemporalFolder + "\\trace.log";
            DumpSystem.DumpContent(message);
        }
    }
}
