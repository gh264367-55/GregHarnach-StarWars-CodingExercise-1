using System;
using Microsoft.EntityFrameworkCore;
using GregHarnach_starWars_CodingExercise.Data;

namespace GregHarnach_starWars_CodingExercise.Tests.TestUtils
{
    internal static class TestDbFactory
    {
        public static AppDbContext CreateInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"StarshipsDb_Test_{Guid.NewGuid()}")
                .Options;

            var db = new AppDbContext(options);
            db.Database.EnsureCreated();
            return db;
        }
    }
}
