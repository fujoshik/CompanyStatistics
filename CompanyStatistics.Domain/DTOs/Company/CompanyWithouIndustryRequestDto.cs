namespace CompanyStatistics.Domain.DTOs.Company
{
    public class CompanyWithouIndustryRequestDto : BaseResponseDto
    {
        public int CompanyIndex { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int Founded { get; set; }
        public int NumberOfEmployees { get; set; }
        public int IsDeleted { get; set; } = 0;
    }
}
