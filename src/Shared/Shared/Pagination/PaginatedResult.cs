namespace Shared.Pagination;

public class PaginatedResult<TData>(int pageIndex, int pageSize, long count, IEnumerable<TData> data)
    where TData : class
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
    public long Count { get; } = count;
    public IEnumerable<TData> Data { get; } = data;
}
