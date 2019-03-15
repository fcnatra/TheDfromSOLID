using System;
using System.IO;
using TheDfromSOLID.Interfaces;

namespace TheDfromSOLID.Services
{
    internal class FileDumpSystem : IDumpSystem
    {
        public string DumpElementName { get; set; }

        public void DumpContent(string content)
        {
            File.AppendAllLines(DumpElementName, new string[] { DateTime.Now.ToString(), content, "" });
        }
    }
}
