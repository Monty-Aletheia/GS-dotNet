using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace UserService.Infra.Migrations
{
	/// <inheritdoc />
	public partial class createdevicetable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "tb_devices",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					device_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					expo_device_token = table.Column<string>(type: "nvarchar(max)", nullable: false),
					user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_tb_devices", x => x.id);
					table.ForeignKey(
						name: "FK_tb_devices_tb_users_UserId",
						column: x => x.UserId,
						principalTable: "tb_users",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_tb_devices_tb_users_user_id",
						column: x => x.user_id,
						principalTable: "tb_users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_tb_devices_user_id",
				table: "tb_devices",
				column: "user_id");

			migrationBuilder.CreateIndex(
				name: "IX_tb_devices_UserId",
				table: "tb_devices",
				column: "UserId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "tb_devices");
		}
	}
}