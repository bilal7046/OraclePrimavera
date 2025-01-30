using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OraclePrimavera.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectRecordManual",
                columns: table => new
                {
                    ProctorNo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    RecordNo = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    ProjectId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    ProjectName = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    ContractNo = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    ProjectOHName = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    ProjectStartDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    ProjectEndDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    Category = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "NCLOB", maxLength: 4000, nullable: true),
                    Currency = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: true),
                    CostCode = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    AnticipatedCost = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: true),
                    ActualCostAmount = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: true),
                    AttachUrl = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    Attachment = table.Column<string>(type: "CLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRecordManual", x => x.ProctorNo);
                });

            migrationBuilder.CreateTable(
                name: "ProjectRecords",
                columns: table => new
                {
                    ProctorNo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    RecordNo = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    CreatedBy = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    ProjectId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    ProjectName = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    ContractNo = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    ProjectOHName = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    ProjectStartDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    ProjectEndDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    Category = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "NCLOB", maxLength: 4000, nullable: true),
                    Currency = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: true),
                    CostCode = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    AnticipatedCost = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: true),
                    ActualCostAmount = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: true),
                    AttachUrl = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    Attachment = table.Column<string>(type: "CLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRecords", x => x.ProctorNo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectRecordManual");

            migrationBuilder.DropTable(
                name: "ProjectRecords");
        }
    }
}
