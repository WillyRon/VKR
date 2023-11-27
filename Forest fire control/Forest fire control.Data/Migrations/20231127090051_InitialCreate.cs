using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Forest_fire_control.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SysName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObservationSite",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Longitude = table.Column<float>(nullable: false),
                    Latitude = table.Column<float>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    RegionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservationSite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObservationSite_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    RegionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incedent",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    VideoArchiveId = table.Column<Guid>(nullable: false),
                    ObservationSiteId = table.Column<Guid>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incedent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incedent_ObservationSite_ObservationSiteId",
                        column: x => x.ObservationSiteId,
                        principalTable: "ObservationSite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ObservationSiteId = table.Column<Guid>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Application_ObservationSite_ObservationSiteId",
                        column: x => x.ObservationSiteId,
                        principalTable: "ObservationSite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Application_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageError",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageError", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageError_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoArchive",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ObservationSiteId = table.Column<Guid>(nullable: false),
                    IncedentId = table.Column<Guid>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoArchive", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoArchive_Incedent_IncedentId",
                        column: x => x.IncedentId,
                        principalTable: "Incedent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoArchive_ObservationSite_ObservationSiteId",
                        column: x => x.ObservationSiteId,
                        principalTable: "ObservationSite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Application_ObservationSiteId",
                table: "Application",
                column: "ObservationSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_UserId",
                table: "Application",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Incedent_ObservationSiteId",
                table: "Incedent",
                column: "ObservationSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageError_UserId",
                table: "MessageError",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ObservationSite_RegionId",
                table: "ObservationSite",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RegionId",
                table: "User",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoArchive_IncedentId",
                table: "VideoArchive",
                column: "IncedentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VideoArchive_ObservationSiteId",
                table: "VideoArchive",
                column: "ObservationSiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "MessageError");

            migrationBuilder.DropTable(
                name: "VideoArchive");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Incedent");

            migrationBuilder.DropTable(
                name: "ObservationSite");

            migrationBuilder.DropTable(
                name: "Region");
        }
    }
}
