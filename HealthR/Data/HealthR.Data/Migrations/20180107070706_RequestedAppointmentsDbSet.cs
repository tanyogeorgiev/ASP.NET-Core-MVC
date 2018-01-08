using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HealthR.Data.Migrations
{
    public partial class RequestedAppointmentsDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleAppointmentRequest_Appointments_AppointmentRequestId",
                table: "ScheduleAppointmentRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleAppointmentRequest_Schedules_RequestedScheduleId",
                table: "ScheduleAppointmentRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleAppointmentRequest",
                table: "ScheduleAppointmentRequest");

            migrationBuilder.RenameTable(
                name: "ScheduleAppointmentRequest",
                newName: "RequestedAppointments");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleAppointmentRequest_AppointmentRequestId",
                table: "RequestedAppointments",
                newName: "IX_RequestedAppointments_AppointmentRequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestedAppointments",
                table: "RequestedAppointments",
                columns: new[] { "RequestedScheduleId", "AppointmentRequestId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RequestedAppointments_Appointments_AppointmentRequestId",
                table: "RequestedAppointments",
                column: "AppointmentRequestId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestedAppointments_Schedules_RequestedScheduleId",
                table: "RequestedAppointments",
                column: "RequestedScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestedAppointments_Appointments_AppointmentRequestId",
                table: "RequestedAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestedAppointments_Schedules_RequestedScheduleId",
                table: "RequestedAppointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestedAppointments",
                table: "RequestedAppointments");

            migrationBuilder.RenameTable(
                name: "RequestedAppointments",
                newName: "ScheduleAppointmentRequest");

            migrationBuilder.RenameIndex(
                name: "IX_RequestedAppointments_AppointmentRequestId",
                table: "ScheduleAppointmentRequest",
                newName: "IX_ScheduleAppointmentRequest_AppointmentRequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleAppointmentRequest",
                table: "ScheduleAppointmentRequest",
                columns: new[] { "RequestedScheduleId", "AppointmentRequestId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleAppointmentRequest_Appointments_AppointmentRequestId",
                table: "ScheduleAppointmentRequest",
                column: "AppointmentRequestId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleAppointmentRequest_Schedules_RequestedScheduleId",
                table: "ScheduleAppointmentRequest",
                column: "RequestedScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
