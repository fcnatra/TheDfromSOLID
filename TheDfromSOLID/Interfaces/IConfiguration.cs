namespace TheDfromSOLID.Interfaces
{
    public interface IConfiguration
    {
        string TemporalFolder { get; }
        double ReadingIntervalInMs { get; }
        void UpdateInformation();
    }
}
