namespace CompanyStatistics.UI.Menus.Abstraction
{
    public interface ICompanyCrudMenu
    {
        Task ReadDataAsync();
        Task CompanyCrudAsync();
    }
}
