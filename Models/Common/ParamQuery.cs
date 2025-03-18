using Gridify;

namespace BackEndApi.Models.Common;

public class ParamQuery : IGridifyQuery
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? Filter { get; set; }
    public string? OrderBy { get; set; }

    public ParamQuery()
    {
    }

    public ParamQuery(int page, int pageSize, string filter, string? orderBy = null)
    {
        Page = page;
        PageSize = pageSize;
        Filter = filter;
        OrderBy = orderBy;
    }
}