using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArasvaAssignment.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BookCopyUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "BookCopys");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "BookCopys",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
