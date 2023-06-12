using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeLeaveTracking.Data.Migrations
{
    public partial class two : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 6, 59, 11, 408, DateTimeKind.Utc).AddTicks(2286), new DateTime(2023, 5, 24, 6, 59, 11, 408, DateTimeKind.Utc).AddTicks(2889) });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 6, 59, 11, 408, DateTimeKind.Utc).AddTicks(4255), new DateTime(2023, 5, 24, 6, 59, 11, 408, DateTimeKind.Utc).AddTicks(4257) });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 6, 59, 11, 408, DateTimeKind.Utc).AddTicks(4259), new DateTime(2023, 5, 24, 6, 59, 11, 408, DateTimeKind.Utc).AddTicks(4259) });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 6, 59, 11, 408, DateTimeKind.Utc).AddTicks(4260), new DateTime(2023, 5, 24, 6, 59, 11, 408, DateTimeKind.Utc).AddTicks(4261) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 6, 59, 11, 407, DateTimeKind.Utc).AddTicks(6774), new DateTime(2023, 5, 24, 6, 59, 11, 407, DateTimeKind.Utc).AddTicks(7945) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 6, 59, 11, 407, DateTimeKind.Utc).AddTicks(9755), new DateTime(2023, 5, 24, 6, 59, 11, 407, DateTimeKind.Utc).AddTicks(9757) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 6, 59, 11, 407, DateTimeKind.Utc).AddTicks(9761), new DateTime(2023, 5, 24, 6, 59, 11, 407, DateTimeKind.Utc).AddTicks(9761) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 6, 59, 11, 407, DateTimeKind.Utc).AddTicks(9763), new DateTime(2023, 5, 24, 6, 59, 11, 407, DateTimeKind.Utc).AddTicks(9763) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 6, 59, 11, 407, DateTimeKind.Utc).AddTicks(9765), new DateTime(2023, 5, 24, 6, 59, 11, 407, DateTimeKind.Utc).AddTicks(9765) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 6, 59, 11, 407, DateTimeKind.Utc).AddTicks(9766), new DateTime(2023, 5, 24, 6, 59, 11, 407, DateTimeKind.Utc).AddTicks(9767) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 6, 59, 11, 408, DateTimeKind.Utc).AddTicks(6179), new DateTime(2023, 5, 24, 6, 59, 11, 408, DateTimeKind.Utc).AddTicks(6762) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 6, 59, 11, 408, DateTimeKind.Utc).AddTicks(8574), new DateTime(2023, 5, 24, 6, 59, 11, 408, DateTimeKind.Utc).AddTicks(8575) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 6, 59, 11, 408, DateTimeKind.Utc).AddTicks(8577), new DateTime(2023, 5, 24, 6, 59, 11, 408, DateTimeKind.Utc).AddTicks(8578) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 5, 31, 15, 118, DateTimeKind.Utc).AddTicks(1445), new DateTime(2023, 5, 24, 5, 31, 15, 118, DateTimeKind.Utc).AddTicks(2242) });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 5, 31, 15, 118, DateTimeKind.Utc).AddTicks(3968), new DateTime(2023, 5, 24, 5, 31, 15, 118, DateTimeKind.Utc).AddTicks(3976) });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 5, 31, 15, 118, DateTimeKind.Utc).AddTicks(3978), new DateTime(2023, 5, 24, 5, 31, 15, 118, DateTimeKind.Utc).AddTicks(3979) });

            migrationBuilder.UpdateData(
                table: "Designations",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 5, 31, 15, 118, DateTimeKind.Utc).AddTicks(3981), new DateTime(2023, 5, 24, 5, 31, 15, 118, DateTimeKind.Utc).AddTicks(3981) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 5, 31, 15, 116, DateTimeKind.Utc).AddTicks(5775), new DateTime(2023, 5, 24, 5, 31, 15, 117, DateTimeKind.Utc).AddTicks(5372) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 5, 31, 15, 117, DateTimeKind.Utc).AddTicks(7569), new DateTime(2023, 5, 24, 5, 31, 15, 117, DateTimeKind.Utc).AddTicks(7572) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 5, 31, 15, 117, DateTimeKind.Utc).AddTicks(7574), new DateTime(2023, 5, 24, 5, 31, 15, 117, DateTimeKind.Utc).AddTicks(7575) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 5, 31, 15, 117, DateTimeKind.Utc).AddTicks(7577), new DateTime(2023, 5, 24, 5, 31, 15, 117, DateTimeKind.Utc).AddTicks(7578) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 5, 31, 15, 117, DateTimeKind.Utc).AddTicks(7581), new DateTime(2023, 5, 24, 5, 31, 15, 117, DateTimeKind.Utc).AddTicks(7582) });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 5, 31, 15, 117, DateTimeKind.Utc).AddTicks(7584), new DateTime(2023, 5, 24, 5, 31, 15, 117, DateTimeKind.Utc).AddTicks(7585) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 5, 31, 15, 118, DateTimeKind.Utc).AddTicks(6703), new DateTime(2023, 5, 24, 5, 31, 15, 118, DateTimeKind.Utc).AddTicks(7413) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 5, 31, 15, 118, DateTimeKind.Utc).AddTicks(9124), new DateTime(2023, 5, 24, 5, 31, 15, 118, DateTimeKind.Utc).AddTicks(9128) });

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 5, 24, 5, 31, 15, 118, DateTimeKind.Utc).AddTicks(9131), new DateTime(2023, 5, 24, 5, 31, 15, 118, DateTimeKind.Utc).AddTicks(9132) });
        }
    }
}
