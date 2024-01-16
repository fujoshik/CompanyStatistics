using CompanyStatistics.Domain.Exceptions;
using CompanyStatistics.Domain.Pagination;
using System.ComponentModel;
using System.Data;

namespace CompanyStatistics.Infrastructure.Extensions
{
    public static class ListExtensions
    {
        public static PaginatedResult<T> Paginate<T>(this IList<T> collection, int pageNumber, int pageSize)
        {
            if (pageNumber < 0 || pageSize < 0)
            {
                throw new ValidationException("Page and size should not be negative values!");
            }

            var totalElements = collection.Count();
            var skip = pageNumber * pageSize;

            if (totalElements == 0 || totalElements < skip)
            {
                return PaginatedResult<T>.EmptyResult(pageNumber);
            }

            var result = collection.Skip(skip).Take(pageSize).ToList();

            return new PaginatedResult<T>(result, pageNumber, pageSize);
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}
