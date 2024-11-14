using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAuthEmp.Migrations
{
    /// <inheritdoc />
    public partial class FinalOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_DepartmentDto_DepartmentDtoId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "DepartmentDto");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepartmentDtoId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DepartmentDtoId",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentDtoId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DepartmentDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentDto", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentDtoId",
                table: "Employees",
                column: "DepartmentDtoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_DepartmentDto_DepartmentDtoId",
                table: "Employees",
                column: "DepartmentDtoId",
                principalTable: "DepartmentDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
