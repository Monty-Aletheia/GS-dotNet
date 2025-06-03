using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Infra.Migrations
{
    /// <inheritdoc />
    public partial class seila : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "firebase_id",
                table: "tb_users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "firebase_id",
                table: "tb_users");
        }
    }
}
