namespace TheDfromSOLID.Interfaces
{
    internal interface IDumpSystem
    {
        string DumpElementName { get; set; }
        void DumpContent(string content);
    }
}