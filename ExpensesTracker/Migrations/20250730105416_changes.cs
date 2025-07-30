using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensesTracker.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Transactions_TransactionId",
                table: "Expenses");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Transactions_TransactionId",
                table: "Expenses",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Transactions_TransactionId",
                table: "Expenses");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Transactions_TransactionId",
                table: "Expenses",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }
    }
}
