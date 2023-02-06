using NDS_ToDo.Domain.Contracts;
using NDS_ToDo.Domain.Entities;

namespace NDS_ToDo.Infra.Paged;

public class PagedResult<T> : IPagedResult<T> where T: Base, new()
{
    public PagedResult()
    {
        Items = new List<T>();
    }
    
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    public int PageCount { get; set; }
    public List<T> Items { get; set; }
}