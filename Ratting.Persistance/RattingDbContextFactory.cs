using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ratting.Persistance;

public class RattingDbContextFactory: IDesignTimeDbContextFactory<RattingDbContext>
{
    public RattingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RattingDbContext>();
        optionsBuilder.UseSqlite("Data Source=game_ratting.db");

        return new RattingDbContext(optionsBuilder.Options);
    }
}