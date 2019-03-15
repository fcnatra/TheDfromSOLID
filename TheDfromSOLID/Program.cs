﻿using System;
using System.Diagnostics;
using TheDfromSOLID.Interfaces;
using TheDfromSOLID.Services;

namespace TheDfromSOLID
{
    class Program
    {
        private static IConfiguration configuration;

        static void Main(string[] args)
        {
            configuration = InitializeConfiguration();
            ConfigureTraceSystem();

            RunSystem();

            Console.WriteLine("Program terminated.");
        }

        private static void ConfigureTraceSystem()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            var dumpSystemTraceListener = new DumpSystemTraceListener
            {
                Configuration = configuration,
                DumpSystem = new Services.FileDumpSystem()
            };
            Trace.Listeners.Add(dumpSystemTraceListener);
        }

        private static void RunSystem()
        {
            InputHubReader inputHub = StartListeningOnInputHub();
            
            PressAnyKeyToExit();

            inputHub.Dispose();
        }

        private static InputHubReader StartListeningOnInputHub()
        {
            var inputHub = new InputHubReader
            {
                Configuration = configuration,
                Hub = new Services.MachineProcessesHub(),
                DumpSystem = new Services.FileDumpSystem()
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
