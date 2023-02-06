using NDS_ToDo.Domain.Entities;

namespace NDS_ToDo.Domain.Contracts.Repository;
public interface IUserRepository : IRepository<User>
{
    Task<User> FindByEmail(string email);
    void Add(User user);
    Task<bool> IsEmailInUse(string email);
}