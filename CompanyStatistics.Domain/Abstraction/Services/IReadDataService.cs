namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface IReadDataService
    {
        Task ReadCsvFileAsync(string fileName);
    }
}
