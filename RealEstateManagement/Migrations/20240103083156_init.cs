using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateManagement.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TenantTble",
                columns: table => new
                {
                    TenantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    T_ContactInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaseStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantTble", x => x.TenantId);
                });

            migrationBuilder.CreateTable(
                name: "AssetTble",
                columns: table => new
                {
                    AssetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    P_Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfUnits = table.Column<int>(type: "int", nullable: false),
                    RentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetTble", x => x.AssetId);
                    table.ForeignKey(
                        name: "FK_AssetTble_TenantTble_TenantId",
                        column: x => x.TenantId,
                        principalTable: "TenantTble",
                        principalColumn: "TenantId");
                });

            migrationBuilder.CreateTable(
                name: "OwnerTble",
                columns: table => new
                {
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Own_ContactInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    AssetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerTble", x => x.OwnerId);
                    table.ForeignKey(
                        name: "FK_OwnerTble_AssetTble_AssetId",
                        column: x => x.AssetId,
                        principalTable: "AssetTble",
                        principalColumn: "AssetId");
                    table.ForeignKey(
                        name: "FK_OwnerTble_TenantTble_TenantId",
                        column: x => x.TenantId,
                        principalTable: "TenantTble",
                        principalColumn: "TenantId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetTble_TenantId",
                table: "AssetTble",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerTble_AssetId",
                table: "OwnerTble",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerTble_TenantId",
                table: "OwnerTble",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnerTble");

            migrationBuilder.DropTable(
                name: "AssetTble");

            migrationBuilder.DropTable(
                name: "TenantTble");
        }
    }
}
