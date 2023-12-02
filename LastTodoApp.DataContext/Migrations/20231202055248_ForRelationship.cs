using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LastTodoApp.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class ForRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a137b45-3cca-4661-9969-a6a241ed0221");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dbf4a41c-0d16-4b86-b06f-91f61a87a8a7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee34f091-2559-4d7b-9093-06172b89ff9f");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "024cfb3d-f3b4-4f98-96db-14be05a4c22b", null, "MANAGER", "MANAGER" },
                    { "b3b723d6-e9c0-4b76-909e-7467e977e81c", null, "USER", "USER" },
                    { "c61fac34-e89b-49de-ad4e-0070554a4f00", null, "ADMIN", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "024cfb3d-f3b4-4f98-96db-14be05a4c22b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3b723d6-e9c0-4b76-909e-7467e977e81c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c61fac34-e89b-49de-ad4e-0070554a4f00");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tasks");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5a137b45-3cca-4661-9969-a6a241ed0221", null, "USER", "USER" },
                    { "dbf4a41c-0d16-4b86-b06f-91f61a87a8a7", null, "ADMIN", "ADMIN" },
                    { "ee34f091-2559-4d7b-9093-06172b89ff9f", null, "MANAGER", "MANAGER" }
                });
        }
    }
}
