using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroServiceCrudUser.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "hash1", "johndoe" },
                    { 2, "jane.smith@example.com", "hash2", "janesmith" },
                    { 3, "michael.brown@example.com", "hash3", "michaelbrown" },
                    { 4, "lisa.jones@example.com", "hash4", "lisajones" },
                    { 5, "chris.miller@example.com", "hash5", "chrismiller" },
                    { 6, "patricia.wilson@example.com", "hash6", "patriciawilson" },
                    { 7, "robert.taylor@example.com", "hash7", "roberttaylor" },
                    { 8, "linda.moore@example.com", "hash8", "lindamoore" },
                    { 9, "william.thomas@example.com", "hash9", "williamthomas" },
                    { 10, "barbara.clark@example.com", "hash10", "barbaraclark" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
