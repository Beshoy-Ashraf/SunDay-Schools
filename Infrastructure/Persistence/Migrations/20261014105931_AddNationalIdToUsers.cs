using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNationalIdToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NationalId",
                table: "Users",
                type: "character varying(14)",
                maxLength: 14,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_NationalId",
                table: "Users",
                column: "NationalId",
                unique: true,
                filter: "\"NationalId\" IS NOT NULL AND \"DeletedAt\" IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_NationalId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NationalId",
                table: "Users");
        }
    }
}
