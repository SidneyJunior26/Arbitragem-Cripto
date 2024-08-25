using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCryptoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_COINS_NETWORKS_COINS_CoinId",
                table: "COINS_NETWORKS");

            migrationBuilder.DropForeignKey(
                name: "FK_OPPORTUNITIES_COINS_CoinId",
                table: "OPPORTUNITIES");

            migrationBuilder.DropForeignKey(
                name: "FK_ORDER_BOOKS_COINS_CoinId",
                table: "ORDER_BOOKS");

            migrationBuilder.DropTable(
                name: "COINS");

            migrationBuilder.CreateTable(
                name: "CRYPTOS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Symbol = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "COIN_ID_INDEX",
                table: "CRYPTOS",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_COINS_NETWORKS_CRYPTOS_CoinId",
                table: "COINS_NETWORKS",
                column: "CoinId",
                principalTable: "CRYPTOS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OPPORTUNITIES_CRYPTOS_CoinId",
                table: "OPPORTUNITIES",
                column: "CoinId",
                principalTable: "CRYPTOS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ORDER_BOOKS_CRYPTOS_CoinId",
                table: "ORDER_BOOKS",
                column: "CoinId",
                principalTable: "CRYPTOS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_COINS_NETWORKS_CRYPTOS_CoinId",
                table: "COINS_NETWORKS");

            migrationBuilder.DropForeignKey(
                name: "FK_OPPORTUNITIES_CRYPTOS_CoinId",
                table: "OPPORTUNITIES");

            migrationBuilder.DropForeignKey(
                name: "FK_ORDER_BOOKS_CRYPTOS_CoinId",
                table: "ORDER_BOOKS");

            migrationBuilder.DropTable(
                name: "CRYPTOS");

            migrationBuilder.CreateTable(
                name: "COINS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RegisterDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Symbol = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "COIN_ID_INDEX",
                table: "COINS",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_COINS_NETWORKS_COINS_CoinId",
                table: "COINS_NETWORKS",
                column: "CoinId",
                principalTable: "COINS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OPPORTUNITIES_COINS_CoinId",
                table: "OPPORTUNITIES",
                column: "CoinId",
                principalTable: "COINS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ORDER_BOOKS_COINS_CoinId",
                table: "ORDER_BOOKS",
                column: "CoinId",
                principalTable: "COINS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
