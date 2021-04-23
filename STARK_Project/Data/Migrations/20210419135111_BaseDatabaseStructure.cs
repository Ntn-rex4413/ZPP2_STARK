using Microsoft.EntityFrameworkCore.Migrations;

namespace STARK_Project.Data.Migrations
{
    public partial class BaseDatabaseStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CryptocurrenyId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cryptocurrenies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cryptocurrenies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cryptocurrenies_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CryptocurrenyUser",
                columns: table => new
                {
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WatchlistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptocurrenyUser", x => new { x.UsersId, x.WatchlistId });
                    table.ForeignKey(
                        name: "FK_CryptocurrenyUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CryptocurrenyUser_Cryptocurrenies_WatchlistId",
                        column: x => x.WatchlistId,
                        principalTable: "Cryptocurrenies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CryptocurrenyId",
                table: "AspNetUsers",
                column: "CryptocurrenyId");

            migrationBuilder.CreateIndex(
                name: "IX_Cryptocurrenies_UserId",
                table: "Cryptocurrenies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CryptocurrenyUser_WatchlistId",
                table: "CryptocurrenyUser",
                column: "WatchlistId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cryptocurrenies_CryptocurrenyId",
                table: "AspNetUsers",
                column: "CryptocurrenyId",
                principalTable: "Cryptocurrenies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cryptocurrenies_CryptocurrenyId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CryptocurrenyUser");

            migrationBuilder.DropTable(
                name: "Cryptocurrenies");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CryptocurrenyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CryptocurrenyId",
                table: "AspNetUsers");
        }
    }
}
