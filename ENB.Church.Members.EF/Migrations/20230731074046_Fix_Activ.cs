using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENB.Church.Members.EF.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Activ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberActivity_Activities_ActivityId",
                table: "MemberActivity");

            migrationBuilder.DropIndex(
                name: "IX_MemberActivity_ActivityId",
                table: "MemberActivity");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "381ed5ed-990c-4df1-87fd-643b6196da26");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69629134-3ed6-4665-8c15-8e41c94eabea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4d94fbb-3cc1-4b4c-b8c5-daf2e04ac0c5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1409826-2abe-46d6-aaa2-0a46c0fee9f2");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "MemberActivity");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "161a6cd4-3ac4-4144-926f-27c0869b280c", null, "Administrator", "ADMINISTRATOR" },
                    { "ac1a2287-512b-4596-a0dc-b6f496945f48", null, "Visitor", "VISITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "161a6cd4-3ac4-4144-926f-27c0869b280c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ce93446-6ed2-4ef2-8af8-081e2a4f596a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f5e13e0-615c-4152-83ed-ee7cd2edd904");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac1a2287-512b-4596-a0dc-b6f496945f48");

            migrationBuilder.AddColumn<int>(
                name: "ActivityId",
                table: "MemberActivity",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "381ed5ed-990c-4df1-87fd-643b6196da26", null, "Administrator", "ADMINISTRATOR" },
                    { "d1409826-2abe-46d6-aaa2-0a46c0fee9f2", null, "Visitor", "VISITOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberActivity_ActivityId",
                table: "MemberActivity",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberActivity_Activities_ActivityId",
                table: "MemberActivity",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");
        }
    }
}
