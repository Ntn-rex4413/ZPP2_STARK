using Microsoft.EntityFrameworkCore.Migrations;

namespace STARK_Project.Data.Migrations
{
    public partial class CreateConditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TresholdMax = table.Column<float>(type: "real", nullable: false),
                    TresholdMin = table.Column<float>(type: "real", nullable: false),
                    CryptocurrencyId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conditions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Conditions_Cryptocurrenies_CryptocurrencyId",
                        column: x => x.CryptocurrencyId,
                        principalTable: "Cryptocurrenies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conditions_CryptocurrencyId",
                table: "Conditions",
                column: "CryptocurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Conditions_UserId",
                table: "Conditions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conditions");
        }
    }
}
