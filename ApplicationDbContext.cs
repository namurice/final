using Microsoft.EntityFrameworkCore;

namespace FinalsProject;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source=database.db");
}

   
