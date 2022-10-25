using Application.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace Application.Database;

internal class ApplicationContext : DbContext
{
    public DbSet<Note> Notes { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=database.db");
    }
}