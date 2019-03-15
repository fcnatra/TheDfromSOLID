using System;
using System.Timers;

namespace TheDfromSOLID
{
    internal class InputHub : IDisposable
    {
        private Timer timer = new Timer();

        public bool IsListening { get { return timer.Enabled; } }

        public Interfaces.IConfiguration Configuration { get; set; }
        public Interfaces.IHub Hub { get; set; }

        public void StartListening()
        {
            CheckIAmNotAlreadyListening();

            timer = new Timer { Interval = Configuration.ReadingIntervalInMs };
            timer.Start();
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ReadFromHub();
        }

        private void CheckIAmNotAlreadyListening()
        {
            if (timer.Enabled) throw new InvalidOperationException("This instance is already listening.");
        }

        internal void StopListening()
        {
            timer.Stop();
            timer.Dispose();
        }

        public void Dispose()
        {
            StopListening();
        }
    }
}
