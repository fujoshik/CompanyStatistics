namespace CompanyStatistics.UI.Menus.Abstraction
{
    public interface IShowMenu
    {
        void MainMenu();
        Task AuthenticateAsync();
        Task ReadDataAsync();
        Task CompanyCrudAsync();
        Task StatisticsAsync();
        Task UserCrudAsync();
    }
}
