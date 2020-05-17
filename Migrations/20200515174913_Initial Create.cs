using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentAppAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassDetails",
                columns: table => new
                {
                    ClassId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassDetails", x => x.ClassId);
                });

            migrationBuilder.CreateTable(
                name: "SubjectDetails",
                columns: table => new
                {
                    SubjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectDetails", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "StudentDetails",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", nullable: false),
                    ClassId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentDetails", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_StudentDetails_ClassDetails_ClassId",
                        column: x => x.ClassId,
                        principalTable: "ClassDetails",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StuSubjectMarks",
                columns: table => new
                {
                    SubMarksId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marks = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    StudentId = table.Column<int>(nullable: true),
                    SubjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StuSubjectMarks", x => x.SubMarksId);
                    table.ForeignKey(
                        name: "FK_StuSubjectMarks_StudentDetails_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentDetails",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StuSubjectMarks_SubjectDetails_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "SubjectDetails",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentDetails_ClassId",
                table: "StudentDetails",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_StuSubjectMarks_StudentId",
                table: "StuSubjectMarks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StuSubjectMarks_SubjectId",
                table: "StuSubjectMarks",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StuSubjectMarks");

            migrationBuilder.DropTable(
                name: "StudentDetails");

            migrationBuilder.DropTable(
                name: "SubjectDetails");

            migrationBuilder.DropTable(
                name: "ClassDetails");
        }
    }
}
