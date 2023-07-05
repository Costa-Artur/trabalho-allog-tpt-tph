using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Univali.Api.Migrations.Customer
{
    /// <inheritdoc />
    public partial class AddCustomerContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Street = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalCustomers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    CNPJ = table.Column<string>(type: "character(14)", fixedLength: true, maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalCustomers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_LegalCustomers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NaturalCustomers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    CPF = table.Column<string>(type: "character(11)", fixedLength: true, maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturalCustomers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_NaturalCustomers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Name" },
                values: new object[,]
                {
                    { 1, "Linus Torvalds" },
                    { 2, "Bill Gates" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "City", "CustomerId", "Street" },
                values: new object[,]
                {
                    { 1, "Elvira", 1, "Verão do Cometa" },
                    { 2, "Perobia", 1, "Borboletas Psicodélicas" },
                    { 3, "Salandra", 2, "Canção Excêntrica" }
                });

            migrationBuilder.InsertData(
                table: "LegalCustomers",
                columns: new[] { "CustomerId", "CNPJ" },
                values: new object[] { 1, "14698277000144" });

            migrationBuilder.InsertData(
                table: "NaturalCustomers",
                columns: new[] { "CustomerId", "CPF" },
                values: new object[] { 2, "95395994076" });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "LegalCustomers");

            migrationBuilder.DropTable(
                name: "NaturalCustomers");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
