using BookBook.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookBook.BookAPI;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{
    public RepositoryContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
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
                                    .MigrationsAssembly("BookBook.API")               
                );
        return new RepositoryContext(builder.Options);
    }
}
