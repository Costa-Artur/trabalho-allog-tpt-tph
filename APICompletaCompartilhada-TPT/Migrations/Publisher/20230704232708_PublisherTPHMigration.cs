using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Univali.Api.Migrations.Publisher
{
    /// <inheritdoc />
    public partial class PublisherTPHMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "PublisherId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "PublisherId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "PublisherId",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJ",
                table: "Publishers",
                type: "character varying(14)",
                maxLength: 14,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character(14)",
                oldFixedLength: true,
                oldMaxLength: 14);

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Publishers",
                type: "character varying(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublisherType",
                table: "Publishers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "PublisherId", "CNPJ", "Name", "PublisherType" },
                values: new object[,]
                {
                    { 1, "64167199000120", "LegalPublisher1", 3 },
                    { 2, "94333690000144", "LegalPublisher2", 3 }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "PublisherId", "CPF", "Name", "PublisherType" },
                values: new object[,]
                {
                    { 3, "07382817946", "NaturalPublisher1", 2 },
                    { 4, "12345678912", "NaturalPublisher2", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "PublisherId",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "PublisherType",
                table: "Publishers");

            migrationBuilder.AlterColumn<string>(
                name: "CNPJ",
                table: "Publishers",
                type: "character(14)",
                fixedLength: true,
                maxLength: 14,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(14)",
                oldMaxLength: 14,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "PublisherId",
                keyValue: 1,
                columns: new[] { "CNPJ", "Name" },
                values: new object[] { "14698277000144", "Steven Spielberg Production Company" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "PublisherId",
                keyValue: 2,
                columns: new[] { "CNPJ", "Name" },
                values: new object[] { "12135618000148", "James Cameron Corporation" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "PublisherId",
                keyValue: 3,
                columns: new[] { "CNPJ", "Name" },
                values: new object[] { "64167199000120", "Quentin Tarantino Production" });
        }
    }
}
