using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensesTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddedTrasactionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Income_Category_CategoryId",
                table: "Income");

            migrationBuilder.DropIndex(
                name: "IX_Income_CategoryId",
                table: "Income");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Income");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Income",
                newName: "TransactionId");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Income",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Income_TransactionId",
                table: "Income",
                column: "TransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_TransactionId",
                table: "Expenses",
                column: "TransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserID",
                table: "Transaction",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Transaction_TransactionId",
                table: "Expenses",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Income_Transaction_TransactionId",
                table: "Income",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Transaction_TransactionId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Income_Transaction_TransactionId",
                table: "Income");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Income_TransactionId",
                table: "Income");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_TransactionId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Income");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Income",
                newName: "CategoryId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Income",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Expenses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Income_CategoryId",
                table: "Income",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Income_Category_CategoryId",
                table: "Income",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
