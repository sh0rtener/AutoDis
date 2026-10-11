using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoDis.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCarModelName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "vin_code",
                table: "car",
                newName: "model");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "model",
                table: "car",
                newName: "vin_code");
        }
    }
}
