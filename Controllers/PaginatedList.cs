using Gate_Pass_management.Models;

namespace Gate_Pass_management.Controllers
{
    internal class PaginatedList<T>
    {
        internal static Task<string?> CreateAsync(IQueryable<VisitorsEntry> visitorsEntries, int v, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}