using CompanyStatistics.UI.Menus;
using CompanyStatistics.UI.Menus.Abstraction;

namespace CompanyStatistics.UI
{
    public class Engine : IEngine
    {
        private IShowMenu _showMenu;
        public Engine()
        {
            _showMenu = new ShowMenu();
        }

        public async Task StartAsync()
        {
            _showMenu.MainMenu();

            var input = Console.ReadLine().ToLower();
            Console.WriteLine();

            while (input != "stop")
            {
                switch (input)
                {
                    case "1":
                        await _showMenu.AuthenticateAsync();
                        break;
                    case "2":
                        await _showMenu.ReadDataAsync();
                        break;
                    case "3":
                        await _showMenu.CompanyCrudAsync();
                        break;
                    case "4":
                        await _showMenu.StatisticsAsync();
                        break;
                    default:
                        break;
                }

                _showMenu.MainMenu();
                input = Console.ReadLine().ToLower();
            }
        }
    }
}
