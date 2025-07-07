using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameWorkPract.Migrations
{
    /// <inheritdoc />
    public partial class AddedMarks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Marks",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marks",
                table: "Students");
        }
    }
}
