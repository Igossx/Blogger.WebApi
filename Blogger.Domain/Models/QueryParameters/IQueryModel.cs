using Blogger.Application.Pagination.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Application.Pagination.QueryParameters
{
    public interface IQueryModel
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        string? SearchPhrase { get; set; }
        SortDirection SortDirection { get; set; }
    }
}
