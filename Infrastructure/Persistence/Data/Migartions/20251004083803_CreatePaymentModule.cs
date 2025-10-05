using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migartions
{
    /// <inheritdoc />
    public partial class CreatePaymentModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserSSN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserBorrowId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GatewayFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Method = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ConfirmedByEmployeeId = table.Column<int>(type: "int", nullable: true),
                    IsDelivery = table.Column<bool>(type: "bit", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_payments_Employees_ConfirmedByEmployeeId",
                        column: x => x.ConfirmedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmpId");
                    table.ForeignKey(
                        name: "FK_payments_UserBorrows_UserBorrowId",
                        column: x => x.UserBorrowId,
                        principalTable: "UserBorrows",
                        principalColumn: "UserBorrowId");
                    table.ForeignKey(
                        name: "FK_payments_Users_UserSSN",
                        column: x => x.UserSSN,
                        principalTable: "Users",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_payments_ConfirmedByEmployeeId",
                table: "payments",
                column: "ConfirmedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_payments_UserBorrowId",
                table: "payments",
                column: "UserBorrowId");

            migrationBuilder.CreateIndex(
                name: "IX_payments_UserSSN",
                table: "payments",
                column: "UserSSN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payments");
        }
    }
}
