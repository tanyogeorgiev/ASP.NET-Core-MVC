using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HealthR.Data.Migrations
{
    public partial class ScheduleOneToMAny : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedules_OwnerId",
                table: "Schedules");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_OwnerId",
                table: "Schedules",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedules_OwnerId",
                table: "Schedules");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_OwnerId",
                table: "Schedules",
                column: "OwnerId",
                unique: true);
        }
    }
}
