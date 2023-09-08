using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaffTimetable.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uuid", nullable: true),
                    Country = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    City = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    State = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Street = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Building = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    PostalCode = table.Column<int>(type: "integer", maxLength: 12, nullable: true),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    DictWorkDepartments_Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    DictWorkingDateType_Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ExternalUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    FirstName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    MiddleName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    IsMale = table.Column<bool>(type: "boolean", nullable: true),
                    UserEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    HireDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    WorkDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: true),
                    DictWorkingDateTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    ManagedOrganizations_OrganizationId = table.Column<Guid>(type: "uuid", nullable: true),
                    ManagedOrganizations_EmployeeId = table.Column<Guid>(type: "uuid", nullable: true),
                    Organization_Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Salary_EmployeeId = table.Column<Guid>(type: "uuid", nullable: true),
                    Rate = table.Column<double>(type: "double precision", nullable: true),
                    DictPostId = table.Column<Guid>(type: "uuid", nullable: true),
                    WorkDepartments_OrganizationId = table.Column<Guid>(type: "uuid", nullable: true),
                    DictWorkDepartmentsId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_DictPostId",
                        column: x => x.DictPostId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_DictWorkDepartmentsId",
                        column: x => x.DictWorkDepartmentsId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_DictWorkingDateTypeId",
                        column: x => x.DictWorkingDateTypeId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_ManagedOrganizations_EmployeeId",
                        column: x => x.ManagedOrganizations_EmployeeId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_ManagedOrganizations_OrganizationId",
                        column: x => x.ManagedOrganizations_OrganizationId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_Salary_EmployeeId",
                        column: x => x.Salary_EmployeeId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_WorkDepartments_OrganizationId",
                        column: x => x.WorkDepartments_OrganizationId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_DictPostId",
                table: "BaseEntity",
                column: "DictPostId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_DictWorkDepartmentsId",
                table: "BaseEntity",
                column: "DictWorkDepartmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_DictWorkingDateTypeId",
                table: "BaseEntity",
                column: "DictWorkingDateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_EmployeeId",
                table: "BaseEntity",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_ManagedOrganizations_EmployeeId",
                table: "BaseEntity",
                column: "ManagedOrganizations_EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_ManagedOrganizations_OrganizationId",
                table: "BaseEntity",
                column: "ManagedOrganizations_OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_OrganizationId",
                table: "BaseEntity",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_Salary_EmployeeId",
                table: "BaseEntity",
                column: "Salary_EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_WorkDepartments_OrganizationId",
                table: "BaseEntity",
                column: "WorkDepartments_OrganizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseEntity");
        }
    }
}
