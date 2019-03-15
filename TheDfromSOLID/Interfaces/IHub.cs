using System.Collections.Generic;

namespace TheDfromSOLID.Interfaces
{
    public interface IHub
    {
        IEnumerable<string> ReadFromHub();
    }
}