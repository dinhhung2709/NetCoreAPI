using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstWebMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddExportReceipt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "ExportReceipts",
                newName: "ExportDate");

            migrationBuilder.CreateIndex(
                name: "IX_ImportReceiptDetails_DeviceId",
                table: "ImportReceiptDetails",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportReceiptDetails_DeviceId",
                table: "ExportReceiptDetails",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportReceiptDetails_ExportReceiptId",
                table: "ExportReceiptDetails",
                column: "ExportReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExportReceiptDetails_Devices_DeviceId",
                table: "ExportReceiptDetails",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExportReceiptDetails_ExportReceipts_ExportReceiptId",
                table: "ExportReceiptDetails",
                column: "ExportReceiptId",
                principalTable: "ExportReceipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportReceiptDetails_Devices_DeviceId",
                table: "ImportReceiptDetails",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExportReceiptDetails_Devices_DeviceId",
                table: "ExportReceiptDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ExportReceiptDetails_ExportReceipts_ExportReceiptId",
                table: "ExportReceiptDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportReceiptDetails_Devices_DeviceId",
                table: "ImportReceiptDetails");

            migrationBuilder.DropIndex(
                name: "IX_ImportReceiptDetails_DeviceId",
                table: "ImportReceiptDetails");

            migrationBuilder.DropIndex(
                name: "IX_ExportReceiptDetails_DeviceId",
                table: "ExportReceiptDetails");

            migrationBuilder.DropIndex(
                name: "IX_ExportReceiptDetails_ExportReceiptId",
                table: "ExportReceiptDetails");

            migrationBuilder.RenameColumn(
                name: "ExportDate",
                table: "ExportReceipts",
                newName: "DateCreated");
        }
    }
}
