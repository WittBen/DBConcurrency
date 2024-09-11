using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace DbConcurrency.Models;

public class Employee
{
  public int Id { get; set; }
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;

  public string Fullname => $"{FirstName} {LastName}";

  public string Department { get; set; } = string.Empty;
  public int EntryYear { get; set; }

  [ConcurrencyCheck]
  public byte[] RowVersion { get; set; }
}

public class DataTypeConiguration : IEntityTypeConfiguration<Employee>
{
  public void Configure(EntityTypeBuilder<Employee> builder)
  {
    builder.HasKey(c => c.Id);
    builder.Property(c => c.Id).ValueGeneratedOnAdd();

    builder.Property(rv => rv.RowVersion).IsRowVersion();
  }
}
