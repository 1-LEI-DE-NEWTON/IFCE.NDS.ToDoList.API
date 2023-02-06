namespace NDS_ToDo.Application.DTOs.Paged;

public class PagedDto<T>
{
    public PagedDto()
    {
        Items = new List<T>();
    }
    
    public List<T> Items { get; set; }
    
    public int Page { get; set; }
    public int Total { get; set; }
    public int PerPage { get; set; }
    public int PageCount { get; set; }
}