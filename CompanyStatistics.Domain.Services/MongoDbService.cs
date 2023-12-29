using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.File;
using CompanyStatistics.Domain.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CompanyStatistics.Domain.Services
{
    public class MongoDbService : IMongoDbService
    {
        private readonly IMongoCollection<FileDto> _fileCollection;

        public MongoDbService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionURI);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _fileCollection = database.GetCollection<FileDto>(settings.Value.CollectionName);
        }

        public async Task CreateFileAsync(FileDto file)
        {
            await _fileCollection.InsertOneAsync(file);
        }

        public async Task<FileDto> GetFileByNameAsync(string name)
        {
            var files = await _fileCollection.FindAsync(x => x.FileName == name);

            return files.FirstOrDefault();
        }

        public async Task UpdateFileIndexAsync(string name, int index)
        {
            var update = Builders<FileDto>.Update.Set(file => file.Index, index);

            await _fileCollection.UpdateOneAsync(file => file.FileName == name, update);
        }
    }
}
