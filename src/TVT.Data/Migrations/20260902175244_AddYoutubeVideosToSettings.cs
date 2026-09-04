using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TVT.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddYoutubeVideosToSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "YoutubeVideo1",
                table: "Settings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoutubeVideo2",
                table: "Settings",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YoutubeVideo1",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "YoutubeVideo2",
                table: "Settings");
        }
    }
}
