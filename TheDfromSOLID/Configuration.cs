using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDfromSOLID
{
    internal class Configuration : Interfaces.IConfiguration
    {
        public string TemporalFolder { get; private set; }

        public double ReadingIntervalInMs { get; private set; }

        public void UpdateInformation()
        {
            TemporalFolder = Environment.GetEnvironmentVariable("TEMP");
            ReadingIntervalInMs = 1000;
        }
    }


}
