using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShitWithFriendAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_MouthPoopKing",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_MouthPoopKing",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "MouthPoopKing",
                table: "Groups");

            migrationBuilder.AlterColumn<Guid>(
                name: "YearPoopKing",
                table: "Groups",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "MonthPoopKing",
                table: "Groups",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_MonthPoopKing",
                table: "Groups",
                column: "MonthPoopKing");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_MonthPoopKing",
                table: "Groups",
                column: "MonthPoopKing",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_MonthPoopKing",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_MonthPoopKing",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "MonthPoopKing",
                table: "Groups");

            migrationBuilder.AlterColumn<Guid>(
                name: "YearPoopKing",
                table: "Groups",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MouthPoopKing",
                table: "Groups",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Groups_MouthPoopKing",
                table: "Groups",
                column: "MouthPoopKing");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_MouthPoopKing",
                table: "Groups",
                column: "MouthPoopKing",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
