using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DATA.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DateOfStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Maps",
                columns: table => new
                {
                    MapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maps", x => x.MapId);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TeamRanking = table.Column<int>(type: "int", nullable: false),
                    TeamNationality = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "EventTeam",
                columns: table => new
                {
                    EventsEventId = table.Column<int>(type: "int", nullable: false),
                    TeamsTeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTeam", x => new { x.EventsEventId, x.TeamsTeamId });
                    table.ForeignKey(
                        name: "FK_EventTeam_Events_EventsEventId",
                        column: x => x.EventsEventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventTeam_Teams_TeamsTeamId",
                        column: x => x.TeamsTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    TeamAId = table.Column<int>(type: "int", nullable: false),
                    TeamBId = table.Column<int>(type: "int", nullable: false),
                    TeamWinnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Games_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Teams_TeamAId",
                        column: x => x.TeamAId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Teams_TeamBId",
                        column: x => x.TeamBId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Games_Teams_TeamWinnerId",
                        column: x => x.TeamWinnerId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MapOfGame",
                columns: table => new
                {
                    MapOfGameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamAResult = table.Column<int>(type: "int", nullable: true),
                    TeamBResult = table.Column<int>(type: "int", nullable: true),
                    MapaId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapOfGame", x => x.MapOfGameId);
                    table.ForeignKey(
                        name: "FK_MapOfGame_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MapOfGame_Maps_MapaId",
                        column: x => x.MapaId,
                        principalTable: "Maps",
                        principalColumn: "MapId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatPlayerOnMap",
                columns: table => new
                {
                    StatPlayerOnMapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kills = table.Column<int>(type: "int", nullable: false),
                    Deaths = table.Column<int>(type: "int", nullable: false),
                    ADR = table.Column<double>(type: "float", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatPlayerOnMap", x => x.StatPlayerOnMapId);
                    table.ForeignKey(
                        name: "FK_StatPlayerOnMap_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatPlayerOnMap_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatPlayerOnMap_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "DateOfEnd", "DateOfStart", "EventName" },
                values: new object[] { 1, new DateTime(2021, 1, 5, 1, 29, 38, 248, DateTimeKind.Local).AddTicks(1300), new DateTime(2020, 12, 31, 1, 29, 38, 245, DateTimeKind.Local).AddTicks(1367), "Blast New York" });

            migrationBuilder.InsertData(
                table: "Maps",
                columns: new[] { "MapId", "Description" },
                values: new object[,]
                {
                    { 1, "Inferno" },
                    { 2, "Vertigo" },
                    { 3, "Dust2" },
                    { 4, "Overpass" },
                    { 5, "Train" }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "PlayerId", "Age", "Facebook", "Instagram", "Name", "Nationality", "Nickname", "TeamId", "Twitter" },
                values: new object[,]
                {
                    { 5, 25, null, null, "Lukas Rossander", "Dinamarca", "gla1ve", null, null },
                    { 4, 27, null, null, "Peter Rasmussen", "Dinamarca", "dupreeh", null, null },
                    { 3, 25, null, null, "Nicolai Reedtz", "Dinamarca", "device", null, null },
                    { 2, 28, null, null, "Vito Giuseppe", "Brazil", "kNgV-", null, null },
                    { 1, 29, null, null, "Gabriel Toledo", "Brazil", "FalleN", null, null }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "TeamName", "TeamNationality", "TeamRanking" },
                values: new object[,]
                {
                    { 1, "Mibr", "Brazil", 21 },
                    { 2, "Astralis", "Dinamarca", 1 },
                    { 3, "Natus Vincere", "Russia", 3 },
                    { 4, "OG", "Europa", 7 },
                    { 5, "Mousesports", "Europa", 9 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "qwerty@qwerty.qwerty", "Admin", "Admin", "Admin" },
                    { 2, "qwerty@qwerty.qwerty", "User", "User", "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventTeam_TeamsTeamId",
                table: "EventTeam",
                column: "TeamsTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_EventId",
                table: "Games",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_TeamAId",
                table: "Games",
                column: "TeamAId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_TeamBId",
                table: "Games",
                column: "TeamBId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_TeamWinnerId",
                table: "Games",
                column: "TeamWinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_MapOfGame_GameId",
                table: "MapOfGame",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_MapOfGame_MapaId",
                table: "MapOfGame",
                column: "MapaId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_StatPlayerOnMap_GameId",
                table: "StatPlayerOnMap",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_StatPlayerOnMap_PlayerId",
                table: "StatPlayerOnMap",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_StatPlayerOnMap_TeamId",
                table: "StatPlayerOnMap",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventTeam");

            migrationBuilder.DropTable(
                name: "MapOfGame");

            migrationBuilder.DropTable(
                name: "StatPlayerOnMap");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Maps");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
