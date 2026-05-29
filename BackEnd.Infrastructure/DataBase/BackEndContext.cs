


using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BackEnd.Infrastructure.DataBase;

public class BackEndContext:DbContext
{
    public BackEndContext(DbContextOptions<BackEndContext>options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
