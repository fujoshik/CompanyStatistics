using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.Paths;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CompanyStatistics.Domain.Services
{
    public class FileService : IFileService
    {
        private readonly string _mainFileDirectory;
        private readonly string _readFilesDirectory;
        private readonly string _statisticsDirectory;

        public FileService(IOptions<FilesFolderPath> fileDirectoryOptions)
        {
            _mainFileDirectory = fileDirectoryOptions.Value.MainPath;
            _readFilesDirectory = fileDirectoryOptions.Value.ReadFilesPath;
            _statisticsDirectory = fileDirectoryOptions.Value.StatisticsPath;
        }

        public void MoveFile(string file)
        {
            if (File.Exists(file))
            {
                File.Move(file, _readFilesDirectory + Path.GetFileName(file));
            }
        }

        public string[] GetFilesFromMainDirectory()
        {
            return Directory.GetFiles(_mainFileDirectory);
        }

        public void WriteAJsonFileWithStatistics(object statistics, string fileName)
        {
            var jsonString = JsonConvert.SerializeObject(statistics);

            File.WriteAllText(_statisticsDirectory + fileName, jsonString);
        }
    }
}
