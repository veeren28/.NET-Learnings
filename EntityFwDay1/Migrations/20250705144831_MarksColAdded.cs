using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFwDay1.Migrations
{
    /// <inheritdoc />
    public partial class MarksColAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "students",
                newName: "Rollno");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rollno",
                table: "students",
                newName: "Id");
        }
    }
}
