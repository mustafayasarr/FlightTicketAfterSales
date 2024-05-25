using FlightTicket.Domain.Messages;
using Microsoft.EntityFrameworkCore;

namespace FlightTicket.Domain.Extension;

public static class PaginatedListExtension
{
    public static async Task<PaginatedList<T>> ToPaginationListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return new PaginatedList<T>(count: await source.CountAsync(cancellationToken), pageContents: await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken), pageNumber: pageNumber, pageSize: pageSize);
    }
}
