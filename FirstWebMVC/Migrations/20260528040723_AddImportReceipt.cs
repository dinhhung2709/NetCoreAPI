using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstWebMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddImportReceipt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "ImportReceipts",
                newName: "ImportDate");

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "ImportReceipts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ImportReceipts_SupplierId",
                table: "ImportReceipts",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportReceiptDetails_ImportReceiptId",
                table: "ImportReceiptDetails",
                column: "ImportReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImportReceiptDetails_ImportReceipts_ImportReceiptId",
                table: "ImportReceiptDetails",
                column: "ImportReceiptId",
                principalTable: "ImportReceipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportReceipts_Suppliers_SupplierId",
                table: "ImportReceipts",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImportReceiptDetails_ImportReceipts_ImportReceiptId",
                table: "ImportReceiptDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportReceipts_Suppliers_SupplierId",
                table: "ImportReceipts");

            migrationBuilder.DropIndex(
                name: "IX_ImportReceipts_SupplierId",
                table: "ImportReceipts");

            migrationBuilder.DropIndex(
                name: "IX_ImportReceiptDetails_ImportReceiptId",
                table: "ImportReceiptDetails");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "ImportReceipts");

            migrationBuilder.RenameColumn(
                name: "ImportDate",
                table: "ImportReceipts",
                newName: "DateCreated");
        }
    }
}
