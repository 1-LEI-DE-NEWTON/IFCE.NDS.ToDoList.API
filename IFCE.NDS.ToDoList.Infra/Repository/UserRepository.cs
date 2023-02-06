using NDS_ToDo.Domain.Entities;
using NDS_ToDo.Infra.Context;
using Microsoft.EntityFrameworkCore;
using NDS_ToDo.Domain.Contracts.Repository;
using NDS_ToDo.Domain.Contracts;

namespace NDS_ToDo.Infra.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> FindByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public IUnitOfWork UnitOfWork => _context;
    public void Add(User user)
    {
        _context.Users.Add(user);
    }

    public async Task<bool> IsEmailInUse(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
    
    public void Dispose()
    {
        _context?.Dispose();
    }
}