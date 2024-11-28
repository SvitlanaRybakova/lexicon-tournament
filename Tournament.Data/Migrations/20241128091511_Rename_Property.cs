using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tournament.Data.Migrations
{
    /// <inheritdoc />
    public partial class Rename_Property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Tournaments_TournamentModelId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "TournamentModelId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Tournaments_TournamentModelId",
                table: "Games",
                column: "TournamentModelId",
                principalTable: "Tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Tournaments_TournamentModelId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "TournamentModelId",
                table: "Games",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Tournaments_TournamentModelId",
                table: "Games",
                column: "TournamentModelId",
                principalTable: "Tournaments",
                principalColumn: "Id");
        }
    }
}
