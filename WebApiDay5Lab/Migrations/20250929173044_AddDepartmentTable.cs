using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiDay5Lab.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "departmentId",
                table: "TblEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TblDepartments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblDepartments", x => x.DepartmentId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblEmployees_departmentId",
                table: "TblEmployees",
                column: "departmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblEmployees_TblDepartments_departmentId",
                table: "TblEmployees",
                column: "departmentId",
                principalTable: "TblDepartments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblEmployees_TblDepartments_departmentId",
                table: "TblEmployees");

            migrationBuilder.DropTable(
                name: "TblDepartments");

            migrationBuilder.DropIndex(
                name: "IX_TblEmployees_departmentId",
                table: "TblEmployees");

            migrationBuilder.DropColumn(
                name: "departmentId",
                table: "TblEmployees");
        }
    }
}
