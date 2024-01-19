namespace CompanyStatistics.Domain.DTOs.CompanyIndustry
{
    public class CompanyIndustryResponseDto
    {
        public string CompanyId { get; set; }
        public string IndustryName { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
