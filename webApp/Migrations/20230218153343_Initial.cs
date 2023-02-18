using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace webApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastName = table.Column<string>(type: "character varying", nullable: true),
                    FirstName = table.Column<string>(type: "character varying", nullable: true),
                    Patronomic = table.Column<string>(type: "character varying", nullable: true),
                    Gradebook = table.Column<string>(type: "character varying", nullable: true),
                    Group = table.Column<string>(type: "character varying", nullable: true),
                    TuitionType = table.Column<string>(type: "character varying", nullable: true),
                    TuitionForm = table.Column<string>(type: "character varying", nullable: true),
                    TrainingLevel = table.Column<string>(type: "character varying", nullable: true),
                    StudentCondition = table.Column<string>(type: "character varying", nullable: true),
                    Speciality = table.Column<string>(type: "character varying", nullable: true),
                    Specialization = table.Column<string>(type: "character varying", nullable: true),
                    Course = table.Column<string>(type: "character varying", nullable: true),
                    DateTime = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastName = table.Column<string>(type: "character varying", nullable: true),
                    FirstName = table.Column<string>(type: "character varying", nullable: true),
                    Patronomic = table.Column<string>(type: "character varying", nullable: true),
                    Post = table.Column<string>(type: "character varying", nullable: true),
                    Degree = table.Column<string>(type: "character varying", nullable: true),
                    Title = table.Column<string>(type: "character varying", nullable: true),
                    Department = table.Column<string>(type: "character varying", nullable: true),
                    DateTime = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    Login = table.Column<string>(type: "character varying", nullable: false),
                    Password = table.Column<string>(type: "character varying", nullable: false),
                    Role = table.Column<string>(type: "character varying", nullable: false),
                    Id = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
