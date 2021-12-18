using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BlogProject.Migrations
{
    public partial class four : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b730631-3c3f-4b28-8798-6dab592badf4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fedacdc0-ea26-4c95-a86e-7f05ae6e856a");

            migrationBuilder.CreateTable(
                name: "saveBlogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    BlogId = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_saveBlogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_saveBlogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_saveBlogs_blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "65626073-f355-448c-9c0f-ce4a96ebe208", "bc38f938-f560-438d-b0fb-a25179dd493c", "AdminStrator", "ADMINSTRATOR" },
                    { "d6104eb0-58f2-4598-a742-1cb9c29f2f6c", "1a410511-55ae-4457-9178-05bac2cbce49", "Normal", "NORMAL" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_saveBlogs_BlogId",
                table: "saveBlogs",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_saveBlogs_UserId",
                table: "saveBlogs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "saveBlogs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65626073-f355-448c-9c0f-ce4a96ebe208");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6104eb0-58f2-4598-a742-1cb9c29f2f6c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2b730631-3c3f-4b28-8798-6dab592badf4", "8dc851f9-7edd-4f30-8ce8-b39bd41c1599", "AdminStrator", "ADMINSTRATOR" },
                    { "fedacdc0-ea26-4c95-a86e-7f05ae6e856a", "d1b7300a-12a9-4f53-8e32-af8b59390d2d", "Normal", "NORMAL" }
                });
        }
    }
}
