using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InitProjectVerticalSlice.Migrations
{
    /// <inheritdoc />
    public partial class AuthUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Auths",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Auths_UserId",
                table: "Auths",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auths_Users_UserId",
                table: "Auths",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auths_Users_UserId",
                table: "Auths");

            migrationBuilder.DropIndex(
                name: "IX_Auths_UserId",
                table: "Auths");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Auths");
        }
    }
}
