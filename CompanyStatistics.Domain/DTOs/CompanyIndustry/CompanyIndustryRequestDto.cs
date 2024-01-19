namespace CompanyStatistics.Domain.DTOs.CompanyIndustry
{
    public class CompanyIndustryRequestDto
    {
        public string CompanyId { get; set; }
        public string IndustryName { get; set; }
        public int IsDeleted { get; set; } = 0;
    }
}
