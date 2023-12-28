using CsvHelper.Configuration;

namespace CompanyStatistics.Domain.DTOs.Organization
{
    public class OrganizationMap : ClassMap<OrganizationDto>
    {
        public OrganizationMap()
        {
            Map(m => m.Index).Name("Index");
            Map(m => m.OrganizationId).Name("Organization Id");
            Map(m => m.Name).Name("Name");
            Map(m => m.Website).Name("Website");
            Map(m => m.Country).Name("Country");
            Map(m => m.Description).Name("Description");
            Map(m => m.Founded).Name("Founded");
            Map(m => m.Industry).Name("Industry");
            Map(m => m.NumberOfEmployees).Name("Number of employees");
        }
    }
}
