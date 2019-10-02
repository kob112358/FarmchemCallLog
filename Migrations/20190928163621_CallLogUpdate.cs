using Microsoft.EntityFrameworkCore.Migrations;

namespace FarmchemCallLog.Migrations
{
    public partial class CallLogUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressID",
                table: "Customer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_AddressID",
                table: "Customer",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Address_AddressID",
                table: "Customer",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Address_AddressID",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_AddressID",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "Customer");
        }
    }
}
