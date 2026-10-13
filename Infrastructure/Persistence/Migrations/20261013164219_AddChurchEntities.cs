using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddChurchEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Hand-edited: EF scaffolded drop/add and mismatched renames here, which would lose data.

            // Users: rename audit columns to the BaseEntity names, keeping existing values.
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "Users",
                newName: "DeletedAt");

            // User now stores email lowercased and username trimmed, and lookups are normalized the same way.
            migrationBuilder.Sql("UPDATE \"Users\" SET \"Email\" = lower(trim(\"Email\")), \"Username\" = trim(\"Username\");");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "character varying(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            // RefreshTokens: rows without a token can never be used.
            migrationBuilder.Sql("DELETE FROM \"RefreshTokens\" WHERE \"Token\" IS NULL;");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "RefreshTokens",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpiryTime",
                table: "RefreshTokens",
                newName: "ExpiresAt");

            migrationBuilder.Sql("UPDATE \"RefreshTokens\" SET \"ExpiresAt\" = now() WHERE \"ExpiresAt\" IS NULL;");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiresAt",
                table: "RefreshTokens",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            // IsRevoked (bool) becomes RevokedAt (timestamp); the original revoke time is unknown, so use now.
            migrationBuilder.AddColumn<DateTime>(
                name: "RevokedAt",
                table: "RefreshTokens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.Sql("UPDATE \"RefreshTokens\" SET \"RevokedAt\" = now() WHERE \"IsRevoked\";");

            migrationBuilder.DropColumn(
                name: "IsRevoked",
                table: "RefreshTokens");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RefreshTokens",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.Sql("UPDATE \"RefreshTokens\" SET \"CreatedAt\" = now();");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "RefreshTokens",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Eparchies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eparchies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Churches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    EparchyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Churches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Churches_Eparchies_EparchyId",
                        column: x => x.EparchyId,
                        principalTable: "Eparchies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChurchMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChurchId = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    DateJoined = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateLeft = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChurchMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChurchMembers_Churches_ChurchId",
                        column: x => x.ChurchId,
                        principalTable: "Churches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChurchMembers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChurchServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChurchId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChurchServices", x => x.Id);
                    table.CheckConstraint("CK_ChurchServices_EndTime_After_StartTime", "\"EndTime\" > \"StartTime\"");
                    table.ForeignKey(
                        name: "FK_ChurchServices_Churches_ChurchId",
                        column: x => x.ChurchId,
                        principalTable: "Churches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChurchServiceSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChurchServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                name: "ChurchServiceAttendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChurchServiceSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChurchMemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordedById = table.Column<Guid>(type: "uuid", nullable: false),
                    AttendedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChurchServiceAttendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChurchServiceAttendances_ChurchMembers_ChurchMemberId",
                        column: x => x.ChurchMemberId,
                        principalTable: "ChurchMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChurchServiceAttendances_ChurchMembers_RecordedById",
                        column: x => x.RecordedById,
                        principalTable: "ChurchMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChurchServiceAttendances_ChurchServiceSessions_ChurchServic~",
                        column: x => x.ChurchServiceSessionId,
                        principalTable: "ChurchServiceSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "\"DeletedAt\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true,
                filter: "\"DeletedAt\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Churches_EparchyId_Name",
                table: "Churches",
                columns: new[] { "EparchyId", "Name" },
                unique: true,
                filter: "\"DeletedAt\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ChurchMembers_ChurchId",
                table: "ChurchMembers",
                column: "ChurchId");

            migrationBuilder.CreateIndex(
                name: "IX_ChurchMembers_UserId_ChurchId",
                table: "ChurchMembers",
                columns: new[] { "UserId", "ChurchId" },
                unique: true,
                filter: "\"DateLeft\" IS NULL AND \"DeletedAt\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ChurchServiceAttendances_ChurchMemberId",
                table: "ChurchServiceAttendances",
                column: "ChurchMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ChurchServiceAttendances_ChurchServiceSessionId_ChurchMembe~",
                table: "ChurchServiceAttendances",
                columns: new[] { "ChurchServiceSessionId", "ChurchMemberId" },
                unique: true,
                filter: "\"DeletedAt\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ChurchServiceAttendances_RecordedById",
                table: "ChurchServiceAttendances",
                column: "RecordedById");

            migrationBuilder.CreateIndex(
                name: "IX_ChurchServices_ChurchId_StartTime",
                table: "ChurchServices",
                columns: new[] { "ChurchId", "StartTime" });

            migrationBuilder.CreateIndex(
                name: "IX_ChurchServiceSessions_ChurchServiceId_StartTime",
                table: "ChurchServiceSessions",
                columns: new[] { "ChurchServiceId", "StartTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Eparchies_Name",
                table: "Eparchies",
                column: "Name",
                unique: true,
                filter: "\"DeletedAt\" IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChurchServiceAttendances");

            migrationBuilder.DropTable(
                name: "ChurchMembers");

            migrationBuilder.DropTable(
                name: "ChurchServiceSessions");

            migrationBuilder.DropTable(
                name: "ChurchServices");

            migrationBuilder.DropTable(
                name: "Churches");

            migrationBuilder.DropTable(
                name: "Eparchies");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens");

            migrationBuilder.AddColumn<bool>(
                name: "IsRevoked",
                table: "RefreshTokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql("UPDATE \"RefreshTokens\" SET \"IsRevoked\" = TRUE WHERE \"RevokedAt\" IS NOT NULL;");

            migrationBuilder.DropColumn(
                name: "RevokedAt",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RefreshTokens");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiresAt",
                table: "RefreshTokens",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.RenameColumn(
                name: "ExpiresAt",
                table: "RefreshTokens",
                newName: "RefreshTokenExpiryTime");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "RefreshTokens",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(512)",
                oldMaxLength: 512);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Users",
                newName: "DeleteDate");

            migrationBuilder.Sql("UPDATE \"Users\" SET \"UpdatedAt\" = \"CreatedAt\" WHERE \"UpdatedAt\" IS NULL;");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Users",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "CreatedDate");
        }
    }
}
