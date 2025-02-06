namespace FizzBuzzFlex.Tests.Utils;

using System;
using FizzBuzzFlex.EF.Context;
using Microsoft.EntityFrameworkCore;

public static class DatabaseContextHelper
{
    public static DatabaseContext CreateInMemoryDatabase()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());
        optionsBuilder.EnableSensitiveDataLogging();

        return new DatabaseContext(optionsBuilder.Options);
    }
}