using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MiniProject5.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    deptid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    deptname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    mgrempid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("department_pkey", x => x.deptid);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    empid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    lname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ssn = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    position = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    salary = table.Column<double>(type: "double precision", nullable: false),
                    sex = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    dob = table.Column<DateOnly>(type: "date", nullable: false),
                    phoneno = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    emptype = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    level = table.Column<int>(type: "integer", nullable: true),
                    deptid = table.Column<int>(type: "integer", nullable: true),
                    supervisorid = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    reason = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    lastupdateddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("employee_pkey", x => x.empid);
                    table.ForeignKey(
                        name: "FK_employee_employee_supervisorid",
                        column: x => x.supervisorid,
                        principalTable: "employee",
                        principalColumn: "empid");
                    table.ForeignKey(
                        name: "fk_department",
                        column: x => x.deptid,
                        principalTable: "department",
                        principalColumn: "deptid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "location",
                columns: table => new
                {
                    locationid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    deptid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("location_pkey", x => x.locationid);
                    table.ForeignKey(
                        name: "fk_department",
                        column: x => x.deptid,
                        principalTable: "department",
                        principalColumn: "deptid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    projid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    projname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    deptid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("project_pkey", x => x.projid);
                    table.ForeignKey(
                        name: "fk_deptid",
                        column: x => x.deptid,
                        principalTable: "department",
                        principalColumn: "deptid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dependent",
                columns: table => new
                {
                    dependentid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    lname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    sex = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    dob = table.Column<DateOnly>(type: "date", nullable: false),
                    relationship = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    empid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("dependent_pkey", x => x.dependentid);
                    table.ForeignKey(
                        name: "fk_employee",
                        column: x => x.empid,
                        principalTable: "employee",
                        principalColumn: "empid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workson",
                columns: table => new
                {
                    empid = table.Column<int>(type: "integer", nullable: false),
                    projid = table.Column<int>(type: "integer", nullable: false),
                    dateworked = table.Column<DateOnly>(type: "date", nullable: false),
                    hoursworked = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("workson_pkey", x => new { x.empid, x.projid });
                    table.ForeignKey(
                        name: "workson_empid_fkey",
                        column: x => x.empid,
                        principalTable: "employee",
                        principalColumn: "empid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "workson_projid_fkey",
                        column: x => x.projid,
                        principalTable: "project",
                        principalColumn: "projid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_department_mgrempid",
                table: "department",
                column: "mgrempid");

            migrationBuilder.CreateIndex(
                name: "IX_dependent_empid",
                table: "dependent",
                column: "empid");

            migrationBuilder.CreateIndex(
                name: "employee_ssn_key",
                table: "employee",
                column: "ssn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_deptid",
                table: "employee",
                column: "deptid");

            migrationBuilder.CreateIndex(
                name: "IX_employee_supervisorid",
                table: "employee",
                column: "supervisorid");

            migrationBuilder.CreateIndex(
                name: "IX_location_deptid",
                table: "location",
                column: "deptid");

            migrationBuilder.CreateIndex(
                name: "IX_project_deptid",
                table: "project",
                column: "deptid");

            migrationBuilder.CreateIndex(
                name: "IX_workson_projid",
                table: "workson",
                column: "projid");

            migrationBuilder.AddForeignKey(
                name: "fk_mgrempid",
                table: "department",
                column: "mgrempid",
                principalTable: "employee",
                principalColumn: "empid",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_mgrempid",
                table: "department");

            migrationBuilder.DropTable(
                name: "dependent");

            migrationBuilder.DropTable(
                name: "location");

            migrationBuilder.DropTable(
                name: "workson");

            migrationBuilder.DropTable(
                name: "project");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "department");
        }
    }
}
