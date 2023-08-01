using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeLeaveTracking.Data.Migrations
{
    public partial class three : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_NotificationTypes_NotificationId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "NotificationId",
                table: "Notifications",
                newName: "NotificationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_NotificationId",
                table: "Notifications",
                newName: "IX_Notifications_NotificationTypeId");

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 10, 28, 44, 809, DateTimeKind.Utc).AddTicks(2759), new DateTime(2023, 7, 10, 10, 28, 44, 809, DateTimeKind.Utc).AddTicks(3430) });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 10, 28, 44, 809, DateTimeKind.Utc).AddTicks(5594), new DateTime(2023, 7, 10, 10, 28, 44, 809, DateTimeKind.Utc).AddTicks(5596) });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 10, 28, 44, 809, DateTimeKind.Utc).AddTicks(5598), new DateTime(2023, 7, 10, 10, 28, 44, 809, DateTimeKind.Utc).AddTicks(5599) });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 10, 28, 44, 809, DateTimeKind.Utc).AddTicks(5600), new DateTime(2023, 7, 10, 10, 28, 44, 809, DateTimeKind.Utc).AddTicks(5600) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 10, 28, 44, 808, DateTimeKind.Utc).AddTicks(7504), new DateTime(2023, 7, 10, 10, 28, 44, 808, DateTimeKind.Utc).AddTicks(8489) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 10, 28, 44, 808, DateTimeKind.Utc).AddTicks(9961), new DateTime(2023, 7, 10, 10, 28, 44, 808, DateTimeKind.Utc).AddTicks(9963) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 10, 28, 44, 808, DateTimeKind.Utc).AddTicks(9964), new DateTime(2023, 7, 10, 10, 28, 44, 808, DateTimeKind.Utc).AddTicks(9965) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 10, 28, 44, 808, DateTimeKind.Utc).AddTicks(9966), new DateTime(2023, 7, 10, 10, 28, 44, 808, DateTimeKind.Utc).AddTicks(9967) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 10, 28, 44, 808, DateTimeKind.Utc).AddTicks(9969), new DateTime(2023, 7, 10, 10, 28, 44, 808, DateTimeKind.Utc).AddTicks(9970) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 10, 28, 44, 808, DateTimeKind.Utc).AddTicks(9971), new DateTime(2023, 7, 10, 10, 28, 44, 808, DateTimeKind.Utc).AddTicks(9972) });

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 10, 28, 44, 810, DateTimeKind.Utc).AddTicks(2900), new DateTime(2023, 7, 10, 10, 28, 44, 810, DateTimeKind.Utc).AddTicks(3612) });

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 10, 28, 44, 810, DateTimeKind.Utc).AddTicks(5091), new DateTime(2023, 7, 10, 10, 28, 44, 810, DateTimeKind.Utc).AddTicks(5095) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 10, 28, 44, 809, DateTimeKind.Utc).AddTicks(8218), new DateTime(2023, 7, 10, 10, 28, 44, 809, DateTimeKind.Utc).AddTicks(8851) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 10, 28, 44, 810, DateTimeKind.Utc).AddTicks(510), new DateTime(2023, 7, 10, 10, 28, 44, 810, DateTimeKind.Utc).AddTicks(512) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 10, 28, 44, 810, DateTimeKind.Utc).AddTicks(517), new DateTime(2023, 7, 10, 10, 28, 44, 810, DateTimeKind.Utc).AddTicks(518) });

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_NotificationTypes_NotificationTypeId",
                table: "Notifications",
                column: "NotificationTypeId",
                principalTable: "NotificationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_NotificationTypes_NotificationTypeId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "NotificationTypeId",
                table: "Notifications",
                newName: "NotificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_NotificationTypeId",
                table: "Notifications",
                newName: "IX_Notifications_NotificationId");

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

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 9, 22, 31, 812, DateTimeKind.Utc).AddTicks(5470), new DateTime(2023, 7, 10, 9, 22, 31, 812, DateTimeKind.Utc).AddTicks(6093) });

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 9, 22, 31, 812, DateTimeKind.Utc).AddTicks(7556), new DateTime(2023, 7, 10, 9, 22, 31, 812, DateTimeKind.Utc).AddTicks(7558) });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_NotificationTypes_NotificationId",
                table: "Notifications",
                column: "NotificationId",
                principalTable: "NotificationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
