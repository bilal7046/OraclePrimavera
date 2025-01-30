using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OraclePrimavera.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "AnticipatedCost",
                table: "ProjectRecords",
                type: "DECIMAL(18, 2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ActualCostAmount",
                table: "ProjectRecords",
                type: "DECIMAL(18, 2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AnticipatedCost",
                table: "ProjectRecordManual",
                type: "DECIMAL(18, 2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ActualCostAmount",
                table: "ProjectRecordManual",
                type: "DECIMAL(18, 2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectRecordFile",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ProjectRecordId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    FileName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Extension = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    MimeType = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Size = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    FileUrl = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRecordFile", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectRecordFile");

            migrationBuilder.AlterColumn<decimal>(
                name: "AnticipatedCost",
                table: "ProjectRecords",
                type: "DECIMAL(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ActualCostAmount",
                table: "ProjectRecords",
                type: "DECIMAL(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AnticipatedCost",
                table: "ProjectRecordManual",
                type: "DECIMAL(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ActualCostAmount",
                table: "ProjectRecordManual",
                type: "DECIMAL(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)",
                oldNullable: true);
        }
    }
}
