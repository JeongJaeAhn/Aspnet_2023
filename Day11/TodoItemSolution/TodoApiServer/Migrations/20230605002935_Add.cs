using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApiServer.Migrations
{
    /// <inheritdoc />
    public partial class Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TodoItems",
                keyColumn: "TodoDate",
                keyValue: null,
                column: "TodoDate",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TodoDate",
                table: "TodoItems",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "IsComplete",
                table: "TodoItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TodoDate",
                table: "TodoItems",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "IsComplete",
                table: "TodoItems",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
