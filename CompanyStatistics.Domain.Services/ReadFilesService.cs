using CompanyStatistics.Domain.Abstraction.Services;

namespace CompanyStatistics.Domain.Services
{
    public class ReadFilesService : IReadFilesService
    {
        private readonly IReadDataService _readDataService;
        private readonly IFileService _fileService;

        public ReadFilesService(IReadDataService readDataService,
                                IFileService fileService)
        {
            _readDataService = readDataService;
            _fileService = fileService;
        }

        public async Task ReadFilesAsync()
        {
            var files = _fileService.GetFilesFromMainDirectory();

            foreach (var file in files)
            {
                if (file.EndsWith(".csv"))
                {
                    await _readDataService.ReadCsvFileAsync(file);
                }

                _fileService.MoveFile(file);
            }
        }
    }
}
