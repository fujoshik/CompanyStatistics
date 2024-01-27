namespace CompanyStatistics.UI.Menus
{
    public class BaseMenu
    {
        protected string GetToken()
        {
            Console.Write("Please input your token: ");
            return Console.ReadLine();
        }
    }
}
