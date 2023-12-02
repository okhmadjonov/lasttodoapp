using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LastTodoApp.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class UserUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Tasks",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b15f9da8-48dd-46a1-bac0-5d98602f91eb", null, "ADMIN", "ADMIN" },
                    { "b207d336-f552-45d7-b621-a27cb6e06ba4", null, "USER", "USER" },
                    { "b2daab43-9b3f-407a-a870-cb0ae669ebe5", null, "MANAGER", "MANAGER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId1",
                table: "Tasks",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_UserId1",
                table: "Tasks",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_UserId1",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UserId1",
                table: "Tasks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b15f9da8-48dd-46a1-bac0-5d98602f91eb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b207d336-f552-45d7-b621-a27cb6e06ba4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2daab43-9b3f-407a-a870-cb0ae669ebe5");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Tasks");

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
    }
}
