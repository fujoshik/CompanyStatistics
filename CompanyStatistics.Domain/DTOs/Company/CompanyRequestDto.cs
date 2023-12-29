namespace CompanyStatistics.Domain.DTOs.Company
{
    public class CompanyRequestDto
    {
        public string Id { get; set; }
        public int CompanyIndex { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int Founded { get; set; }
        public string Industry { get; set; }
        public int NumberOfEmployees { get; set; }
    }
}
