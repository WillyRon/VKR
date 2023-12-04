using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Forest_fire_control.Data.Migrations
{
    public partial class IncidentActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoArchive_Incedent_IncedentId",
                table: "VideoArchive");

            migrationBuilder.AlterColumn<Guid>(
                name: "IncedentId",
                table: "VideoArchive",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<bool>(
                name: "IsActiveIncident",
                table: "ObservationSite",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoArchive_Incedent_IncedentId",
                table: "VideoArchive",
                column: "IncedentId",
                principalTable: "Incedent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoArchive_Incedent_IncedentId",
                table: "VideoArchive");

            migrationBuilder.DropColumn(
                name: "IsActiveIncident",
                table: "ObservationSite");

            migrationBuilder.AlterColumn<Guid>(
                name: "IncedentId",
                table: "VideoArchive",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoArchive_Incedent_IncedentId",
                table: "VideoArchive",
                column: "IncedentId",
                principalTable: "Incedent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
