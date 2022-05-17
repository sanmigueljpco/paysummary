using Microsoft.EntityFrameworkCore.Migrations;

namespace PaySummary.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeReports",
                columns: table => new
                {
                    TimeReportId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(nullable: false),
                    Date = table.Column<string>(nullable: true),
                    HoursWorked = table.Column<decimal>(nullable: false),
                    JobGroup = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeReports", x => x.TimeReportId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeReports");
        }
    }
}
