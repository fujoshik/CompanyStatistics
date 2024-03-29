﻿namespace CompanyStatistics.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base("The item was not found") { }

        public NotFoundException(string message)
            : base(message) { }

        public NotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}
