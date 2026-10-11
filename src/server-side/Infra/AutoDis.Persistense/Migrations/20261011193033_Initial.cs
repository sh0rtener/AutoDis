using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AutoDis.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "car_country",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    iso_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car_country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "car_manufacturer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car_manufacturer", x => x.id);
                    table.ForeignKey(
                        name: "FK_car_manufacturer_car_country_country_id",
                        column: x => x.country_id,
                        principalTable: "car_country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "car",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vin_code = table.Column<string>(type: "text", nullable: false),
                    manufacturer_id = table.Column<int>(type: "integer", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    color_type = table.Column<int>(type: "integer", nullable: false),
                    body_type = table.Column<int>(type: "integer", nullable: false),
                    fuel_type = table.Column<int>(type: "integer", nullable: false),
                    transmission_type = table.Column<int>(type: "integer", nullable: false),
                    drive_train = table.Column<int>(type: "integer", nullable: false),
                    power = table.Column<int>(type: "integer", nullable: false),
                    doors = table.Column<int>(type: "integer", nullable: false),
                    seats = table.Column<int>(type: "integer", nullable: false),
                    mileage = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car", x => x.id);
                    table.ForeignKey(
                        name: "FK_car_car_manufacturer_manufacturer_id",
                        column: x => x.manufacturer_id,
                        principalTable: "car_manufacturer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "car_bill",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cost = table.Column<decimal>(type: "numeric", nullable: false),
                    is_archieve = table.Column<bool>(type: "boolean", nullable: false),
                    car_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car_bill", x => x.id);
                    table.ForeignKey(
                        name: "FK_car_bill_car_car_id",
                        column: x => x.car_id,
                        principalTable: "car",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "car_bill_images",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    car_bill_id = table.Column<int>(type: "integer", nullable: false),
                    image_path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car_bill_images", x => x.id);
                    table.ForeignKey(
                        name: "FK_car_bill_images_car_bill_car_bill_id",
                        column: x => x.car_bill_id,
                        principalTable: "car_bill",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "car_bill_notes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    cost = table.Column<decimal>(type: "numeric", nullable: false),
                    available = table.Column<bool>(type: "boolean", nullable: false),
                    car_bill_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car_bill_notes", x => x.id);
                    table.ForeignKey(
                        name: "FK_car_bill_notes_car_bill_car_bill_id",
                        column: x => x.car_bill_id,
                        principalTable: "car_bill",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_car_manufacturer_id",
                table: "car",
                column: "manufacturer_id");

            migrationBuilder.CreateIndex(
                name: "IX_car_bill_car_id",
                table: "car_bill",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "IX_car_bill_images_car_bill_id",
                table: "car_bill_images",
                column: "car_bill_id");

            migrationBuilder.CreateIndex(
                name: "IX_car_bill_notes_car_bill_id",
                table: "car_bill_notes",
                column: "car_bill_id");

            migrationBuilder.CreateIndex(
                name: "IX_car_manufacturer_country_id",
                table: "car_manufacturer",
                column: "country_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "car_bill_images");

            migrationBuilder.DropTable(
                name: "car_bill_notes");

            migrationBuilder.DropTable(
                name: "car_bill");

            migrationBuilder.DropTable(
                name: "car");

            migrationBuilder.DropTable(
                name: "car_manufacturer");

            migrationBuilder.DropTable(
                name: "car_country");
        }
    }
}
