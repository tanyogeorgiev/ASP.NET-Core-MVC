using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HealthR.Data.Migrations
{
    public partial class PrescriptionColumnsInMedicalSheetRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalSheets_Prescriptions_PrescriptionId",
                table: "MedicalSheets");

            migrationBuilder.DropIndex(
                name: "IX_MedicalSheets_PrescriptionId",
                table: "MedicalSheets");

            migrationBuilder.DropColumn(
                name: "PrescriptionId",
                table: "MedicalSheets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrescriptionId",
                table: "MedicalSheets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalSheets_PrescriptionId",
                table: "MedicalSheets",
                column: "PrescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalSheets_Prescriptions_PrescriptionId",
                table: "MedicalSheets",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
