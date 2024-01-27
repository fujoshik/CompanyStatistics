using CompanyStatistics.UI.HttpClients;
using CompanyStatistics.UI.HttpClients.Abstraction;
using CompanyStatistics.UI.Menus.Abstraction;

namespace CompanyStatistics.UI.Menus
{
    public class StatisticsMenu : BaseMenu, IStatisticsMenu
    {
        private readonly IStatisticsClient _statisticsClient;

        public StatisticsMenu()
        {
            _statisticsClient = new StatisticsClient();
        }

        public async Task StatisticsAsync()
        {
            Console.WriteLine("Company statistics:");
            Console.WriteLine("1. Count employees by industry");
            Console.WriteLine("2. Get top n companies by employee count");
            Console.WriteLine("3. Group companies by country and industry");
            Console.WriteLine("4. Generate pdf by company name");
            Console.WriteLine();

            string choice = Console.ReadLine().ToLower();

            switch (choice)
            {
                case "1":
                    await CountEmployeesByIndustryAsync();
                    break;
                case "2":
                    await GetTopNCompaniesByEmployeeCountAsync();
                    break;
                case "3":
                    await GroupCompaniesByCountryAndIndustryAsync();
                    break;
                case "4":
                    await GeneratePdfAsync();
                    break;
                default:
                    break;
            }
        }

        private async Task CountEmployeesByIndustryAsync()
        {
            Console.Write("Industry: ");
            var industry = Console.ReadLine();

            var result = await _statisticsClient.CountEmployeesByIndustryAsync(industry);

            Console.WriteLine(result);
        }

        private async Task GetTopNCompaniesByEmployeeCountAsync()
        {
            Console.Write("N: ");
            var n = int.Parse(Console.ReadLine());

            var result = await _statisticsClient.GetTopNCompaniesByEmployeeCountAsync(n);

            Console.WriteLine(result.ToString());
        }

        private async Task GroupCompaniesByCountryAndIndustryAsync()
        {
            Console.Write("Country: ");
            var country = Console.ReadLine();

            Console.Write("Industry: ");
            var industry = Console.ReadLine();

            var result = await _statisticsClient.GroupCompaniesByCountryAndIndustryAsync(country, industry);

            Console.WriteLine(string.Join("; ", result.ToString()));
        }

        private async Task GeneratePdfAsync()
        {
            var token = GetToken();
            Console.WriteLine();

            Console.Write("Company name: ");
            var companyName = Console.ReadLine();

            var result = await _statisticsClient.GeneratePdfAsync(companyName, token);

            Console.WriteLine(result);
        }
    }
}
