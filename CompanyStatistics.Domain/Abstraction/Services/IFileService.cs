namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface IFileService
    {
        void MoveFile(string file);
        string[] GetFilesFromMainDirectory();
        MemoryStream ReturnFileAsStream(string fileName);
        void WriteAJsonFileWithStatistics(object statistics, string fileName);
    }
}
