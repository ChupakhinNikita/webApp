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
                name: "students",
                columns: table => new
                {
                    IdStudent = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    lastName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    firstName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    patronomic = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    gradebook = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    group = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    tuitionType = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    tuitionForm = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    trainingLevel = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    studentCondition = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    speciality = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: true),
                    specialization = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: true),
                    course = table.Column<int>(type: "integer", nullable: true),
                    dateTime = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("students_pkey", x => x.IdStudent);
                });

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    IdTeacher = table.Column<string>(type: "character varying", nullable: false),
                    lastName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    firstName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    patronomic = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    post = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    degree = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    title = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    department = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: true),
                    dateTime = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("teachers_pkey", x => x.IdTeacher);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Login = table.Column<string>(type: "character varying", nullable: false),
                    Password = table.Column<string>(type: "character varying", nullable: false),
                    Role = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("User_pkey", x => x.IdUser);
                });

            migrationBuilder.CreateIndex(
                name: "unique_id",
                table: "User",
                column: "IdUser",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "teachers");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
