namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface IReadDataService
    {
        Task ReadFilesAsync();
        Task ReadCsvFileAsync(string fileName);
    }
}
