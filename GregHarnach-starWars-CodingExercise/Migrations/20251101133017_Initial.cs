using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GregHarnach_starWars_CodingExercise.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Starships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CostInCredits = table.Column<long>(type: "bigint", nullable: true),
                    Length = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxAtmospheringSpeed = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Crew = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Passengers = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CargoCapacity = table.Column<long>(type: "bigint", nullable: true),
                    Consumables = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HyperdriveRating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MGLT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StarshipClass = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PilotsCsv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilmsCsv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SwapiUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Starships", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Starships_Name",
                table: "Starships",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Starships");
        }
    }
}
