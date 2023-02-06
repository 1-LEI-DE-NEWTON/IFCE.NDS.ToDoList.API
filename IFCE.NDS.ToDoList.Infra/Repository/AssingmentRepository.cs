using Microsoft.EntityFrameworkCore;
using NDS_ToDo.Domain.Contracts;
using NDS_ToDo.Domain.Contracts.Repository;
using NDS_ToDo.Domain.Entities;
using NDS_ToDo.Domain.FIlter;
using NDS_ToDo.Infra.Context;
using NDS_ToDo.Infra.Paged;

namespace NDS_ToDo.Infra.Repository;

public class AssignmentRepository : IAssignmentRepository
{
    private readonly ApplicationDbContext _context;

    public AssignmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IPagedResult<Assignment>> Search(Guid userId, AssignmentFilter filter, int perPage = 10, int page = 1, Guid? listId = null)
    {
        var query = _context.Assignment
            .AsNoTrackingWithIdentityResolution()
            .AsQueryable();

        ApplyFilter(userId, filter, ref query, listId);

        ApplyOrdenation(filter, ref query);

        var result = new PagedResult<Assignment>
        {
            Items = await query.Skip((page - 1) * perPage).Take(perPage).ToListAsync(),
            Total = await query.CountAsync(),
            Page = page,
            PerPage = perPage
        };

        var pageCount = (double)result.Total / perPage;
        result.PageCount = (int)Math.Ceiling(pageCount);

        return result;
    }

    public async Task<Assignment> GetByID(Guid id, Guid userId)
    {
        return await _context.Assignment
                            .FirstOrDefaultAsync(c => c.UserId == userId && c.Id == id);
    }

    public void Add(Assignment assignment)
    {
        _context.Assignment.Add(assignment);
    }

    public void Edit(Assignment assignment)
    {
        _context.Assignment.Update(assignment);
    }

    public void Delete(Assignment assignment)
    {
        _context.Assignment.Remove(assignment);
    }

    private static void ApplyFilter(Guid userId, AssignmentFilter filter, ref IQueryable<Assignment> query, Guid? listId = null)
    {
        if (!string.IsNullOrWhiteSpace(filter.Description))
            query = query.Where(c => c.Description.Contains(filter.Description));

        if (filter.Concluded.HasValue)
            query = query.Where(c => c.Concluded == filter.Concluded.Value);

        if (filter.StartDeadline.HasValue)
            query = query.Where(c => c.Deadline >= filter.StartDeadline.Value);

        if (filter.EndDeadline.HasValue)
            query = query.Where(c => c.Deadline <= filter.EndDeadline.Value);

        if (listId.HasValue)
        {
            query = query
                .Where(c => c.AssingmentListId == listId)
                .Where(c => c.AssignmentList.UserId == userId);
        }

        if (!listId.HasValue)
        {
            query = query
                .Where(c => c.AssingmentListId == null)
                .Where(c => c.UserId == userId);
        }
    }

    private static void ApplyOrdenation(AssignmentFilter filter, ref IQueryable<Assignment> query)
    {
        if (filter.OrderDir.ToLower().Equals("asc"))
        {
            query = filter.OrderBy.ToLower() switch
            {
                "description" => query.OrderBy(c => c.Description),
                "concluded" => query.OrderBy(c => c.Concluded),
                "deadline" => query.OrderBy(c => c.Deadline),
                _ => query.OrderBy(c => c.CreatedAt)
            };
            return;
        }

        query = filter.OrderBy.ToLower() switch
        {
            "description" => query.OrderByDescending(c => c.Description),
            "concluded" => query.OrderByDescending(c => c.Concluded),
            "deadline" => query.OrderByDescending(c => c.Deadline),
            _ => query.OrderByDescending(c => c.CreatedAt)
        };
    }

    public void Dispose()
    {
        _context?.Dispose();
    }

}