using Blogger.Application.Pagination.QueryParameters;

namespace Blogger.Application.Extensions
{
    public static class PaginationExtension
    {
        public static IEnumerable<T> GetItemsForPage<T>(this IEnumerable<T> query, IQueryModel queryModel)
        {
            return query
                .Skip((queryModel.PageNumber - 1) * queryModel.PageSize)
                .Take(queryModel.PageSize);
        }
    }
}
