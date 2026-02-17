using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Vikta.Data.Academico;

public sealed class AcademicoDbContextFactory : IDesignTimeDbContextFactory<AcademicoDbContext>
{
    public AcademicoDbContext CreateDbContext(string[] args)
    {
        var connectionString =
            Environment.GetEnvironmentVariable("VIKTA_DB_CONNECTION")
            ?? "Server=localhost,1433;Database=ViktaDb;User ID=sa;Password=Admin@123;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True";

        var optionsBuilder = new DbContextOptionsBuilder<AcademicoDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new AcademicoDbContext(optionsBuilder.Options);
    }
}
