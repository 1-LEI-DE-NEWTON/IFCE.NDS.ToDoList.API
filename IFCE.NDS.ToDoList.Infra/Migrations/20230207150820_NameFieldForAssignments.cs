using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IFCE.NDS.ToDoList.Infra.Migrations
{
    public partial class NameFieldForAssignments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Assignment",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Assignment");
        }
    }
}
