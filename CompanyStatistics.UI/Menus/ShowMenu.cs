﻿using CompanyStatistics.UI.Menus.Abstraction;

namespace CompanyStatistics.UI.Menus
{
    public class ShowMenu : IShowMenu
    {
        private readonly IAuthenticationMenu _authenticationMenu;
        private readonly ICompanyCrudMenu _companyCrudMenu;
        private readonly IStatisticsMenu _statisticsMenu;
        private readonly IUserCrudMenu _userCrudMenu;

        public ShowMenu()
        {
            _authenticationMenu = new AuthenticationMenu();
            _companyCrudMenu = new CompanyCrudMenu();
            _statisticsMenu = new StatisticsMenu();
            _userCrudMenu = new UserCrudMenu();
        }

        public void MainMenu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Authentication");
            Console.WriteLine("2. Read data");
            Console.WriteLine("3. CRUD for companies");
            Console.WriteLine("4. Statistics for companies");
            Console.WriteLine("5. CRUD for users");

            Console.WriteLine("Please choose an option (write the number)");
            Console.WriteLine();
        }
        
        public async Task AuthenticateAsync()
        {
            await _authenticationMenu.AuthenticationOptionsAsync();
            Console.WriteLine();
        }

        public async Task ReadDataAsync()
        {
            await _companyCrudMenu.ReadDataAsync();
            Console.WriteLine();
        }

        public async Task CompanyCrudAsync()
        {
            await _companyCrudMenu.CompanyCrudAsync();
            Console.WriteLine();
        }

        public async Task StatisticsAsync()
        {
            await _statisticsMenu.StatisticsAsync();
            Console.WriteLine();
        }

        public async Task UserCrudAsync()
        {
            await _userCrudMenu.UserCrudAsync();
            Console.WriteLine();
        }
    }
}
