using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace UserService.Infra.Migrations
{
	/// <inheritdoc />
	public partial class createaddresstable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "tb_address",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					street = table.Column<string>(type: "nvarchar(max)", nullable: false),
					number = table.Column<string>(type: "nvarchar(max)", nullable: false),
					neighborhood = table.Column<string>(type: "nvarchar(max)", nullable: false),
					city = table.Column<string>(type: "nvarchar(max)", nullable: false),
					state = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
					user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_tb_address", x => x.id);
					table.ForeignKey(
						name: "FK_tb_address_tb_users_user_id",
						column: x => x.user_id,
						principalTable: "tb_users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_tb_address_user_id",
				table: "tb_address",
				column: "user_id",
				unique: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "tb_address");
		}
	}
}