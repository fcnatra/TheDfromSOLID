namespace TheDfromSOLID.Interfaces
{
    internal interface IDumpSystem
    {
        string Name { get; set; }
        void DumpContent(string content);
    }
}