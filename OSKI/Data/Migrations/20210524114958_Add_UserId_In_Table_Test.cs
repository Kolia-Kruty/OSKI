using Microsoft.EntityFrameworkCore.Migrations;

namespace OSKI.Data.Migrations
{
    public partial class Add_UserId_In_Table_Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Tests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_ApplicationUserId",
                table: "Tests",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_AspNetUsers_ApplicationUserId",
                table: "Tests",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_AspNetUsers_ApplicationUserId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_ApplicationUserId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Tests");
        }
    }
}
