namespace DbConcurrency.Context;

public class DBContext : DbContext
{
  public DBContext(DbContextOptions<DBContext> options)
  : base(options)
  {
    this.EnsureSeedData();
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(Employee).Assembly);
  }


  public DbSet<Employee> Employees { get; set; }
}
