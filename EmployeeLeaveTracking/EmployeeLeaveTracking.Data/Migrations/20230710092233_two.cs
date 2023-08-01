using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeLeaveTracking.Data.Migrations
{
    public partial class two : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeaveRequestId = table.Column<int>(type: "int", nullable: false),
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    IsViewed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_LeaveRequests_LeaveRequestId",
                        column: x => x.LeaveRequestId,
                        principalTable: "LeaveRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationTypes_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 9, 22, 31, 811, DateTimeKind.Utc).AddTicks(1808), new DateTime(2023, 7, 10, 9, 22, 31, 811, DateTimeKind.Utc).AddTicks(2409) });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 9, 22, 31, 811, DateTimeKind.Utc).AddTicks(4865), new DateTime(2023, 7, 10, 9, 22, 31, 811, DateTimeKind.Utc).AddTicks(4872) });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 9, 22, 31, 811, DateTimeKind.Utc).AddTicks(4876), new DateTime(2023, 7, 10, 9, 22, 31, 811, DateTimeKind.Utc).AddTicks(4877) });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 9, 22, 31, 811, DateTimeKind.Utc).AddTicks(4879), new DateTime(2023, 7, 10, 9, 22, 31, 811, DateTimeKind.Utc).AddTicks(4883) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 9, 22, 31, 810, DateTimeKind.Utc).AddTicks(6369), new DateTime(2023, 7, 10, 9, 22, 31, 810, DateTimeKind.Utc).AddTicks(7307) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 9, 22, 31, 810, DateTimeKind.Utc).AddTicks(8896), new DateTime(2023, 7, 10, 9, 22, 31, 810, DateTimeKind.Utc).AddTicks(8898) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 9, 22, 31, 810, DateTimeKind.Utc).AddTicks(8902), new DateTime(2023, 7, 10, 9, 22, 31, 810, DateTimeKind.Utc).AddTicks(8903) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 9, 22, 31, 810, DateTimeKind.Utc).AddTicks(8904), new DateTime(2023, 7, 10, 9, 22, 31, 810, DateTimeKind.Utc).AddTicks(8905) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 9, 22, 31, 810, DateTimeKind.Utc).AddTicks(8907), new DateTime(2023, 7, 10, 9, 22, 31, 810, DateTimeKind.Utc).AddTicks(8908) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 9, 22, 31, 810, DateTimeKind.Utc).AddTicks(8910), new DateTime(2023, 7, 10, 9, 22, 31, 810, DateTimeKind.Utc).AddTicks(8910) });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "NotificationTypeName" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 7, 10, 9, 22, 31, 812, DateTimeKind.Utc).AddTicks(5470), false, null, new DateTime(2023, 7, 10, 9, 22, 31, 812, DateTimeKind.Utc).AddTicks(6093), "Leave Request" },
                    { 2, null, new DateTime(2023, 7, 10, 9, 22, 31, 812, DateTimeKind.Utc).AddTicks(7556), false, null, new DateTime(2023, 7, 10, 9, 22, 31, 812, DateTimeKind.Utc).AddTicks(7558), "Leave Response" }
                });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 9, 22, 31, 811, DateTimeKind.Utc).AddTicks(9540), new DateTime(2023, 7, 10, 9, 22, 31, 812, DateTimeKind.Utc).AddTicks(440) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 9, 22, 31, 812, DateTimeKind.Utc).AddTicks(2294), new DateTime(2023, 7, 10, 9, 22, 31, 812, DateTimeKind.Utc).AddTicks(2298) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 9, 22, 31, 812, DateTimeKind.Utc).AddTicks(2302), new DateTime(2023, 7, 10, 9, 22, 31, 812, DateTimeKind.Utc).AddTicks(2303) });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_LeaveRequestId",
                table: "Notifications",
                column: "LeaveRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationId",
                table: "Notifications",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 4, 13, 32, 35, 435, DateTimeKind.Utc).AddTicks(8077), new DateTime(2023, 7, 4, 13, 32, 35, 435, DateTimeKind.Utc).AddTicks(8849) });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 4, 13, 32, 35, 436, DateTimeKind.Utc).AddTicks(857), new DateTime(2023, 7, 4, 13, 32, 35, 436, DateTimeKind.Utc).AddTicks(858) });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 4, 13, 32, 35, 436, DateTimeKind.Utc).AddTicks(861), new DateTime(2023, 7, 4, 13, 32, 35, 436, DateTimeKind.Utc).AddTicks(861) });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 4, 13, 32, 35, 436, DateTimeKind.Utc).AddTicks(864), new DateTime(2023, 7, 4, 13, 32, 35, 436, DateTimeKind.Utc).AddTicks(864) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 4, 13, 32, 35, 435, DateTimeKind.Utc).AddTicks(1816), new DateTime(2023, 7, 4, 13, 32, 35, 435, DateTimeKind.Utc).AddTicks(3038) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 4, 13, 32, 35, 435, DateTimeKind.Utc).AddTicks(4998), new DateTime(2023, 7, 4, 13, 32, 35, 435, DateTimeKind.Utc).AddTicks(4999) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 4, 13, 32, 35, 435, DateTimeKind.Utc).AddTicks(5002), new DateTime(2023, 7, 4, 13, 32, 35, 435, DateTimeKind.Utc).AddTicks(5003) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 4, 13, 32, 35, 435, DateTimeKind.Utc).AddTicks(5005), new DateTime(2023, 7, 4, 13, 32, 35, 435, DateTimeKind.Utc).AddTicks(5006) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 4, 13, 32, 35, 435, DateTimeKind.Utc).AddTicks(5008), new DateTime(2023, 7, 4, 13, 32, 35, 435, DateTimeKind.Utc).AddTicks(5009) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 4, 13, 32, 35, 435, DateTimeKind.Utc).AddTicks(5010), new DateTime(2023, 7, 4, 13, 32, 35, 435, DateTimeKind.Utc).AddTicks(5011) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 4, 13, 32, 35, 436, DateTimeKind.Utc).AddTicks(3490), new DateTime(2023, 7, 4, 13, 32, 35, 436, DateTimeKind.Utc).AddTicks(4240) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 4, 13, 32, 35, 437, DateTimeKind.Utc).AddTicks(6808), new DateTime(2023, 7, 4, 13, 32, 35, 437, DateTimeKind.Utc).AddTicks(6822) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 4, 13, 32, 35, 437, DateTimeKind.Utc).AddTicks(6828), new DateTime(2023, 7, 4, 13, 32, 35, 437, DateTimeKind.Utc).AddTicks(6829) });
        }
    }
}
