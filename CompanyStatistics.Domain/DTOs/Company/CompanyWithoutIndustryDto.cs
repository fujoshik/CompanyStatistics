namespace CompanyStatistics.Domain.DTOs.Company
{
    public class CompanyWithoutIndustryDto : BaseResponseDto
    {
        public int CompanyIndex { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int Founded { get; set; }
        public int NumberOfEmployees { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime DateRead { get; set; }
    }
}
