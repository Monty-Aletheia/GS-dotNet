using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Infra.Migrations
{
    /// <inheritdoc />
    public partial class initialdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    password = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    firebase_id = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_address",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    street = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    number = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    neighborhood = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    city = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    state = table.Column<string>(type: "NVARCHAR2(2)", maxLength: 2, nullable: false),
                    user_id = table.Column<Guid>(type: "RAW(16)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "tb_devices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    device_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    expo_device_token = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    user_id = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_devices", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_devices_tb_users_user_id",
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

            migrationBuilder.CreateIndex(
                name: "IX_tb_devices_user_id",
                table: "tb_devices",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_address");

            migrationBuilder.DropTable(
                name: "tb_devices");

            migrationBuilder.DropTable(
                name: "tb_users");
        }
    }
}
