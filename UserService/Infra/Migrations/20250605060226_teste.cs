using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Infra.Migrations
{
	/// <inheritdoc />
	public partial class teste : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "id",
				table: "tb_devices",
				newName: "Id");

			migrationBuilder.RenameColumn(
				name: "id",
				table: "tb_address",
				newName: "Id");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "Id",
				table: "tb_devices",
				newName: "id");

			migrationBuilder.RenameColumn(
				name: "Id",
				table: "tb_address",
				newName: "id");
		}
	}
}