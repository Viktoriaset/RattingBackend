using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ratting.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class add_feaild_bestResult_for_table_players : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BestResult",
                table: "players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestResult",
                table: "players");
        }
    }
}
