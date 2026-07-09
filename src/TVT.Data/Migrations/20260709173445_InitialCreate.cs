using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TVT.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameAz = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NameEn = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NameRu = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SlugAz = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    SlugEn = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    SlugRu = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    DescriptionAz = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    DescriptionEn = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    DescriptionRu = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    Logo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Website = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoTitleAz = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SeoTitleEn = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SeoTitleRu = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SeoDescriptionAz = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoDescriptionEn = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoDescriptionRu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoKeywordsAz = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SeoKeywordsEn = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SeoKeywordsRu = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    NameAz = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NameEn = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NameRu = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SlugAz = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SlugEn = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SlugRu = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DescriptionAz = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    DescriptionEn = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    DescriptionRu = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Image = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Icon = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoTitleAz = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoTitleEn = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoTitleRu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoDescriptionAz = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoDescriptionEn = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoDescriptionRu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoKeywordsAz = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SeoKeywordsEn = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SeoKeywordsRu = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Message = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleAz = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TitleEn = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TitleRu = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SlugAz = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    SlugEn = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    SlugRu = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ShortDescriptionAz = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ShortDescriptionEn = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ShortDescriptionRu = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ContentAz = table.Column<string>(type: "text", nullable: false),
                    ContentEn = table.Column<string>(type: "text", nullable: false),
                    ContentRu = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SeoTitleAz = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SeoTitleEn = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SeoTitleRu = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SeoDescriptionAz = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoDescriptionEn = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoDescriptionRu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoKeywordsAz = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SeoKeywordsEn = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SeoKeywordsRu = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleAz = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TitleEn = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TitleRu = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SlugAz = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    SlugEn = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    SlugRu = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ContentAz = table.Column<string>(type: "text", nullable: false),
                    ContentEn = table.Column<string>(type: "text", nullable: false),
                    ContentRu = table.Column<string>(type: "text", nullable: false),
                    SeoTitleAz = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SeoTitleEn = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SeoTitleRu = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SeoDescriptionAz = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoDescriptionEn = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoDescriptionRu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoKeywordsAz = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SeoKeywordsEn = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SeoKeywordsRu = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Logo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Phone1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Phone2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    AddressAz = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AddressEn = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AddressRu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    WorkingHoursAz = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    WorkingHoursEn = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    WorkingHoursRu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Facebook = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Instagram = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Youtube = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Linkedin = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    GoogleMap = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleAz = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    TitleEn = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    TitleRu = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DescriptionAz = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    DescriptionEn = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    DescriptionRu = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    Image = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ButtonTextAz = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ButtonTextEn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ButtonTextRu = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ButtonLink = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsSubscribed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NameAz = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NameEn = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NameRu = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SlugAz = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    SlugEn = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    SlugRu = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    DescriptionAz = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    DescriptionEn = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    DescriptionRu = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    OldPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    StockQuantity = table.Column<int>(type: "integer", nullable: false),
                    IsFeatured = table.Column<bool>(type: "boolean", nullable: false),
                    IsNew = table.Column<bool>(type: "boolean", nullable: false),
                    SeoTitleAz = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SeoTitleEn = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SeoTitleRu = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SeoDescriptionAz = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoDescriptionEn = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoDescriptionRu = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SeoKeywordsAz = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SeoKeywordsEn = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    SeoKeywordsRu = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    File = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDocuments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductSpecifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    NameAz = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NameEn = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NameRu = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ValueAz = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ValueEn = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ValueRu = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSpecifications_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ProductId",
                table: "ProductCategories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDocuments_ProductId",
                table: "ProductDocuments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecifications_ProductId",
                table: "ProductSpecifications",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "ProductDocuments");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductSpecifications");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
