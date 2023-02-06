using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NDS_ToDo.Domain.Contracts;
using NDS_ToDo.Domain.Entities;

namespace NDS_ToDo.Infra.Context;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Assignment> Assignment {get ; set; }
    public DbSet<AssignmentList> AssignmentLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e=> e.GetProperties())
            .Where(p => p.ClrType == typeof(string)))
            property.SetColumnType("varchar(100)");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    public async Task<bool> Commit() => await SaveChangesAsync() > 0;
}