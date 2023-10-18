using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BookBook.Repository.ContextFactory;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{
    public RepositoryContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
                .SetBasePath("C:\\ViVi\\CodeLearn\\BookBook\\BookBook.API")
                .AddJsonFile("appsettings.Development.json")
                .Build();

        var connectionString = configuration["MySqlConnection:ConnectionString"];

        var builder = new DbContextOptionsBuilder<RepositoryContext>()
            .UseMySql(connectionString,
            MySqlServerVersion.LatestSupportedServerVersion,
                mySqlOptions =>
                        mySqlOptions
                        .EnableRetryOnFailure(
                                        maxRetryCount: 3,
                                        maxRetryDelay: TimeSpan.FromSeconds(50),
                                        errorNumbersToAdd: null)
                                    .MigrationsAssembly("BookBook.Repository")               
                );
        return new RepositoryContext(builder.Options);
    }
}
