using System;
using TheDfromSOLID.Interfaces;

namespace TheDfromSOLID
{
    class Program
    {
        private static IConfiguration configuration;

        static void Main(string[] args)
        {
            configuration = InitializeConfiguration();
            InputHub inputHub = StartListeningOnInputHub();

            PressAnyKeyToExit();

            inputHub.StopListening();

            Console.WriteLine("Program terminated.");
        }

        private static InputHub StartListeningOnInputHub()
        {
            var inputHub = new InputHub { Configuration = configuration };
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
