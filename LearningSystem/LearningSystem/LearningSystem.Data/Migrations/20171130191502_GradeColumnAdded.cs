
namespace LearningSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    using System;
    using System.Collections.Generic;

    public partial class GradeColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "StudenCourse",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "StudenCourse");
        }
    }
}
