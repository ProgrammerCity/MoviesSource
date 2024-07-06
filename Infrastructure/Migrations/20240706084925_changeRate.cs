using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFreamewoerkCore.Migrations
{
    /// <inheritdoc />
    public partial class changeRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Rate",
                table: "Movie",
                type: "real",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Rate",
                table: "Movie",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
