namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface IFileService
    {
        void MoveFile(string file);
        string[] GetFilesFromMainDirectory();
        void WriteAJsonFileWithStatistics(object statistics, string fileName);
    }
}
