using System;
using System.Diagnostics;
using TheDfromSOLID.Interfaces;

namespace TheDfromSOLID
{
    class Program
    {
        private static IConfiguration configuration;

        static void Main(string[] args)
        {
            ConfigureTraceSystem();

            RunSystem();

            Console.WriteLine("Program terminated.");
        }

        private static void RunSystem()
        {
            configuration = InitializeConfiguration();

            InputHubReader inputHub = StartListeningOnInputHub();
            
            PressAnyKeyToExit();

            inputHub.Dispose();
        }

        private static void ConfigureTraceSystem()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
        }

        private static InputHubReader StartListeningOnInputHub()
        {
            var inputHub = new InputHubReader
            {
                Configuration = configuration,
                Hub = new Services.MachineProcessesHub()
            };
            inputHub.StartListening();
            return inputHub;
        }

        private static IConfiguration InitializeConfiguration()
        {
            IConfiguration configuration = new Configuration();
            configuration.UpdateInformation();
            return configuration;
        }

        private static void PressAnyKeyToExit()
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
