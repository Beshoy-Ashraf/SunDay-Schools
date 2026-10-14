using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSessionsAndActivityTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChurchServiceAttendances_ChurchServiceSessions_ChurchServic~",
                table: "ChurchServiceAttendances");

            migrationBuilder.DropTable(
                name: "ChurchServiceSessions");

            migrationBuilder.DropTable(
                name: "ServantActivityRecords");

            migrationBuilder.DropTable(
                name: "ActivityTypes");

            migrationBuilder.DropIndex(
                name: "IX_ChurchServiceAttendances_ChurchServiceSessionId_ChurchMembe~",
                table: "ChurchServiceAttendances");

            migrationBuilder.RenameColumn(
                name: "ChurchServiceSessionId",
                table: "ChurchServiceAttendances",
                newName: "ChurchServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ChurchServiceAttendances_ChurchServiceId_ChurchMemberId_Att~",
                table: "ChurchServiceAttendances",
                columns: new[] { "ChurchServiceId", "ChurchMemberId", "AttendedAt" });

            migrationBuilder.AddForeignKey(
                name: "FK_ChurchServiceAttendances_ChurchServices_ChurchServiceId",
                table: "ChurchServiceAttendances",
                column: "ChurchServiceId",
                principalTable: "ChurchServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChurchServiceAttendances_ChurchServices_ChurchServiceId",
                table: "ChurchServiceAttendances");

            migrationBuilder.DropIndex(
                name: "IX_ChurchServiceAttendances_ChurchServiceId_ChurchMemberId_Att~",
                table: "ChurchServiceAttendances");

            migrationBuilder.RenameColumn(
                name: "ChurchServiceId",
                table: "ChurchServiceAttendances",
                newName: "ChurchServiceSessionId");

            migrationBuilder.CreateTable(
                name: "ActivityTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChurchServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppliesTo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Code = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    MaxPerMonth = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityTypes_ChurchServices_ChurchServiceId",
                        column: x => x.ChurchServiceId,
                        principalTable: "ChurchServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChurchServiceSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChurchServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChurchServiceSessions", x => x.Id);
                    table.CheckConstraint("CK_ChurchServiceSessions_EndTime_After_StartTime", "\"EndTime\" > \"StartTime\"");
                    table.ForeignKey(
                        name: "FK_ChurchServiceSessions_ChurchServices_ChurchServiceId",
                        column: x => x.ChurchServiceId,
                        principalTable: "ChurchServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServantActivityRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivityTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChurchServiceAttendanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Answered = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PointsEarned = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                name: "IX_ChurchServiceAttendances_ChurchServiceSessionId_ChurchMembe~",
                table: "ChurchServiceAttendances",
                columns: new[] { "ChurchServiceSessionId", "ChurchMemberId" },
                unique: true,
                filter: "\"DeletedAt\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTypes_ChurchServiceId_Code",
                table: "ActivityTypes",
                columns: new[] { "ChurchServiceId", "Code" },
                unique: true,
                filter: "\"DeletedAt\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ChurchServiceSessions_ChurchServiceId_StartTime",
                table: "ChurchServiceSessions",
                columns: new[] { "ChurchServiceId", "StartTime" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_ChurchServiceAttendances_ChurchServiceSessions_ChurchServic~",
                table: "ChurchServiceAttendances",
                column: "ChurchServiceSessionId",
                principalTable: "ChurchServiceSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
