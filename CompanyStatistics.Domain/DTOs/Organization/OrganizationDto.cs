using CsvHelper.Configuration.Attributes;

namespace CompanyStatistics.Domain.DTOs.Organization
{
    public class OrganizationDto
    {
        [Name("Index")]
        public int Index { get; set; }

        [Name("Organization Id")]
        public string OrganizationId { get; set; }

        [Name("Name")]
        public string Name { get; set; }

        [Name("Website")]
        public string Website { get; set; }

        [Name("Country")]
        public string Country { get; set; }

        [Name("Description")]
        public string Description { get; set; }

        [Name("Founded")]
        public string Founded { get; set; }

        [Name("Industry")]
        public string Industry { get; set; }

        [Name("Number of employees")]
        public int NumberOfEmployees { get; set; }
    }
}
