using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WashAndWow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NonRequiredBookingVoucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Voucher_VoucherID",
                table: "Booking");

            migrationBuilder.AlterColumn<string>(
                name: "VoucherID",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Voucher_VoucherID",
                table: "Booking",
                column: "VoucherID",
                principalTable: "Voucher",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Voucher_VoucherID",
                table: "Booking");

            migrationBuilder.AlterColumn<string>(
                name: "VoucherID",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Voucher_VoucherID",
                table: "Booking",
                column: "VoucherID",
                principalTable: "Voucher",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
