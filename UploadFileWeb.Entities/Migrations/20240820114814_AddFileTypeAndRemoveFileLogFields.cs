using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UploadFileWeb.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddFileTypeAndRemoveFileLogFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileLogs_Transactions_TransactionId",
                table: "FileLogs");

            migrationBuilder.DropIndex(
                name: "IX_FileLogs_TransactionId",
                table: "FileLogs");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "FileLogs");

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Transactions",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "FileLogs",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FileLogs_TransactionId",
                table: "FileLogs",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileLogs_Transactions_TransactionId",
                table: "FileLogs",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "TransactionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
