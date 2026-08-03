using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TVT.Data.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManySpecificationGroupCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationGroups_Categories_CategoryId",
                table: "SpecificationGroups");

            migrationBuilder.DropIndex(
                name: "IX_SpecificationGroups_CategoryId",
                table: "SpecificationGroups");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "SpecificationGroups");

            migrationBuilder.CreateTable(
                name: "CategorySpecificationGroups",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    SpecificationGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySpecificationGroups", x => new { x.CategoryId, x.SpecificationGroupId });
                    table.ForeignKey(
                        name: "FK_CategorySpecificationGroups_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategorySpecificationGroups_SpecificationGroups_Specificati~",
                        column: x => x.SpecificationGroupId,
                        principalTable: "SpecificationGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategorySpecificationGroups_SpecificationGroupId",
                table: "CategorySpecificationGroups",
                column: "SpecificationGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategorySpecificationGroups");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "SpecificationGroups",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationGroups_CategoryId",
                table: "SpecificationGroups",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationGroups_Categories_CategoryId",
                table: "SpecificationGroups",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
