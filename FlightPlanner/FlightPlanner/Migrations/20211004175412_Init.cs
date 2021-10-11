using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightPlanner.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirportCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FLights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromId = table.Column<int>(type: "int", nullable: true),
                    ToId = table.Column<int>(type: "int", nullable: true),
                    Carrier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartureTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArrivalTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FLights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FLights_Airports_FromId",
                        column: x => x.FromId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FLights_Airports_ToId",
                        column: x => x.ToId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FLights_FromId",
                table: "FLights",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_FLights_ToId",
                table: "FLights",
                column: "ToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FLights");

            migrationBuilder.DropTable(
                name: "Airports");
        }
    }
}
