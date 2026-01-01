using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShitWithFriendAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsSecret = table.Column<bool>(type: "boolean", nullable: false),
                    XpValue = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    YearPoopKing = table.Column<Guid>(type: "uuid", nullable: true),
                    MonthPoopKing = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Users_MonthPoopKing",
                        column: x => x.MonthPoopKing,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Groups_Users_YearPoopKing",
                        column: x => x.YearPoopKing,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HighScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    GameId = table.Column<Guid>(type: "uuid", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HighScores_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HighScores_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Poops",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TypeOfPoop = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Poops_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAchievements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AchievementCode = table.Column<string>(type: "text", nullable: false),
                    UnlockedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAchievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAchievements_Achievements_AchievementCode",
                        column: x => x.AchievementCode,
                        principalTable: "Achievements",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAchievements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupAdministrators",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupAdministrators", x => new { x.GroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_GroupAdministrators_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupAdministrators_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupUsers",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUsers", x => new { x.GroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_GroupUsers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersGroupsEmoji",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    Emoji = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersGroupsEmoji", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersGroupsEmoji_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersGroupsEmoji_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Code", "Description", "IsSecret", "Name", "XpValue" },
                values: new object[,]
                {
                    { "DEFAULT_1", "L'originale.", false, "Classica", 0 },
                    { "DEFAULT_2", "Distinta ed elegante.", false, "Il Signore", 0 },
                    { "DEFAULT_3", "Fatta con amore.", false, "Love", 0 },
                    { "EARLY_BIRD", "Posta tra le 05:00 e le 07:00.", false, "Il Gallo", 20 },
                    { "FERRAGOSTO", "Posta il 15 Agosto.", false, "Cacca Caliente", 20 },
                    { "GAMER_COMPLETIONIST", "Gioca a tutti i minigiochi.", false, "Nerd da Bagno", 100 },
                    { "GAMER_ROOKIE", "Gioca la tua prima partita.", false, "Insert Coin", 10 },
                    { "HALLOWEEN", "Posta il 31 Ottobre.", true, "Terrore in Bagno", 30 },
                    { "KING_MONTH", "Vinci un titolo di Re del Mese.", false, "Barone del Trono", 100 },
                    { "KING_YEAR", "Vinci un titolo di Re dell'Anno.", false, "Imperatore Supremo", 500 },
                    { "LUNCH_TIMER", "Posta tra le 12:30 e le 14:00.", false, "Pausa Pranzo", 15 },
                    { "NEW_YEAR", "Posta il 1° Gennaio.", false, "Anno Nuovo", 50 },
                    { "NIGHT_OWL", "Posta tra le 03:00 e le 05:00.", true, "Il Vampiro", 50 },
                    { "POOP_1", "La tua prima cacca registrata.", false, "Il Primo Passo", 10 },
                    { "POOP_10", "10 cacche registrate.", false, "Riscaldamento", 20 },
                    { "POOP_100", "100 cacche. Un esercito.", false, "Il Centurione", 100 },
                    { "POOP_1000", "1000 cacche. Leggendario.", true, "Divinità Intestinale", 1000 },
                    { "POOP_25", "25 cacche registrate.", false, "Apprendista", 30 },
                    { "POOP_250", "250 cacche. Arte pura.", false, "Maestro Zen", 250 },
                    { "POOP_50", "50 cacche. Rispetto.", false, "Cintura Marrone", 50 },
                    { "POOP_500", "500 cacche. Regale.", true, "Il Trono di Ceramica", 500 },
                    { "STREAK_3", "3 giorni consecutivi.", false, "Tris", 20 },
                    { "STREAK_30", "30 giorni consecutivi.", true, "Iron Man", 150 },
                    { "STREAK_7", "7 giorni consecutivi.", false, "Settimana Santa", 50 },
                    { "TURBO_POOPER", "2 cacche in meno di 1 ora.", true, "Mitragliatrice", 50 },
                    { "TYPE_LIQUID", "5 cacche di tipo Liquida.", false, "Idrante", 30 },
                    { "VALENTINE", "Posta il 14 Febbraio.", false, "Innamorato", 20 },
                    { "WEEKEND_WARRIOR", "Cacca sia Sabato che Domenica nello stesso weekend.", false, "Guerriero del Weekend", 30 },
                    { "XMAS", "Posta il 25 Dicembre.", true, "Regalino di Natale", 50 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupAdministrators_UserId",
                table: "GroupAdministrators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_MonthPoopKing",
                table: "Groups",
                column: "MonthPoopKing");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_YearPoopKing",
                table: "Groups",
                column: "YearPoopKing");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_UserId",
                table: "GroupUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HighScores_GameId",
                table: "HighScores",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_HighScores_UserId",
                table: "HighScores",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Poops_UserId",
                table: "Poops",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_AchievementCode",
                table: "UserAchievements",
                column: "AchievementCode");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_UserId",
                table: "UserAchievements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersGroupsEmoji_GroupId",
                table: "UsersGroupsEmoji",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersGroupsEmoji_UserId",
                table: "UsersGroupsEmoji",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupAdministrators");

            migrationBuilder.DropTable(
                name: "GroupUsers");

            migrationBuilder.DropTable(
                name: "HighScores");

            migrationBuilder.DropTable(
                name: "Poops");

            migrationBuilder.DropTable(
                name: "UserAchievements");

            migrationBuilder.DropTable(
                name: "UsersGroupsEmoji");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
