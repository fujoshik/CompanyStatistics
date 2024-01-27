using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.DTOs.Industry;
using CompanyStatistics.Domain.Pagination;
using CompanyStatistics.UI.HttpClients;
using CompanyStatistics.UI.HttpClients.Abstraction;
using CompanyStatistics.UI.Menus.Abstraction;

namespace CompanyStatistics.UI.Menus
{
    public class CompanyCrudMenu : BaseMenu, ICompanyCrudMenu
    {
        private readonly ICompanyClient _companyClient;

        public CompanyCrudMenu()
        {
            _companyClient = new CompanyClient();
        }

        public async Task CompanyCrudAsync()
        {
            Console.WriteLine("Company CRUD:");
            Console.WriteLine("1. Create");
            Console.WriteLine("2. Get by id");
            Console.WriteLine("3. Get page");
            Console.WriteLine("4. Update");
            Console.WriteLine("5. Delete");
            Console.WriteLine();

            string choice = Console.ReadLine().ToLower();

            switch (choice)
            {
                case "1":
                    await CreateAsync();
                    break;
                case "2":
                    await GetByIdAsync();
                    break;
                case "3":
                    await GetPageAsync();
                    break;
                case "4":
                    await UpdateAsync();
                    break;
                case "5":
                    await DeleteAsync();
                    break;
                default:
                    break;
            }
        }

        public async Task ReadDataAsync()
        {
            await _companyClient.ReadDataAsync();
        }

        private async Task CreateAsync()
        {
            var token = GetToken();
            Console.WriteLine();

            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("Country: ");
            var country = Console.ReadLine();

            Console.Write("Website: ");
            var website = Console.ReadLine();

            Console.Write("Description: ");
            var description = Console.ReadLine();

            Console.Write("Founded: ");
            var founded = int.Parse(Console.ReadLine());

            Console.Write("Number of employees: ");
            var numOfEmployees = int.Parse(Console.ReadLine());

            Console.Write("Industry name: ");
            var industry = Console.ReadLine();

            var result = await _companyClient.CreateAsync(new CompanyCreateDto()
            {
                Name = name,
                Country = country,
                Description = description,
                Website = website,
                Founded = founded,
                NumberOfEmployees = numOfEmployees,
                Industries = new List<IndustryRequestDto>() { new IndustryRequestDto() { Name = industry } }
            }, token);

            Console.WriteLine(result);
            Console.WriteLine();
        }

        private async Task UpdateAsync()
        {
            var token  = GetToken();
            Console.WriteLine();

            Console.Write("Id: ");
            var id = Console.ReadLine();

            Console.Write("Company index: ");
            var companyIndex = int.Parse(Console.ReadLine());

            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("Country: ");
            var country = Console.ReadLine();

            Console.Write("Website: ");
            var website = Console.ReadLine();

            Console.Write("Description: ");
            var description = Console.ReadLine();

            Console.Write("Founded: ");
            var founded = int.Parse(Console.ReadLine());

            Console.Write("Number of employees: ");
            var numOfEmployees = int.Parse(Console.ReadLine());

            Console.Write("Industry name: ");
            var industry = Console.ReadLine();

            var result = await _companyClient.UpdateCompanyAsync(id, new CompanyWithoutIdDto()
            {
                CompanyIndex = companyIndex,
                Name = name,
                Country = country,
                Description = description,
                Website = website,
                Founded = founded,
                NumberOfEmployees = numOfEmployees,
                Industries = new List<IndustryRequestDto>() { new IndustryRequestDto() { Name = industry } },
                IsDeleted = 0
            }, token);

            Console.WriteLine(result);
            Console.WriteLine();
        }

        private async Task GetByIdAsync()
        {
            var token = GetToken();
            Console.WriteLine();

            Console.Write("Id: ");
            var id = Console.ReadLine();

            var result = await _companyClient.GetCompanyAsync(id, token);

            Console.WriteLine(result);
            Console.WriteLine();
        }

        private async Task GetPageAsync()
        {
            var token = GetToken();
            Console.WriteLine();

            Console.Write("Page number: ");
            var pageNum = int.Parse(Console.ReadLine());

            Console.Write("Page size: ");
            var pageSize = int.Parse(Console.ReadLine());

            var result = await _companyClient.GetPageAsync(pageNum, pageSize, token);

            Console.WriteLine(result);
            Console.WriteLine();
        }

        private async Task DeleteAsync()
        {
            var token = GetToken();
            Console.WriteLine();

            Console.Write("Id: ");
            var id = Console.ReadLine();

            var result = await _companyClient.DeleteAsync(id, token);

            Console.WriteLine(result);
            Console.WriteLine();
        }
    }
}
