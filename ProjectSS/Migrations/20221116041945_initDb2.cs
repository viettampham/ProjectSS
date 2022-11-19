using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectSS.Migrations
{
    public partial class initDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Cartid",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Cartid",
                table: "AspNetUsers",
                column: "Cartid");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Carts_Cartid",
                table: "AspNetUsers",
                column: "Cartid",
                principalTable: "Carts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Carts_Cartid",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Cartid",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Cartid",
                table: "AspNetUsers");
        }
    }
}
