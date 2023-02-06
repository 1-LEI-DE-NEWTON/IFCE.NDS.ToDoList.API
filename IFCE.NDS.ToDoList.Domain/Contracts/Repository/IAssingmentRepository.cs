using NDS_ToDo.Domain.Entities;
using NDS_ToDo.Domain.FIlter;

namespace NDS_ToDo.Domain.Contracts.Repository;
public interface IAssignmentRepository : IRepository<Assignment>
{
    Task<IPagedResult<Assignment>> Search(Guid userId, AssignmentFilter filter, int perPage = 10,
        int page = 1, Guid? listId = null);
    Task<Assignment> GetByID(Guid id, Guid userId);
    void Add(Assignment assignment);
    void Edit(Assignment assignment);
    void Delete(Assignment assignment);
}
