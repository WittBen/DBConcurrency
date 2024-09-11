var serviceProvider = new ServiceCollection().AddTransient<IMyService, MyService>().BuildServiceProvider();

var service = serviceProvider.GetService<IMyService>()!;


//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//  In this example I will simulate a DBConcurrencyException
//  that does not occur in "free" runtime,
//  this is for illustration purposes only
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


// Simulate two parallel updates


// wihout strategy
service.UpdateEntity(1);

// client wins
service.UpdateEntity_ClientWins(1);

// database wins
service.UpdateEntity_DatabaseWins(1);
