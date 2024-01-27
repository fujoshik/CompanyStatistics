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
            Console.WriteLine();

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

            Console.WriteLine();
            var result = await _authenticateClient.LoginAsync(new LoginDto() { Email = email, Password = password });

            Console.WriteLine(result);
            Console.WriteLine();
        }

        private async Task RegisterAsync()
        {
            Console.Write("First name: ");
            var firstName = Console.ReadLine();

            Console.Write("Last name: ");
            var lastName = Console.ReadLine();

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

            Console.Write("Country: ");
            var country = Console.ReadLine();

            Console.Write("Email: ");
            var email = Console.ReadLine();

            Console.Write("Password: ");
            var password = Console.ReadLine();

            Console.WriteLine();

            var result = await _authenticateClient.RegisterAsync(new RegisterDto()
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Country = country,
                Email = email,
                Password = password
            });

            Console.WriteLine(result);
            Console.WriteLine();
        }
    }
}
