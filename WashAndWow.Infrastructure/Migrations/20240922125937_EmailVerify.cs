using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WashAndWow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmailVerify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailVerification",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpireTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailVerification", x => x.UserID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailVerification_Token",
                table: "EmailVerification",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailVerification_UserID",
                table: "EmailVerification",
                column: "UserID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailVerification");
        }
    }
}
