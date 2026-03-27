using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisasterAlertSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alertSettings",
                columns: table => new
                {
                    RegionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisasterTypes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThresholdScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alertSettings", x => x.RegionId);
                });

            migrationBuilder.CreateTable(
                name: "disasterRisks",
                columns: table => new
                {
                    RegionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisasterType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RiskScore = table.Column<int>(type: "int", nullable: false),
                    RiskLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlertTriggered = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disasterRisks", x => new { x.RegionId, x.DisasterType });
                });

            migrationBuilder.CreateTable(
                name: "regions",
                columns: table => new
                {
                    RegionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocationCoordinates_latitude = table.Column<double>(type: "float", nullable: false),
                    LocationCoordinates_longitude = table.Column<double>(type: "float", nullable: false),
                    DisasterTypes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_regions", x => x.RegionId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alertSettings");

            migrationBuilder.DropTable(
                name: "disasterRisks");

            migrationBuilder.DropTable(
                name: "regions");
        }
    }
}
