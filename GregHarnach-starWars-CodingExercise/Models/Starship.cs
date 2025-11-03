using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GregHarnach_starWars_CodingExercise.Models
{
    public class Starship
    {
        [Key]
        public int Id { get; set; }                 // Our DB PK (not SWAPI id)

        [Required, MaxLength(200)]
        public string Name { get; set; } = "";

        [MaxLength(200)]
        public string? Model { get; set; }

        [MaxLength(300)]
        public string? Manufacturer { get; set; }

        public long? CostInCredits { get; set; }    // many values fit in long

        public decimal? Length { get; set; }        // can be decimal (meters)

        [MaxLength(50)]
        public string? MaxAtmospheringSpeed { get; set; } // sometimes "n/a"

        [MaxLength(50)]
        public string? Crew { get; set; }           // often ranges like "5-10"

        [MaxLength(50)]
        public string? Passengers { get; set; }

        public long? CargoCapacity { get; set; }

        [MaxLength(100)]
        public string? Consumables { get; set; }

        public decimal? HyperdriveRating { get; set; }

        [MaxLength(50)]
        public string? MGLT { get; set; }           // can be "unknown"

        [MaxLength(100)]
        public string? StarshipClass { get; set; }

        // Keep raw references for simplicity (comma-joined). In a fuller model,
        // you’d normalize these relations.
        public string? PilotsCsv { get; set; }
        public string? FilmsCsv { get; set; }

        [MaxLength(300)]
        public string? SwapiUrl { get; set; }
    }
}
