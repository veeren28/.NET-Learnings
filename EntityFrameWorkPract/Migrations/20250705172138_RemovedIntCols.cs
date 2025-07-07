using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameWorkPract.Migrations
{
    /// <inheritdoc />
    public partial class RemovedIntCols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Students",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Students",
                newName: "StudentId");
        }
    }
}
