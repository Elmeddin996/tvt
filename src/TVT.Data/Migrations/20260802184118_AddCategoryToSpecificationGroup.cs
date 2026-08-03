using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TVT.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryToSpecificationGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specifications_SpecificationGroups_SpecificationGroupId",
                table: "Specifications");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Specifications_SpecificationGroups_SpecificationGroupId",
                table: "Specifications",
                column: "SpecificationGroupId",
                principalTable: "SpecificationGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationGroups_Categories_CategoryId",
                table: "SpecificationGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Specifications_SpecificationGroups_SpecificationGroupId",
                table: "Specifications");

            migrationBuilder.DropIndex(
                name: "IX_SpecificationGroups_CategoryId",
                table: "SpecificationGroups");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "SpecificationGroups");

            migrationBuilder.AddForeignKey(
                name: "FK_Specifications_SpecificationGroups_SpecificationGroupId",
                table: "Specifications",
                column: "SpecificationGroupId",
                principalTable: "SpecificationGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
