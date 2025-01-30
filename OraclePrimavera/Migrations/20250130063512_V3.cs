using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OraclePrimavera.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "Base64File",
                table: "ProjectRecordFile",
                type: "CLOB",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64File",
                table: "ProjectRecordFile");

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
