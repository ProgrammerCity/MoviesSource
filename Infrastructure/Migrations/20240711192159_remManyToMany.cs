using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class remManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieCategury");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieCategury",
                columns: table => new
                {
                    CateguryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MvoieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCategury", x => new { x.CateguryId, x.MvoieId });
                    table.ForeignKey(
                        name: "FK_MovieCategury_Categury_CateguryId",
                        column: x => x.CateguryId,
                        principalTable: "Categury",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieCategury_Movie_MvoieId",
                        column: x => x.MvoieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieCategury_MvoieId",
                table: "MovieCategury",
                column: "MvoieId");
        }
    }
}
