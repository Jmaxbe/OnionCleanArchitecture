using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DictPost",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictPost", x => x.Id);
                    table.UniqueConstraint("AK_DictPost_UniqueId", x => x.UniqueId);
                });

            migrationBuilder.CreateTable(
                name: "DictWorkDepartments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictWorkDepartments", x => x.Id);
                    table.UniqueConstraint("AK_DictWorkDepartments_UniqueId", x => x.UniqueId);
                });

            migrationBuilder.CreateTable(
                name: "DictWorkingDateType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictWorkingDateType", x => x.Id);
                    table.UniqueConstraint("AK_DictWorkingDateType_UniqueId", x => x.UniqueId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExternalUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    LastName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    IsMale = table.Column<bool>(type: "boolean", nullable: false),
                    UserEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    HireDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.UniqueConstraint("AK_Employee_UniqueId", x => x.UniqueId);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                    table.UniqueConstraint("AK_Organization_UniqueId", x => x.UniqueId);
                });

            migrationBuilder.CreateTable(
                name: "FlowOfWorks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DictWorkingDateTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowOfWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowOfWorks_DictWorkingDateType_DictWorkingDateTypeId",
                        column: x => x.DictWorkingDateTypeId,
                        principalTable: "DictWorkingDateType",
                        principalColumn: "UniqueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowOfWorks_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "UniqueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salary",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rate = table.Column<double>(type: "double precision", nullable: false),
                    DictPostId = table.Column<Guid>(type: "uuid", nullable: false),
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salary_DictPost_DictPostId",
                        column: x => x.DictPostId,
                        principalTable: "DictPost",
                        principalColumn: "UniqueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Salary_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "UniqueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Country = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    City = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    State = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Street = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Building = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    PostalCode = table.Column<int>(type: "integer", maxLength: 12, nullable: true),
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "UniqueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManagedOrganizations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizationId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagedOrganizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagedOrganizations_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "UniqueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManagedOrganizations_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "UniqueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkDepartments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizationId = table.Column<Guid>(type: "uuid", nullable: false),
                    DictWorkDepartmentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkDepartments_DictWorkDepartments_DictWorkDepartmentsId",
                        column: x => x.DictWorkDepartmentsId,
                        principalTable: "DictWorkDepartments",
                        principalColumn: "UniqueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkDepartments_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "UniqueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_OrganizationId",
                table: "Address",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowOfWorks_DictWorkingDateTypeId",
                table: "FlowOfWorks",
                column: "DictWorkingDateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowOfWorks_EmployeeId",
                table: "FlowOfWorks",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagedOrganizations_EmployeeId",
                table: "ManagedOrganizations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagedOrganizations_OrganizationId",
                table: "ManagedOrganizations",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Salary_DictPostId",
                table: "Salary",
                column: "DictPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Salary_EmployeeId",
                table: "Salary",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkDepartments_DictWorkDepartmentsId",
                table: "WorkDepartments",
                column: "DictWorkDepartmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkDepartments_OrganizationId",
                table: "WorkDepartments",
                column: "OrganizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "FlowOfWorks");

            migrationBuilder.DropTable(
                name: "ManagedOrganizations");

            migrationBuilder.DropTable(
                name: "Salary");

            migrationBuilder.DropTable(
                name: "WorkDepartments");

            migrationBuilder.DropTable(
                name: "DictWorkingDateType");

            migrationBuilder.DropTable(
                name: "DictPost");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "DictWorkDepartments");

            migrationBuilder.DropTable(
                name: "Organization");
        }
    }
}
