﻿using CompanyStatistics.Domain.DTOs.Company;

namespace CompanyStatistics.Domain.Abstraction.Repositories
{
    public interface ICompanyRepository : IBaseRepository
    {
        Task<HashSet<string>> GetAllCompanyIdsAsync();
        Task<int> CountEmployeesByIndustryAsync(string industry);
        Task BulkInsertAsync(List<CompanyRequestDto> companies);
        Task<CompanyResponseDto> GetCompanyByNameAsync(string name);
        Task<List<CompanyResponseDto>> GetCompaniesByDateAsync(DateTime date);
        Task<List<CompanyResponseDto>> GetTopNCompaniesByEmployeeCountAndDateAsync(int n, DateTime? date = null);
        Task<List<CompanyResponseDto>> GroupCompaniesByCountryAndIndustryAsync(string country, string industry);
    }
}
