using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddServantActivityRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServantActivityRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChurchServiceAttendanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivityTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Answered = table.Column<bool>(type: "boolean", nullable: false),
                    PointsEarned = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServantActivityRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServantActivityRecords_ActivityTypes_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "ActivityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServantActivityRecords_ChurchServiceAttendances_ChurchServi~",
                        column: x => x.ChurchServiceAttendanceId,
                        principalTable: "ChurchServiceAttendances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServantActivityRecords_ActivityTypeId",
                table: "ServantActivityRecords",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServantActivityRecords_ChurchServiceAttendanceId_ActivityTy~",
                table: "ServantActivityRecords",
                columns: new[] { "ChurchServiceAttendanceId", "ActivityTypeId" },
                unique: true,
                filter: "\"DeletedAt\" IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServantActivityRecords");
        }
    }
}
