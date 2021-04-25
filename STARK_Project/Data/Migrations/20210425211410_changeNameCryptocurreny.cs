using Microsoft.EntityFrameworkCore.Migrations;

namespace STARK_Project.Data.Migrations
{
    public partial class changeNameCryptocurreny : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cryptocurrenies_CryptocurrenyId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CryptocurrenyUser");

            migrationBuilder.RenameColumn(
                name: "CryptocurrenyId",
                table: "AspNetUsers",
                newName: "CryptocurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_CryptocurrenyId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_CryptocurrencyId");

            migrationBuilder.CreateTable(
                name: "CryptocurrencyUser",
                columns: table => new
                {
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WatchlistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptocurrencyUser", x => new { x.UsersId, x.WatchlistId });
                    table.ForeignKey(
                        name: "FK_CryptocurrencyUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CryptocurrencyUser_Cryptocurrenies_WatchlistId",
                        column: x => x.WatchlistId,
                        principalTable: "Cryptocurrenies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CryptocurrencyUser_WatchlistId",
                table: "CryptocurrencyUser",
                column: "WatchlistId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cryptocurrenies_CryptocurrencyId",
                table: "AspNetUsers",
                column: "CryptocurrencyId",
                principalTable: "Cryptocurrenies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cryptocurrenies_CryptocurrencyId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CryptocurrencyUser");

            migrationBuilder.RenameColumn(
                name: "CryptocurrencyId",
                table: "AspNetUsers",
                newName: "CryptocurrenyId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_CryptocurrencyId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_CryptocurrenyId");

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
    }
}
