using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wt_lab7_7postgres_efcore_glebko.Migrations
{
    /// <inheritdoc />
    public partial class AddBaggageWeight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BaggageWeight",
                table: "Flights",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "nomer_reisa",
                keyValue: 1,
                column: "BaggageWeight",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "nomer_reisa",
                keyValue: 2,
                column: "BaggageWeight",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "nomer_reisa",
                keyValue: 3,
                column: "BaggageWeight",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "nomer_reisa",
                keyValue: 4,
                column: "BaggageWeight",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "nomer_reisa",
                keyValue: 5,
                column: "BaggageWeight",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaggageWeight",
                table: "Flights");
        }
    }
}
