﻿using CompanyStatistics.Domain.DTOs.Industry;

namespace CompanyStatistics.Domain.DTOs.Company
{
    public class CompanyResponseDto : BaseResponseDto
    {
        public int CompanyIndex { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int Founded { get; set; }
        public List<IndustryResponseDto> Industries { get; set; }
        public int NumberOfEmployees { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime DateRead { get; set; }
    }
}
