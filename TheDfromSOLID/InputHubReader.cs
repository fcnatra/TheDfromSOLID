﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Timers;
using TheDfromSOLID.Interfaces;

namespace TheDfromSOLID
{
    public class InputHubReader : IDisposable
    {
        private readonly string _traceCategory;
        private Timer timer = new Timer();

        public bool IsListening { get { return timer.Enabled; } }
        public IConfiguration Configuration { get; set; }
        public IHub Hub { get; set; }
        public IDumpSystem DumpSystem{get; set;}

        public InputHubReader()
        {
            _traceCategory = this.GetType().Name;
        }

        public void StartListening()
        {
            Trace.WriteLine("Start listening", _traceCategory);

            CheckIAmNotAlreadyListening();

            timer = new Timer { Interval = Configuration.ReadingIntervalInMs };
            timer.Elapsed += Timer_Elapsed;

            DumpHubContent();
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DumpHubContent();
        }

        private void DumpHubContent()
        {
            Trace.WriteLine($"Reading from Hub {Hub.GetType().Name}", _traceCategory);

            IEnumerable<string> data = Hub.ReadFromHub();
            string dataProcessed = ConvertToDumpContent(data);
            DumpData(dataProcessed);
        }

        private void DumpData(string dumpContentProcessed)
        {
            var dumpName = $"{Configuration.TemporalFolder}\\{DateTime.Now.Minute}.dump";
            DumpSystem.DumpElementName = dumpName;
            DumpSystem.DumpContent(dumpContentProcessed);
        }

        private string ConvertToDumpContent(IEnumerable<string> info)
        {
            var builder = new StringBuilder();
            foreach (var item in info)
                builder.Append($"{item}{Environment.NewLine}");

            return builder.ToString();
        }

        private void CheckIAmNotAlreadyListening()
        {
            if (timer.Enabled) throw new InvalidOperationException("This instance is already listening.");
        }

        public void StopListening()
        {
            if (!timer.Enabled) return;

            Trace.WriteLine("Stop listening", _traceCategory);
            timer.Stop();
            timer.Dispose();
        }

        public void Dispose()
        {
            StopListening();
            Trace.WriteLine("Disposing InputHub Reader", _traceCategory);
        }
    }
}
