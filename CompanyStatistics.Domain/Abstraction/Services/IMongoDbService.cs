using CompanyStatistics.Domain.DTOs.File;

namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface IMongoDbService
    {
        Task CreateFileAsync(FileDto file);
        Task<FileDto> GetFileByNameAsync(string name);
        Task UpdateFileIndexAsync(string name, int index);
    }
}
