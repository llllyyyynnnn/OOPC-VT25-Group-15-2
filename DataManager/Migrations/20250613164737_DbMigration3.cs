using System;
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
            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    mailAddress = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    birthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pinCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    specialisation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Gears",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    condition = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    available = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gears", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    mailAddress = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    birthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pinCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    paymentStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    activity = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    caloriesBurnt = table.Column<int>(type: "int", nullable: false),
                    participants = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    time = table.Column<TimeOnly>(type: "time", nullable: false),
                    location = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Coachid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Sessions_Coaches_Coachid",
                        column: x => x.Coachid,
                        principalTable: "Coaches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GearLoans",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gearid = table.Column<int>(type: "int", nullable: false),
                    loanOwnerid = table.Column<int>(type: "int", nullable: false),
                    loanDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    returnDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearLoans", x => x.id);
                    table.ForeignKey(
                        name: "FK_GearLoans_Gears_gearid",
                        column: x => x.gearid,
                        principalTable: "Gears",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GearLoans_Members_loanOwnerid",
                        column: x => x.loanOwnerid,
                        principalTable: "Members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_GearLoans_gearid",
                table: "GearLoans",
                column: "gearid");

            migrationBuilder.CreateIndex(
                name: "IX_GearLoans_loanOwnerid",
                table: "GearLoans",
                column: "loanOwnerid");

            migrationBuilder.CreateIndex(
                name: "IX_MemberSession_sessionsid",
                table: "MemberSession",
                column: "sessionsid");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Coachid",
                table: "Sessions",
                column: "Coachid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GearLoans");

            migrationBuilder.DropTable(
                name: "MemberSession");

            migrationBuilder.DropTable(
                name: "Gears");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Coaches");
        }
    }
}
