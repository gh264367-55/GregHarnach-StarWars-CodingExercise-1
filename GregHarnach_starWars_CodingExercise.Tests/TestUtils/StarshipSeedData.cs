using GregHarnach_starWars_CodingExercise.Models;
using GregHarnach_starWars_CodingExercise.Data;

namespace GregHarnach_starWars_CodingExercise.Tests.TestUtils
{
    internal static class StarshipSeedData
    {
        public static void AddSampleStarships(AppDbContext db)
        {
            db.Starships.AddRange(
                new Starship { Name = "Millennium Falcon", Model = "YT-1300", Manufacturer = "Corellian Engineering", StarshipClass = "Light freighter", HyperdriveRating = 0.5m },
                new Starship { Name = "X-wing", Model = "T-65", Manufacturer = "Incom Corporation", StarshipClass = "Starfighter", HyperdriveRating = 1.0m },
                new Starship { Name = "TIE Advanced x1", Model = "Twin Ion Engine", Manufacturer = "Sienar Fleet Systems", StarshipClass = "Starfighter", HyperdriveRating = null }
            );
            db.SaveChanges();
        }
    }
}
