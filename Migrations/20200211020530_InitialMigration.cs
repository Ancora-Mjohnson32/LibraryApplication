using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryApplication.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Author = table.Column<string>(maxLength: 30, nullable: false),
                    Genre = table.Column<int>(nullable: false),
                    SubGenre = table.Column<int>(nullable: false),
                    PageCount = table.Column<int>(maxLength: 4, nullable: false),
                    Series = table.Column<string>(maxLength: 70, nullable: true),
                    BookNumber = table.Column<int>(maxLength: 3, nullable: false),
                    Overview = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
