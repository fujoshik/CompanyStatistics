using CompanyStatistics.Domain.DTOs.Authentication;
using CompanyStatistics.UI.HttpClients;
using CompanyStatistics.UI.HttpClients.Abstraction;
using CompanyStatistics.UI.Menus.Abstraction;

namespace CompanyStatistics.UI.Menus
{
    public class AuthenticationMenu : IAuthenticationMenu
    {
        private readonly IAuthenticateClient _authenticateClient;

        public AuthenticationMenu()
        {
            _authenticateClient = new AuthenticateClient();
        }

        public async Task AuthenticationOptionsAsync()
        {
            Console.WriteLine("Authentication:");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine();

            string choice = Console.ReadLine().ToLower();

            switch (choice)
            {
                case "1":
                    await LoginAsync();
                    break;

                case "2":
                    await RegisterAsync();
                    break;
                default:
                    break;
            }
        }

        private async Task LoginAsync()
        {
            Console.Write("Email: ");
            var email = Console.ReadLine();

            Console.Write("Password: ");
            var password = Console.ReadLine();

            var result = await _authenticateClient.LoginAsync(new LoginDto() { Email = email, Password = password });
            Console.WriteLine();

            Console.WriteLine(result);
        }

        private async Task RegisterAsync()
        {
            Console.Write("First name: ");
            var firstName = Console.ReadLine();

            Console.Write("Last name: ");
            var lastName = Console.ReadLine();

            Console.Write("Age: ");
            var age = int.Parse(Console.ReadLine());

            Console.Write("Country: ");
            var country = Console.ReadLine();

            Console.Write("Email: ");
            var email = Console.ReadLine();

            Console.Write("Password: ");
            var password = Console.ReadLine();

            await _authenticateClient.RegisterAsync(new RegisterDto()
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Country = country,
                Email = email,
                Password = password
            });
            Console.WriteLine();
        }
    }
}
