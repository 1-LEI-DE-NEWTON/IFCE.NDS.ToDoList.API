using NDS_ToDo.Domain.Entities;

namespace NDS_ToDo.Domain.Contracts.Repository;

public interface IAssignmentListRepository : IRepository<AssignmentList>
{
    Task<IPagedResult<AssignmentList>> Search(Guid userId, string name, int perPage = 10, int page = 1);
    Task<AssignmentList> GetById(Guid id, Guid userId);
    Task<AssignmentList> GetByIdWithAssignments(Guid id, Guid userId);
    void Add(AssignmentList assignment);
    void Edit(AssignmentList assignment);
    void Delete(AssignmentList assignment);
}
