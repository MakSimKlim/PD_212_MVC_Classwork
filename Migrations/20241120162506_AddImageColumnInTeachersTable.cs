using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PD_212_MVC_Classwork.Migrations
{
    /// <inheritdoc />
    public partial class AddImageColumnInTeachersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Directions",
                columns: table => new
                {
                    direction_id = table.Column<byte>(type: "tinyint", nullable: false),
                    direction_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directions", x => x.direction_id);
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    discipline_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    discipline_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    number_of_lessons = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.discipline_id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    teacher_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    middle_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    work_since = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.teacher_id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    group_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    group_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    direction = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.group_id);
                    table.ForeignKey(
                        name: "FK_Groups_Directions_direction",
                        column: x => x.direction,
                        principalTable: "Directions",
                        principalColumn: "direction_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectionsDisciplinesRelation",
                columns: table => new
                {
                    direction = table.Column<byte>(type: "tinyint", nullable: false),
                    discipline = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectionsDisciplinesRelation", x => new { x.direction, x.discipline });
                    table.ForeignKey(
                        name: "FK_DirectionsDisciplinesRelation_Directions_direction",
                        column: x => x.direction,
                        principalTable: "Directions",
                        principalColumn: "direction_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectionsDisciplinesRelation_Disciplines_discipline",
                        column: x => x.discipline,
                        principalTable: "Disciplines",
                        principalColumn: "discipline_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeachersDisciplinesRelation",
                columns: table => new
                {
                    teacher = table.Column<int>(type: "int", nullable: false),
                    discipline = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersDisciplinesRelation", x => new { x.teacher, x.discipline });
                    table.ForeignKey(
                        name: "FK_TeachersDisciplinesRelation_Disciplines_discipline",
                        column: x => x.discipline,
                        principalTable: "Disciplines",
                        principalColumn: "discipline_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachersDisciplinesRelation_Teachers_teacher",
                        column: x => x.teacher,
                        principalTable: "Teachers",
                        principalColumn: "teacher_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    stud_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    middle_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    group = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.stud_id);
                    table.ForeignKey(
                        name: "FK_Students_Groups_group",
                        column: x => x.group,
                        principalTable: "Groups",
                        principalColumn: "group_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DirectionsDisciplinesRelation_discipline",
                table: "DirectionsDisciplinesRelation",
                column: "discipline");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_direction",
                table: "Groups",
                column: "direction");

            migrationBuilder.CreateIndex(
                name: "IX_Students_group",
                table: "Students",
                column: "group");

            migrationBuilder.CreateIndex(
                name: "IX_TeachersDisciplinesRelation_discipline",
                table: "TeachersDisciplinesRelation",
                column: "discipline");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DirectionsDisciplinesRelation");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "TeachersDisciplinesRelation");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Directions");
        }
    }
}
