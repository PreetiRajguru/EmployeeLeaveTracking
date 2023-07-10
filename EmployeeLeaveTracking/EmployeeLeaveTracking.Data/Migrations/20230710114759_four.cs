using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeLeaveTracking.Data.Migrations
{
    public partial class four : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 11, 47, 57, 963, DateTimeKind.Utc).AddTicks(9472), new DateTime(2023, 7, 10, 11, 47, 57, 964, DateTimeKind.Utc).AddTicks(169) });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 11, 47, 57, 964, DateTimeKind.Utc).AddTicks(1490), new DateTime(2023, 7, 10, 11, 47, 57, 964, DateTimeKind.Utc).AddTicks(1492) });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 11, 47, 57, 964, DateTimeKind.Utc).AddTicks(1494), new DateTime(2023, 7, 10, 11, 47, 57, 964, DateTimeKind.Utc).AddTicks(1494) });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 11, 47, 57, 964, DateTimeKind.Utc).AddTicks(1496), new DateTime(2023, 7, 10, 11, 47, 57, 964, DateTimeKind.Utc).AddTicks(1497) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 11, 47, 57, 962, DateTimeKind.Utc).AddTicks(9347), new DateTime(2023, 7, 10, 11, 47, 57, 963, DateTimeKind.Utc).AddTicks(5208) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 11, 47, 57, 963, DateTimeKind.Utc).AddTicks(6584), new DateTime(2023, 7, 10, 11, 47, 57, 963, DateTimeKind.Utc).AddTicks(6586) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 11, 47, 57, 963, DateTimeKind.Utc).AddTicks(6588), new DateTime(2023, 7, 10, 11, 47, 57, 963, DateTimeKind.Utc).AddTicks(6588) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 11, 47, 57, 963, DateTimeKind.Utc).AddTicks(6589), new DateTime(2023, 7, 10, 11, 47, 57, 963, DateTimeKind.Utc).AddTicks(6590) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 11, 47, 57, 963, DateTimeKind.Utc).AddTicks(6591), new DateTime(2023, 7, 10, 11, 47, 57, 963, DateTimeKind.Utc).AddTicks(6592) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 11, 47, 57, 963, DateTimeKind.Utc).AddTicks(6593), new DateTime(2023, 7, 10, 11, 47, 57, 963, DateTimeKind.Utc).AddTicks(6594) });

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 11, 47, 57, 964, DateTimeKind.Utc).AddTicks(7834), new DateTime(2023, 7, 10, 11, 47, 57, 964, DateTimeKind.Utc).AddTicks(8385) });

            migrationBuilder.UpdateData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 11, 47, 57, 964, DateTimeKind.Utc).AddTicks(9627), new DateTime(2023, 7, 10, 11, 47, 57, 964, DateTimeKind.Utc).AddTicks(9629) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 11, 47, 57, 964, DateTimeKind.Utc).AddTicks(3585), new DateTime(2023, 7, 10, 11, 47, 57, 964, DateTimeKind.Utc).AddTicks(4274) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 11, 47, 57, 964, DateTimeKind.Utc).AddTicks(5658), new DateTime(2023, 7, 10, 11, 47, 57, 964, DateTimeKind.Utc).AddTicks(5660) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 7, 10, 11, 47, 57, 964, DateTimeKind.Utc).AddTicks(5661), new DateTime(2023, 7, 10, 11, 47, 57, 964, DateTimeKind.Utc).AddTicks(5662) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
