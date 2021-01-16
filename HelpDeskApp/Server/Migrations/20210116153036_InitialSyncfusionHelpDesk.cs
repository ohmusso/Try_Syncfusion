using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationsSyncfusionHelpDesk
{
    public partial class InitialSyncfusionHelpDesk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HelpDeskTickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TicketStatus = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    TicketDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TicketDescription = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    TicketRequesterEmail = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    TicketGUID = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpDeskTickets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HelpDeskTicketDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HelpDeskTicketId = table.Column<int>(type: "INTEGER", nullable: false),
                    TicketDetailDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TicketDescription = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpDeskTicketDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HelpDeskTicketDetails_HelpDeskTickets",
                        column: x => x.HelpDeskTicketId,
                        principalTable: "HelpDeskTickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HelpDeskTicketDetails_HelpDeskTicketId",
                table: "HelpDeskTicketDetails",
                column: "HelpDeskTicketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HelpDeskTicketDetails");

            migrationBuilder.DropTable(
                name: "HelpDeskTickets");
        }
    }
}
