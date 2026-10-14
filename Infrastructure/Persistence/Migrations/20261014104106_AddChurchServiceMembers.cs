using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddChurchServiceMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChurchServiceMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChurchServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChurchMemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChurchServiceMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChurchServiceMembers_ChurchMembers_ChurchMemberId",
                        column: x => x.ChurchMemberId,
                        principalTable: "ChurchMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChurchServiceMembers_ChurchServices_ChurchServiceId",
                        column: x => x.ChurchServiceId,
                        principalTable: "ChurchServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChurchServiceMembers_ChurchMemberId",
                table: "ChurchServiceMembers",
                column: "ChurchMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ChurchServiceMembers_ChurchServiceId_ChurchMemberId",
                table: "ChurchServiceMembers",
                columns: new[] { "ChurchServiceId", "ChurchMemberId" },
                unique: true,
                filter: "\"DeletedAt\" IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChurchServiceMembers");
        }
    }
}
