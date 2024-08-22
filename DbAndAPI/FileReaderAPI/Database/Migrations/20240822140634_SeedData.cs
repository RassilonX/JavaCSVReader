using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerRef", "AddressLine1", "AddressLine2", "Country", "County", "CustomerName", "Postcode", "Town" },
                values: new object[] { "TEST", "Test House", "Test Building", "Testland", "Testshire", "John Tester", "TE573RS", "Testmouth" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "CustomerRef",
                keyValue: "TEST");
        }
    }
}
