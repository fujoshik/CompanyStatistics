﻿namespace CompanyStatistics.Domain.Abstraction.Repositories
{
    public interface IUnitOfWork
    {
        ICompanyRepository CompanyRepository { get; }
    }
}