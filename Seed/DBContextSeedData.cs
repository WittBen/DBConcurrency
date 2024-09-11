using static DbConcurrency.Services.MyService;

namespace DbConcurrency.Seed;

public static class DBContextSeedData
{

  public static void EnsureSeedData(this DBContext context)
  {

    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    var customers = GenerateCustomers();
    context.Employees.AddRange(customers);
    context.SaveChanges();
  }


  #region Data
  public static Employee[] GenerateCustomers()
  {
    Employee[] myData = new Employee[1];

    string[] sureName = { "Thomas", "Stephan", "Axel", "Markus", "Michel", "Tom", "Nadine", "Brit", "Fritz", "Mike" };
    string[] lastName = { "Meier", "Schmitt", "Weiner", "Bitner", "Antony", "Maxwell", "Joshua", "Fury", "Tyson" };

    string[] department = { "Access Board", "Cabinet Office", "", "Marketing", "Operation", "Sales", "HR", "Purchase" };


    for (int i = 0; i < 1; i++)
    {
      Array.Resize(ref myData, i + 1);
      myData[i] = new Employee()
      {
        FirstName = sureName[new Random().Next(0, sureName.Length)],
        LastName = lastName[new Random().Next(0, lastName.Length)],
        Department = department[new Random().Next(0, department.Length)],
        EntryYear = new Random().Next(1985, 2015)
      };
    }

    return myData;
  }
  #endregion
}