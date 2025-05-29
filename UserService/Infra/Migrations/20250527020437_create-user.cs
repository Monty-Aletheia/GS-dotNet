using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace UserService.Infra.Migrations
{
	/// <inheritdoc />
	public partial class createuser : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "tb_users",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					email = table.Column<string>(type: "nvarchar(max)", nullable: false),
					password = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_tb_users", x => x.Id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "tb_users");
		}
	}
}