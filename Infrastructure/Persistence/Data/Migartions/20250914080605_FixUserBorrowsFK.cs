using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migartions
{
    /// <inheritdoc />
    public partial class FixUserBorrowsFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "UserBorrows",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBorrows_EmployeeId",
                table: "UserBorrows",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBorrows_Employees_EmployeeId",
                table: "UserBorrows",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmpId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBorrows_Employees_EmployeeId",
                table: "UserBorrows");

            migrationBuilder.DropIndex(
                name: "IX_UserBorrows_EmployeeId",
                table: "UserBorrows");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "UserBorrows");
        }
    }
}
