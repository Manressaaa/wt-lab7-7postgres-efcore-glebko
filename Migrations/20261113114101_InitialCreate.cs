using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wt_lab6_6minimalapi_glebko.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    nomer_reisa = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gorod_vilet = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    gorod_prilet = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    vremya_vileta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    cena = table.Column<decimal>(type: "numeric", nullable: false),
                    mesta = table.Column<int>(type: "integer", nullable: false),
                    dostupno = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.nomer_reisa);
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "nomer_reisa", "CreatedAt", "cena", "dostupno", "gorod_prilet", "gorod_vilet", "mesta", "vremya_vileta" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 250m, true, "Горки)", "Нюйорк", 45, new DateTime(2026, 11, 15, 8, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 180m, true, "Горки", "Минск", 30, new DateTime(2026, 11, 16, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 400m, false, "Горки", "Москва", 0, new DateTime(2026, 11, 17, 22, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 300m, true, "Горки", "Гомель", 50, new DateTime(2026, 12, 20, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 150m, true, "Горки", "Витебск", 20, new DateTime(2026, 12, 25, 6, 45, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
