using CompanyStatistics.UI.Factories;
using CompanyStatistics.UI.Factories.Abstraction;
using CompanyStatistics.UI.HttpClients;
using CompanyStatistics.UI.HttpClients.Abstraction;
using CompanyStatistics.UI.Menus.Abstraction;

namespace CompanyStatistics.UI.Menus
{
    public class UserCrudMenu : BaseMenu, IUserCrudMenu
    {
        private readonly IUserClient _userClient;
        private readonly IUserFactory _userFactory;

        public UserCrudMenu()
        {
            _userClient = new UserClient();
            _userFactory = new UserFactory();
        }

        public async Task UserCrudAsync()
        {
            Console.WriteLine("User CRUD:");
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

        private async Task CreateAsync()
        {
            var token = GetToken();
            Console.WriteLine();

            Console.Write("First name: ");
            var firstName = Console.ReadLine();

            Console.Write("Last name: ");
            var lastName = Console.ReadLine();

            Console.Write("Country: ");
            var country = Console.ReadLine();

            int age = 0;

            try
            {
                Console.Write("Age: ");
                age = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.Write("Account Id: ");
            var accountId = Console.ReadLine();

            var user = _userFactory.CreateUserRequestDto(firstName, lastName, country,
                age, accountId);

            Console.WriteLine();
            var result = await _userClient.CreateAsync(user, token);

            Console.WriteLine(result);
            Console.WriteLine();
        }

        private async Task UpdateAsync()
        {
            var token = GetToken();
            Console.WriteLine();

            Console.Write("Id: ");
            var id = Console.ReadLine();

            Console.Write("First name: ");
            var firstName = Console.ReadLine();

            Console.Write("Last name: ");
            var lastName = Console.ReadLine();

            Console.Write("Country: ");
            var country = Console.ReadLine();

            int age = 0;

            try
            {
                Console.Write("Age: ");
                age = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.Write("Account Id: ");
            var accountId = Console.ReadLine();

            var user = _userFactory.CreateUserRequestDto(firstName, lastName, country,
                age, accountId);

            Console.WriteLine();
            var result = await _userClient.UpdateAsync(id, user, token);

            Console.WriteLine(result);
            Console.WriteLine();
        }

        private async Task GetByIdAsync()
        {
            Console.Write("Id: ");
            var id = Console.ReadLine();

            Console.WriteLine();
            var result = await _userClient.GetUserAsync(id);

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
            var result = await _userClient.GetPageAsync(pageNum, pageSize);

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
            var result = await _userClient.DeleteAsync(id, token);

            Console.WriteLine(result);
            Console.WriteLine();
        }
    }
}
