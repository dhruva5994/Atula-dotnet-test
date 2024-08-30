using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticalDemo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LoginViewModel",
                table: "LoginViewModel");

            migrationBuilder.RenameTable(
                name: "LoginViewModel",
                newName: "loginViewModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_loginViewModels",
                table: "loginViewModels",
                column: "LoginID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_loginViewModels",
                table: "loginViewModels");

            migrationBuilder.RenameTable(
                name: "loginViewModels",
                newName: "LoginViewModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoginViewModel",
                table: "LoginViewModel",
                column: "LoginID");
        }
    }
}
