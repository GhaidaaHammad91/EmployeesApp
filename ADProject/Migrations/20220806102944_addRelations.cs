using Microsoft.EntityFrameworkCore.Migrations;

namespace ADProject.Migrations
{
    public partial class addRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refreshtoken_Users_UserId",
                table: "Refreshtoken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Refreshtoken",
                table: "Refreshtoken");

            migrationBuilder.RenameTable(
                name: "Refreshtoken",
                newName: "refreshtokens");

            migrationBuilder.RenameIndex(
                name: "IX_Refreshtoken_UserId",
                table: "refreshtokens",
                newName: "IX_refreshtokens_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_refreshtokens",
                table: "refreshtokens",
                column: "TokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_refreshtokens_Users_UserId",
                table: "refreshtokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refreshtokens_Users_UserId",
                table: "refreshtokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_refreshtokens",
                table: "refreshtokens");

            migrationBuilder.RenameTable(
                name: "refreshtokens",
                newName: "Refreshtoken");

            migrationBuilder.RenameIndex(
                name: "IX_refreshtokens_UserId",
                table: "Refreshtoken",
                newName: "IX_Refreshtoken_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Refreshtoken",
                table: "Refreshtoken",
                column: "TokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Refreshtoken_Users_UserId",
                table: "Refreshtoken",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
