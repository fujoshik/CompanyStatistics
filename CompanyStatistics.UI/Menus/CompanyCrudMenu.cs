using CompanyStatistics.UI.Factories;
using CompanyStatistics.UI.Factories.Abstraction;
using CompanyStatistics.UI.HttpClients;
using CompanyStatistics.UI.HttpClients.Abstraction;
using CompanyStatistics.UI.Menus.Abstraction;

namespace CompanyStatistics.UI.Menus
{
    public class CompanyCrudMenu : BaseMenu, ICompanyCrudMenu
    {
        private readonly ICompanyClient _companyClient;
        private readonly ICompanyFactory _companyFactory;

        public CompanyCrudMenu()
        {
            _companyClient = new CompanyClient();
            _companyFactory = new CompanyFactory();
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
            Console.WriteLine();

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
            var result = await _companyClient.ReadDataAsync();
            Console.WriteLine(result);
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

            int founded = 0, numOfEmployees = 0;

            try
            {
                Console.Write("Founded: ");
                founded = int.Parse(Console.ReadLine());

                Console.Write("Number of employees: ");
                numOfEmployees = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.Write("Industry names: ");
            var industries = Console.ReadLine();

            var company = _companyFactory.CreateCompanyCreateDto(name, website, description, country, 
                founded, numOfEmployees, industries);

            Console.WriteLine();
            var result = await _companyClient.CreateAsync(company, token);

            Console.WriteLine(result);
            Console.WriteLine();
        }

        private async Task UpdateAsync()
        {
            var token  = GetToken();
            Console.WriteLine();

            Console.Write("Id: ");
            var id = Console.ReadLine();

            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("Country: ");
            var country = Console.ReadLine();

            Console.Write("Website: ");
            var website = Console.ReadLine();

            Console.Write("Description: ");
            var description = Console.ReadLine();

            int founded = 0, numOfEmployees = 0;

            try
            {
                Console.Write("Founded: ");
                founded = int.Parse(Console.ReadLine());

                Console.Write("Number of employees: ");
                numOfEmployees = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.Write("Industry names: ");
            var industries = Console.ReadLine();

            var company = _companyFactory.CreateCompanyCreateDto(name, website, description, country,
                founded, numOfEmployees, industries);

            Console.WriteLine();
            var result = await _companyClient.UpdateCompanyAsync(id, company, token);

            Console.WriteLine(result);
            Console.WriteLine();
        }

        private async Task GetByIdAsync()
        {
            Console.Write("Id: ");
            var id = Console.ReadLine();

            Console.WriteLine();
            var result = await _companyClient.GetCompanyAsync(id);

            Console.WriteLine(result);
            Console.WriteLine();
        }

        private async Task GetPageAsync()
        {
            int pageNum = 0, pageSize = 0;

            try
            {
                Console.Write("Page number: ");
                pageNum = int.Parse(Console.ReadLine());

                Console.Write("Page size: ");
                pageSize = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }     

            Console.WriteLine();
            var result = await _companyClient.GetPageAsync(pageNum, pageSize);

            Console.WriteLine(result);
            Console.WriteLine();
        }

        private async Task DeleteAsync()
        {
            var token = GetToken();
            Console.WriteLine();

            Console.Write("Id: ");
            var id = Console.ReadLine();

            Console.WriteLine();
            var result = await _companyClient.DeleteAsync(id, token);

            Console.WriteLine(result);
            Console.WriteLine();
        }
    }
}
