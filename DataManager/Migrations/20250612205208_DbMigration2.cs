using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Migrations
{
    /// <inheritdoc />
    public partial class DbMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GearLoans_Gears_gearid",
                table: "GearLoans");

            migrationBuilder.AddColumn<bool>(
                name: "paymentStatus",
                table: "Members",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_GearLoans_Gears_gearid",
                table: "GearLoans",
                column: "gearid",
                principalTable: "Gears",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GearLoans_Gears_gearid",
                table: "GearLoans");

            migrationBuilder.DropColumn(
                name: "paymentStatus",
                table: "Members");

            migrationBuilder.AddForeignKey(
                name: "FK_GearLoans_Gears_gearid",
                table: "GearLoans",
                column: "gearid",
                principalTable: "Gears",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
