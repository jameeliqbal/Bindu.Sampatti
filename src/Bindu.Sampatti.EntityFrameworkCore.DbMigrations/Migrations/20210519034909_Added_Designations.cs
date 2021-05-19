using Microsoft.EntityFrameworkCore.Migrations;

namespace Bindu.Sampatti.Migrations
{
    public partial class Added_Designations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppDepartments",
                table: "AppDepartments");

            migrationBuilder.RenameTable(
                name: "AppDepartments",
                newName: "AppDesignations");

            migrationBuilder.RenameIndex(
                name: "IX_AppDepartments_Name",
                table: "AppDesignations",
                newName: "IX_AppDesignations_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppDesignations",
                table: "AppDesignations",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppDesignations",
                table: "AppDesignations");

            migrationBuilder.RenameTable(
                name: "AppDesignations",
                newName: "AppDepartments");

            migrationBuilder.RenameIndex(
                name: "IX_AppDesignations_Name",
                table: "AppDepartments",
                newName: "IX_AppDepartments_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppDepartments",
                table: "AppDepartments",
                column: "Id");
        }
    }
}
