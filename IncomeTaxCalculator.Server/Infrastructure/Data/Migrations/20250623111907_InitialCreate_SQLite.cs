using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IncomeTaxCalculator.Server.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_SQLite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxBrackets",
                columns: table => new
                {
                    TaxSystem = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    BandName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LowerLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpperLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RatePercentage = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxBrackets", x => new { x.TaxSystem, x.BandName });
                });

            migrationBuilder.InsertData(
                table: "TaxBrackets",
                columns: new[] { "BandName", "TaxSystem", "LowerLimit", "RatePercentage", "UpperLimit" },
                values: new object[,]
                {
                    { "Tax Band A", "UK", 0m, 0, 5000m },
                    { "Tax Band B", "UK", 5000m, 20, 20000m },
                    { "Tax Band C", "UK", 20000m, 40, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxBrackets_TaxSystem",
                table: "TaxBrackets",
                column: "TaxSystem");

            migrationBuilder.CreateIndex(
                name: "IX_TaxBrackets_TaxSystem_LowerLimit",
                table: "TaxBrackets",
                columns: new[] { "TaxSystem", "LowerLimit" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxBrackets");
        }
    }
}
