namespace TheDfromSOLID.Interfaces
{
    public interface IDumpSystem
    {
        string DumpElementName { get; set; }
        void DumpContent(string content);
    }
}