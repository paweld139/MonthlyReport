using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonthlyReport.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EntryHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Hours",
                table: "Entries",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hours",
                table: "Entries");
        }
    }
}
