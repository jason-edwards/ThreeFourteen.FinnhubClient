using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThreeFourteen.Finnhub.Client.Migrations
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiRequests",
                columns: table => new
                {
                    RequestId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(maxLength: 50, nullable: true),
                    RequestTime = table.Column<DateTime>(nullable: false),
                    Weight = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiRequests", x => x.RequestId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiRequests");
        }
    }
}
