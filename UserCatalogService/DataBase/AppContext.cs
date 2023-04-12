using Microsoft.EntityFrameworkCore;
using UserCatalogService.Models;

namespace UserCatalogService.DataBase;

public class AppContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "UserCatalog");
    }
    public DbSet<UserModel> Users { get; set; }
}
