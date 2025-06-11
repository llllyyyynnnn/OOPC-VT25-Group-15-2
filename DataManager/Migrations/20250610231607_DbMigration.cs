using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Migrations
{
    /// <inheritdoc />
    public partial class DbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    specialisation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    pinCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    pinCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Gears",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    categoryid = table.Column<int>(type: "int", nullable: false),
                    condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    available = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gears", x => x.id);
                    table.ForeignKey(
                        name: "FK_Gears_Categories_categoryid",
                        column: x => x.categoryid,
                        principalTable: "Categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    startTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    memberid = table.Column<int>(type: "int", nullable: false),
                    coachid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Sessions_Coaches_coachid",
                        column: x => x.coachid,
                        principalTable: "Coaches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sessions_Members_memberid",
                        column: x => x.memberid,
                        principalTable: "Members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Gears_categoryid",
                table: "Gears",
                column: "categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_coachid",
                table: "Sessions",
                column: "coachid");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_memberid",
                table: "Sessions",
                column: "memberid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GearLoans");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Gears");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
