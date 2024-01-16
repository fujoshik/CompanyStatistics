namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface IPdfService
    {
        Task<string> GetPdfForByCompanyName(string companyName);
    }
}
