using CreditCardApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CreditCardApiTests.Utils;

public static class DbHelper
{
    public static AppDbContext CreateContext (string dbName)
    {
         var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new AppDbContext(options);

    }
}