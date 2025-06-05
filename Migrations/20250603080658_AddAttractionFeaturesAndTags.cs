using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TH_WEB.Migrations
{
    public partial class AddAttractionFeaturesAndTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    ReviewCount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    FavoriteCount = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attractions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttractionFeature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttractionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IconClass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttractionFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttractionFeature_Attractions_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttractionImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttractionId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttractionImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttractionImage_Attractions_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttractionTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttractionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttractionTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttractionTag_Attractions_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperatingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttractionId = table.Column<int>(type: "int", nullable: false),
                    Monday = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tuesday = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wednesday = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Thursday = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Friday = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Saturday = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sunday = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperatingHours_Attractions_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pricing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttractionId = table.Column<int>(type: "int", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    AdultPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ChildPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SeniorPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StudentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pricing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pricing_Attractions_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttractionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    HelpfulCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Attractions_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9425), new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9426) });

            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9428), new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9428) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9342), new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9343) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9369), new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9370) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9373), new DateTime(2025, 6, 3, 8, 6, 57, 496, DateTimeKind.Utc).AddTicks(9373) });

            migrationBuilder.CreateIndex(
                name: "IX_AttractionFeature_AttractionId",
                table: "AttractionFeature",
                column: "AttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttractionImage_AttractionId",
                table: "AttractionImage",
                column: "AttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttractionTag_AttractionId",
                table: "AttractionTag",
                column: "AttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_OperatingHours_AttractionId",
                table: "OperatingHours",
                column: "AttractionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pricing_AttractionId",
                table: "Pricing",
                column: "AttractionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_AttractionId",
                table: "Review",
                column: "AttractionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttractionFeature");

            migrationBuilder.DropTable(
                name: "AttractionImage");

            migrationBuilder.DropTable(
                name: "AttractionTag");

            migrationBuilder.DropTable(
                name: "OperatingHours");

            migrationBuilder.DropTable(
                name: "Pricing");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Attractions");

            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8738), new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8738) });

            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8741), new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8741) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8638), new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8638) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8644), new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8644) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8647), new DateTime(2025, 5, 25, 11, 33, 2, 120, DateTimeKind.Utc).AddTicks(8647) });
        }
    }
}
