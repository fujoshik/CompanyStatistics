namespace CompanyStatistics.Domain.DTOs.Company
{
    public class CompanyUpdateDto
    {
        public string Name { get; set; }
        public string Website { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int Founded { get; set; }
        public int NumberOfEmployees { get; set; }
    }
}
