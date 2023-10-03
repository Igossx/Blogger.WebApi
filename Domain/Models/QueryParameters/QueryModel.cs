using Blogger.Application.Pagination.Enums;

namespace Blogger.Application.Pagination.QueryParameters
{
    public class QueryModel : IQueryModel
    {
        private const int MAX_PAGE_SIZE = 50;
        private int pageSize = 10;

        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : value;
            }
        }

        public SortDirection SortDirection { get; set; }

    }
}
