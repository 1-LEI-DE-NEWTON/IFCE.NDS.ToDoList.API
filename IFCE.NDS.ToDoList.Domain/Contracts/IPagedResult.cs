using NDS_ToDo.Domain.Entities;

namespace NDS_ToDo.Domain.Contracts;

public interface IPagedResult <T> where T: Base, new()
{
    public List<T> Items { get; set; }
    public int Total { get; set; }
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int PageCount { get; set; }
}