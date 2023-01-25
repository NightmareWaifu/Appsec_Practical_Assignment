using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppsecAssignment.Migrations
{
    /// <inheritdoc />
    public partial class addingLoginStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "loggedIn",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "loggedIn",
                table: "AspNetUsers");
        }
    }
}
