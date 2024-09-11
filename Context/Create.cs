using Microsoft.Extensions.Configuration;

namespace DbConcurrency.Context
{
  public class Create
  {
    public static DBContext Context()
    {
      var configurationBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

      IConfigurationRoot configuration = configurationBuilder.Build();
      string connectionString = "Server=tcp:localhost,1433;Initial Catalog=MyDatabase;Persist Security Info=True;User ID=***;Password=****;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

      DbContextOptionsBuilder<DBContext> optionsBuilder = new DbContextOptionsBuilder<DBContext>()
          .UseSqlServer(connectionString, x => x.MigrationsAssembly("DB.Migrations"));

      if (!optionsBuilder.IsConfigured)
        throw new Exception("context doesn't configured");
      else
        return new DBContext(optionsBuilder.Options);
    }
  }
}
