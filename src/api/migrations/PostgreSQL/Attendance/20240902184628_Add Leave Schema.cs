using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSH.Starter.WebApi.Migrations.PostgreSQL.Attendance
{
    /// <inheritdoc />
    public partial class AddLeaveSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "attendance");

            migrationBuilder.CreateTable(
                name: "LeaveCategory",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveType",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leaves",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FromDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ToDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Remarks = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    LeaveCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    LeaveTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leaves_LeaveCategory_LeaveCategoryId",
                        column: x => x.LeaveCategoryId,
                        principalSchema: "attendance",
                        principalTable: "LeaveCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leaves_LeaveType_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalSchema: "attendance",
                        principalTable: "LeaveType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leaves_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_LeaveCategoryId",
                schema: "attendance",
                table: "Leaves",
                column: "LeaveCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_LeaveTypeId",
                schema: "attendance",
                table: "Leaves",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_UserId",
                schema: "attendance",
                table: "Leaves",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leaves",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "LeaveCategory",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "LeaveType",
                schema: "attendance");
        }
    }
}
