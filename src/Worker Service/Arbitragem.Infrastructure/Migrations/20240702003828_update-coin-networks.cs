using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArbitraX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatecoinnetworks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_COINS_NETWORKS",
                table: "COINS_NETWORKS");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "COINS_NETWORKS",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_COINS_NETWORKS",
                table: "COINS_NETWORKS",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_COINS_NETWORKS_CoinId",
                table: "COINS_NETWORKS",
                column: "CoinId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_COINS_NETWORKS",
                table: "COINS_NETWORKS");

            migrationBuilder.DropIndex(
                name: "IX_COINS_NETWORKS_CoinId",
                table: "COINS_NETWORKS");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "COINS_NETWORKS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_COINS_NETWORKS",
                table: "COINS_NETWORKS",
                columns: new[] { "CoinId", "NetworkId" });
        }
    }
}
