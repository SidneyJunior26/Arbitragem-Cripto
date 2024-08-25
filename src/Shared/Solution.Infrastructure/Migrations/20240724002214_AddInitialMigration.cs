using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ADM_CONFIGURATIONS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MinSpread = table.Column<int>(type: "int", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "COINS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Symbol = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RegisterDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EXCHANGES",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiUrl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiKey = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiSecretKey = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RegisterDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NETWORKS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RegisterDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Trial = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TrialExpiration = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ORDER_BOOKS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Side = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "double", nullable: false),
                    Amount = table.Column<double>(type: "double", nullable: false),
                    TotalValue = table.Column<double>(type: "double", nullable: false),
                    CoinId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ExchangeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RegisterDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ORDER_BOOKS_COINS_CoinId",
                        column: x => x.CoinId,
                        principalTable: "COINS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ORDER_BOOKS_EXCHANGES_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "EXCHANGES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "COINS_NETWORKS",
                columns: table => new
                {
                    CoinId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NetworkId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RegisterDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FOREIGN", x => new { x.CoinId, x.NetworkId });
                    table.ForeignKey(
                        name: "FK_COINS_NETWORKS_COINS_CoinId",
                        column: x => x.CoinId,
                        principalTable: "COINS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COINS_NETWORKS_NETWORKS_NetworkId",
                        column: x => x.NetworkId,
                        principalTable: "NETWORKS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OPPORTUNITIES",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LowerValue = table.Column<double>(type: "double", nullable: false),
                    HighestValue = table.Column<double>(type: "double", nullable: false),
                    DifferencePercentage = table.Column<double>(type: "double", nullable: false),
                    CoinId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ExchangeToBuyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ExchangeToSellId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    OrderBookId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RegisterDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OPPORTUNITIES_COINS_CoinId",
                        column: x => x.CoinId,
                        principalTable: "COINS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OPPORTUNITIES_EXCHANGES_ExchangeToBuyId",
                        column: x => x.ExchangeToBuyId,
                        principalTable: "EXCHANGES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OPPORTUNITIES_EXCHANGES_ExchangeToSellId",
                        column: x => x.ExchangeToSellId,
                        principalTable: "EXCHANGES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OPPORTUNITIES_ORDER_BOOKS_OrderBookId",
                        column: x => x.OrderBookId,
                        principalTable: "ORDER_BOOKS",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ADM_CONFIG_ID_INDEX",
                table: "ADM_CONFIGURATIONS",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "COIN_ID_INDEX",
                table: "COINS",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "COIN_NETWORK_ID_INDEX",
                table: "COINS_NETWORKS",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COINS_NETWORKS_NetworkId",
                table: "COINS_NETWORKS",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "EXCHANGE_ID_INDEX",
                table: "EXCHANGES",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "NETWORK_ID_INDEX",
                table: "NETWORKS",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OPPORTUNITIES_CoinId",
                table: "OPPORTUNITIES",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_OPPORTUNITIES_ExchangeToBuyId",
                table: "OPPORTUNITIES",
                column: "ExchangeToBuyId");

            migrationBuilder.CreateIndex(
                name: "IX_OPPORTUNITIES_ExchangeToSellId",
                table: "OPPORTUNITIES",
                column: "ExchangeToSellId");

            migrationBuilder.CreateIndex(
                name: "IX_OPPORTUNITIES_OrderBookId",
                table: "OPPORTUNITIES",
                column: "OrderBookId");

            migrationBuilder.CreateIndex(
                name: "OPPORTUNITY_ID_INDEX",
                table: "OPPORTUNITIES",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_BOOKS_CoinId",
                table: "ORDER_BOOKS",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_BOOKS_ExchangeId",
                table: "ORDER_BOOKS",
                column: "ExchangeId");

            migrationBuilder.CreateIndex(
                name: "ORDER_BOOK_ID_INDEX",
                table: "ORDER_BOOKS",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "USER_ID_INDEX",
                table: "USERS",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADM_CONFIGURATIONS");

            migrationBuilder.DropTable(
                name: "COINS_NETWORKS");

            migrationBuilder.DropTable(
                name: "OPPORTUNITIES");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "NETWORKS");

            migrationBuilder.DropTable(
                name: "ORDER_BOOKS");

            migrationBuilder.DropTable(
                name: "COINS");

            migrationBuilder.DropTable(
                name: "EXCHANGES");
        }
    }
}
