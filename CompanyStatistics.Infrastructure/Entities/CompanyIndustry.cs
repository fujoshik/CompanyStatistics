namespace CompanyStatistics.Infrastructure.Entities
{
    public class CompanyIndustry : BaseEntity
    {
        public string CompanyId { get; set; }
        public string IndustryName { get; set; }
        public int IsDeleted { get; set; }
    }
}
