using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShitWithFriendAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAvatarColumn_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "DEFAULT_1");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    IsSecret = table.Column<bool>(type: "INTEGER", nullable: false),
                    XpValue = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "UserAchievements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AchievementCode = table.Column<string>(type: "TEXT", nullable: false),
                    UnlockedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
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
                name: "IX_UserAchievements_AchievementCode",
                table: "UserAchievements",
                column: "AchievementCode");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_UserId",
                table: "UserAchievements",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAchievements");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");
        }
    }
}
