using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LastTodoApp.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class UserUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Tasks",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "237a0ff0-9d7c-4e0f-9926-89a1cb20f4cb", null, "MANAGER", "MANAGER" },
                    { "5d8b8310-68bc-4b26-a309-31fdc666ac6f", null, "ADMIN", "ADMIN" },
                    { "a639cb7e-9711-40c3-975a-b5b7f4f4013a", null, "USER", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_UserId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "237a0ff0-9d7c-4e0f-9926-89a1cb20f4cb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d8b8310-68bc-4b26-a309-31fdc666ac6f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a639cb7e-9711-40c3-975a-b5b7f4f4013a");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Tasks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

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
    }
}
