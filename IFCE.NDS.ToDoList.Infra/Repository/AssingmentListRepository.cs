using NDS_ToDo.Infra.Context;
using NDS_ToDo.Domain.Entities;
using NDS_ToDo.Infra.Paged;
using Microsoft.EntityFrameworkCore;
using NDS_ToDo.Domain.Contracts;
using NDS_ToDo.Domain.Contracts.Repository;

namespace NDS_ToDo.Infra.Repository;

public class AssignmentListRepository : IAssignmentListRepository
{
    private readonly ApplicationDbContext _context;

    public AssignmentListRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IPagedResult<AssignmentList>> Search(Guid userId, string name, int perPage = 10, int page = 1)
    {
        var query = _context.AssignmentLists
            .AsNoTrackingWithIdentityResolution()
            .Where(c => c.UserId == userId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(c => c.Name.Contains(name));

        var result = new PagedResult<AssignmentList>
        {
            Items = await query.OrderBy(c => c.Name).Skip((page - 1) * perPage).Take(perPage).ToListAsync(),
            Total = await query.CountAsync(),
            Page = page,
            PerPage = perPage
        };

        var pageCount = (double)result.Total / perPage;
        result.PageCount = (int)Math.Ceiling(pageCount);

        return result;
    }

    public async Task<AssignmentList> GetById(Guid id, Guid userId)
    {
        return await _context.AssignmentLists
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
    }

    public async Task<AssignmentList> GetByIdWithAssignments(Guid id, Guid userId)
    {
        return await _context.AssignmentLists
            .Include(c => c.Assignments)
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
    }

    public void Add(AssignmentList assignment)
    {
        _context.AssignmentLists.Add(assignment);
    }

    public void Edit(AssignmentList assignment)
    {
        _context.AssignmentLists.Update(assignment);
    }

    public void Delete(AssignmentList assignment)
    {
        _context.AssignmentLists.Remove(assignment);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

}