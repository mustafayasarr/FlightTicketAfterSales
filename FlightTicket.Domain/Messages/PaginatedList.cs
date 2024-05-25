using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicket.Domain.Messages
{
    public class PaginatedList<T>
    {
        public IReadOnlyCollection<T> PageContents { get; }

        public int PageNumber { get; }

        public int TotalPages { get; }

        public int TotalCount { get; }

        public int PageSize { get; }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;

        public PaginatedList(IReadOnlyCollection<T> pageContents, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((double)count / (double)pageSize);
            TotalCount = count;
            PageContents = pageContents;
        }
    }
}
