using NDS_ToDo.Domain.Entities;

namespace NDS_ToDo.Domain.Contracts.Repository;
public interface IRepository <T> : IDisposable where T : Base
{
    IUnitOfWork UnitOfWork { get; }
}