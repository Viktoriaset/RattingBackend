using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ratting.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class add_money : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Money",
                table: "players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Money",
                table: "players");
        }
    }
}
