using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using People.Core.Entities;

namespace People.Core.DataContext;

public class PeopleDbContext : DbContext
{
    public DbSet<Person> People { get; set; }
    
    public PeopleDbContext(DbContextOptions<PeopleDbContext> options) : base(options)
    {
        
    }
}