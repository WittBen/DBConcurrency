namespace DbConcurrency.Services;

public interface IMyService
{
  void UpdateEntity(int Id);
  void UpdateEntity_ClientWins(int Id);
  void UpdateEntity_DatabaseWins(int Id);
}