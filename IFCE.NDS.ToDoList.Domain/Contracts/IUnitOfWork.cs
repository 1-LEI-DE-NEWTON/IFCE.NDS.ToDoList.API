namespace NDS_ToDo.Domain.Contracts;

public interface IUnitOfWork
{
    Task<bool> Commit();
}
