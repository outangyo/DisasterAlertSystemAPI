using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisasterAlertSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAlertSettingsKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_alertSettings",
                table: "alertSettings");

            migrationBuilder.RenameColumn(
                name: "LocationCoordinates_longitude",
                table: "regions",
                newName: "LocationCoordinates_Longitude");

            migrationBuilder.RenameColumn(
                name: "LocationCoordinates_latitude",
                table: "regions",
                newName: "LocationCoordinates_Latitude");

            migrationBuilder.AlterColumn<string>(
                name: "DisasterTypes",
                table: "alertSettings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_alertSettings",
                table: "alertSettings",
                columns: new[] { "RegionId", "DisasterTypes" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_alertSettings",
                table: "alertSettings");

            migrationBuilder.RenameColumn(
                name: "LocationCoordinates_Longitude",
                table: "regions",
                newName: "LocationCoordinates_longitude");

            migrationBuilder.RenameColumn(
                name: "LocationCoordinates_Latitude",
                table: "regions",
                newName: "LocationCoordinates_latitude");

            migrationBuilder.AlterColumn<string>(
                name: "DisasterTypes",
                table: "alertSettings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_alertSettings",
                table: "alertSettings",
                column: "RegionId");
        }
    }
}
