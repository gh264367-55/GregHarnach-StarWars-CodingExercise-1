using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using GregHarnach_starWars_CodingExercise.Data;
using GregHarnach_starWars_CodingExercise.Models;

namespace GregHarnach_starWars_CodingExercise.Seeding
{
    // DTOs that map snake_case JSON -> C#
    public class StarshipDto
    {
        [JsonPropertyName("name")] public string? Name { get; set; }
        [JsonPropertyName("model")] public string? Model { get; set; }
        [JsonPropertyName("manufacturer")] public string? Manufacturer { get; set; }
        [JsonPropertyName("cost_in_credits")] public string? CostInCredits { get; set; }
        [JsonPropertyName("length")] public string? Length { get; set; }
        [JsonPropertyName("max_atmosphering_speed")] public string? MaxAtmospheringSpeed { get; set; }
        [JsonPropertyName("crew")] public string? Crew { get; set; }
        [JsonPropertyName("passengers")] public string? Passengers { get; set; }
        [JsonPropertyName("cargo_capacity")] public string? CargoCapacity { get; set; }
        [JsonPropertyName("consumables")] public string? Consumables { get; set; }
        [JsonPropertyName("hyperdrive_rating")] public string? HyperdriveRating { get; set; }
        [JsonPropertyName("MGLT")] public string? MGLT { get; set; }
        [JsonPropertyName("starship_class")] public string? StarshipClass { get; set; }
        [JsonPropertyName("pilots")] public List<string>? Pilots { get; set; }
        [JsonPropertyName("films")] public List<string>? Films { get; set; }
        [JsonPropertyName("url")] public string? Url { get; set; }
    }

    public class StarshipSeeder(AppDbContext db, IHttpClientFactory httpClientFactory)
    {
        private readonly AppDbContext _db = db;
        private readonly HttpClient _http = httpClientFactory.CreateClient();

        public async Task SeedAsync()
        {
            if (await _db.Starships.AnyAsync()) return; // already seeded

            // Try swapi.info first (single endpoint that returns a list)
            var starships = await TryFetchFromSwapiInfoAsync()
                          ?? await FetchFromSwapiDevAsync(); // fallback paging

            if (starships?.Any() == true)
            {
                foreach (var s in starships)
                {
                    var entity = new Starship
                    {
                        Name = s.Name ?? "",
                        Model = s.Model,
                        Manufacturer = s.Manufacturer,
                        CostInCredits = ParseLong(s.CostInCredits),
                        Length = ParseDecimal(s.Length),
                        MaxAtmospheringSpeed = s.MaxAtmospheringSpeed,
                        Crew = s.Crew,
                        Passengers = s.Passengers,
                        CargoCapacity = ParseLong(s.CargoCapacity),
                        Consumables = s.Consumables,
                        HyperdriveRating = ParseDecimal(s.HyperdriveRating),
                        MGLT = s.MGLT,
                        StarshipClass = s.StarshipClass,
                        PilotsCsv = s.Pilots is { Count: > 0 } ? string.Join(",", s.Pilots) : null,
                        FilmsCsv = s.Films is { Count: > 0 } ? string.Join(",", s.Films) : null,
                        SwapiUrl = s.Url
                    };
                    _db.Starships.Add(entity);
                }
                await _db.SaveChangesAsync();
            }
        }

        private async Task<List<StarshipDto>?> TryFetchFromSwapiInfoAsync()
        {
            try
            {
                // swapi.info/starships returns JSON (array). If it changes, fallback is used.
                using var req = new HttpRequestMessage(HttpMethod.Get, "https://swapi.info/starships");
                using var resp = await _http.SendAsync(req);
                resp.EnsureSuccessStatusCode();
                return await resp.Content.ReadFromJsonAsync<List<StarshipDto>>();
            }
            catch
            {
                return null;
            }
        }

        private async Task<List<StarshipDto>> FetchFromSwapiDevAsync()
        {
            // Standard SWAPI DEV paged endpoint
            var list = new List<StarshipDto>();
            string? next = "https://swapi.dev/api/starships/";

            while (!string.IsNullOrEmpty(next))
            {
                var page = await _http.GetFromJsonAsync<SwapiDevPage<StarshipDto>>(next);
                if (page?.Results != null) list.AddRange(page.Results);
                next = page?.Next;
            }
            return list;
        }

        private static long? ParseLong(string? s)
            => long.TryParse((s ?? "").Replace(",", ""), out var v) ? v : null;

        private static decimal? ParseDecimal(string? s)
            => decimal.TryParse((s ?? "").Replace(",", ""), out var v) ? v : null;

        private class SwapiDevPage<T>
        {
            [JsonPropertyName("count")] public int Count { get; set; }
            [JsonPropertyName("next")] public string? Next { get; set; }
            [JsonPropertyName("previous")] public string? Previous { get; set; }
            [JsonPropertyName("results")] public List<T>? Results { get; set; }
        }
    }
}
