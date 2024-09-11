namespace DbConcurrency.Services;

public class MyService : IMyService
{
  private readonly DBContext _context;

  public MyService()
  {
    if (_context == null)
      _context = Create.Context();
  }


  /// <summary>
  /// Update method with Client-Wins strategy
  /// </summary>
  /// <param name="id"></param>
  public void UpdateEntity(int id)
  {
    using (var dbContext1 = Create.Context())
    using (var dbContext2 = Create.Context())
    {
      var loadedEmployee = dbContext1.Employees.Find(id);
      var existingEmployeeInDatabase = dbContext2.Employees.Find(id);

      if (loadedEmployee != null && existingEmployeeInDatabase != null)
      {
        loadedEmployee.LastName = "NewLastName1";
        dbContext1.SaveChanges();


        // Change existingEmployeeInDatabase now
        existingEmployeeInDatabase.LastName = "NewLastName2";

        try
        {
          dbContext2.SaveChanges();  // Try to save changes in existingEmployeeInDatabase, which should throw a DbUpdateConcurrencyException
          Console.WriteLine("Records updated successfully.");
        }
        catch (DbUpdateConcurrencyException ex)
        {
          Console.WriteLine($"Concurrency conflict on existingEmployeeInDatabase: {ex.Message}");
          // Here you can implement conflict handling, e.g. retry or report errors to the user interface.
        }
      }
    }
  }


  /// <summary>
  /// Update method with client wins strategy
  /// </summary>
  /// <param name="id"></param>
  public void UpdateEntity_ClientWins(int id)
  {
    using (var dbContext1 = Create.Context())
    using (var dbContext2 = Create.Context())
    {
      var loadedEmployee = dbContext1.Employees.Find(id);
      var existingEmployeeInDatabase = dbContext2.Employees.Find(id);

      if (loadedEmployee != null && existingEmployeeInDatabase != null)
      {
        loadedEmployee.LastName = "NewLastName1";

        try
        {
          dbContext1.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          Console.WriteLine($"Concurrency conflict on LoadedEmployee: {ex.Message}");

          ex.Entries.Single().Reload();
          dbContext1.SaveChanges();
          Console.WriteLine("Client Wins: Records updated successfully.");
        }

        // Change existingEmployeeInDatabase now
        existingEmployeeInDatabase.LastName = "NewLastName2";

        try
        {
          dbContext2.SaveChanges();  // Try to save changes in existingEmployeeInDatabase, which should throw a DbUpdateConcurrencyException
          Console.WriteLine("Records updated successfully.");
        }
        catch (DbUpdateConcurrencyException ex)
        {
          Console.WriteLine($"Concurrency conflict on existingEmployeeInDatabase: {ex.Message}");
          // Here you can implement conflict handling, e.g. retry or report errors to the user interface.
        }
      }
    }
  }


  /// <summary>
  /// Update method with database wins strategy
  /// </summary>
  /// <param name="id"></param>
  public void UpdateEntity_DatabaseWins(int id)
  {
    using (var dbContext1 = Create.Context())
    using (var dbContext2 = Create.Context())
    {
      var loadedEmployee = dbContext1.Employees.Find(id);
      var existingEmployeeInDatabase = dbContext2.Employees.Find(id);

      if (loadedEmployee != null && existingEmployeeInDatabase != null)
      {
        loadedEmployee.LastName = "NewLastName1";

        try
        {
          dbContext1.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
          Console.WriteLine($"Concurrency conflict on LoadedEmployee: {ex.Message}");
        }

        // Change existingEmployeeInDatabase now
        existingEmployeeInDatabase.LastName = "NewLastName2";

        try
        {
          dbContext2.SaveChanges();  // Try to save changes in existingEmployeeInDatabase, which should throw a DbUpdateConcurrencyException
          Console.WriteLine("Records updated successfully.");
        }
        catch (DbUpdateConcurrencyException ex)
        {
          Console.WriteLine($"Concurrency conflict on existingEmployeeInDatabase: {ex.Message}");
          ex.Entries.Single().Reload();
          dbContext1.SaveChanges();
          Console.WriteLine("Database Wins: Records updated successfully.");
        }
      }
    }
  }
}
