using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TVT.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSettingsModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Linkedin",
                table: "Settings",
                newName: "WhatsApp");

            migrationBuilder.AlterColumn<string>(
                name: "GoogleMap",
                table: "Settings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultSeoDescriptionAz",
                table: "Settings",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultSeoDescriptionEn",
                table: "Settings",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultSeoDescriptionRu",
                table: "Settings",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultSeoKeywordsAz",
                table: "Settings",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultSeoKeywordsEn",
                table: "Settings",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultSeoKeywordsRu",
                table: "Settings",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultSeoTitleAz",
                table: "Settings",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultSeoTitleEn",
                table: "Settings",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultSeoTitleRu",
                table: "Settings",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FooterTextAz",
                table: "Settings",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FooterTextEn",
                table: "Settings",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FooterTextRu",
                table: "Settings",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telegram",
                table: "Settings",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TikTok",
                table: "Settings",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultSeoDescriptionAz",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "DefaultSeoDescriptionEn",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "DefaultSeoDescriptionRu",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "DefaultSeoKeywordsAz",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "DefaultSeoKeywordsEn",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "DefaultSeoKeywordsRu",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "DefaultSeoTitleAz",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "DefaultSeoTitleEn",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "DefaultSeoTitleRu",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "FooterTextAz",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "FooterTextEn",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "FooterTextRu",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Telegram",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "TikTok",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "WhatsApp",
                table: "Settings",
                newName: "Linkedin");

            migrationBuilder.AlterColumn<string>(
                name: "GoogleMap",
                table: "Settings",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
