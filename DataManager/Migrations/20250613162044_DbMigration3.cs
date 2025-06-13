using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Migrations
{
    /// <inheritdoc />
    public partial class DbMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GearLoans_Gears_gearid",
                table: "GearLoans");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Sessions_Sessionid",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_Sessionid",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Sessionid",
                table: "Members");

            migrationBuilder.CreateTable(
                name: "MemberSession",
                columns: table => new
                {
                    membersid = table.Column<int>(type: "int", nullable: false),
                    sessionsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberSession", x => new { x.membersid, x.sessionsid });
                    table.ForeignKey(
                        name: "FK_MemberSession_Members_membersid",
                        column: x => x.membersid,
                        principalTable: "Members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberSession_Sessions_sessionsid",
                        column: x => x.sessionsid,
                        principalTable: "Sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberSession_sessionsid",
                table: "MemberSession",
                column: "sessionsid");

            migrationBuilder.AddForeignKey(
                name: "FK_GearLoans_Gears_gearid",
                table: "GearLoans",
                column: "gearid",
                principalTable: "Gears",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GearLoans_Gears_gearid",
                table: "GearLoans");

            migrationBuilder.DropTable(
                name: "MemberSession");

            migrationBuilder.AddColumn<int>(
                name: "Sessionid",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_Sessionid",
                table: "Members",
                column: "Sessionid");

            migrationBuilder.AddForeignKey(
                name: "FK_GearLoans_Gears_gearid",
                table: "GearLoans",
                column: "gearid",
                principalTable: "Gears",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Sessions_Sessionid",
                table: "Members",
                column: "Sessionid",
                principalTable: "Sessions",
                principalColumn: "id");
        }
    }
}
