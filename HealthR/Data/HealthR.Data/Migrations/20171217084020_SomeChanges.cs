using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HealthR.Data.Migrations
{
    public partial class SomeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_MedicalSheets_MedicalSheetId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_MedicalSheetId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "MedicalSheetId",
                table: "Prescriptions");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "MedicalSheetId",
                table: "Prescriptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_MedicalSheetId",
                table: "Prescriptions",
                column: "MedicalSheetId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_MedicalSheets_MedicalSheetId",
                table: "Prescriptions",
                column: "MedicalSheetId",
                principalTable: "MedicalSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
