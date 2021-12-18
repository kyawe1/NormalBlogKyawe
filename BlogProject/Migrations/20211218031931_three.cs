using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Migrations
{
    public partial class three : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_blogs_AspNetUsers_User_id",
                table: "blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_AspNetUsers_UserId",
                table: "comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4512509b-e908-4b3e-a27e-82291df44172");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ecc6844-bb74-44ef-8e43-846920bae3e9");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "comments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "User_id",
                table: "blogs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2b730631-3c3f-4b28-8798-6dab592badf4", "8dc851f9-7edd-4f30-8ce8-b39bd41c1599", "AdminStrator", "ADMINSTRATOR" },
                    { "fedacdc0-ea26-4c95-a86e-7f05ae6e856a", "d1b7300a-12a9-4f53-8e32-af8b59390d2d", "Normal", "NORMAL" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_blogs_AspNetUsers_User_id",
                table: "blogs",
                column: "User_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_AspNetUsers_UserId",
                table: "comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_blogs_AspNetUsers_User_id",
                table: "blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_AspNetUsers_UserId",
                table: "comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b730631-3c3f-4b28-8798-6dab592badf4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fedacdc0-ea26-4c95-a86e-7f05ae6e856a");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "comments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "User_id",
                table: "blogs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4512509b-e908-4b3e-a27e-82291df44172", "9a8fc6cd-4797-468d-919e-115684db8487", "Normal", "NORMAL" },
                    { "6ecc6844-bb74-44ef-8e43-846920bae3e9", "ef9bdca8-1b10-4330-b993-7bcc987bffc8", "AdminStrator", "ADMINSTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_blogs_AspNetUsers_User_id",
                table: "blogs",
                column: "User_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_AspNetUsers_UserId",
                table: "comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
