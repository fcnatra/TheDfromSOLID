namespace TheDfromSOLID.Interfaces
{
    internal interface IConfiguration
    {
        string TemporalFolder { get; }
        double ReadingIntervalInMs { get; }
        void UpdateInformation();
    }
}
