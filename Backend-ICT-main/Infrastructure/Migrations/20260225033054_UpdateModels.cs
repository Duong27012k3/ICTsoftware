using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Contacts",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Contacts",
                newName: "ContactId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Contacts",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Contacts",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Contacts",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    FieldId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Uid = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "active"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.FieldId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "user"),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "active")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "FieldTrans",
                columns: table => new
                {
                    FieldTransId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FieldId = table.Column<int>(type: "integer", nullable: false),
                    LangCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldTrans", x => x.FieldTransId);
                    table.ForeignKey(
                        name: "FK_FieldTrans_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "FieldId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FieldId = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CatalogueUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "active"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "FieldId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FieldId = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CatalogueUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "active"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                    table.ForeignKey(
                        name: "FK_Services_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "FieldId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    FeatureId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    LangCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.FeatureId);
                    table.ForeignKey(
                        name: "FK_Features_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTrans",
                columns: table => new
                {
                    ProjectTransId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    LangCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ShortDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTrans", x => x.ProjectTransId);
                    table.ForeignKey(
                        name: "FK_ProjectTrans_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blocks",
                columns: table => new
                {
                    BlockId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    OwnerId = table.Column<int>(type: "integer", nullable: false),
                    BlockType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BlockOrder = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ServiceId = table.Column<int>(type: "integer", nullable: true),
                    ProjectId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocks", x => x.BlockId);
                    table.ForeignKey(
                        name: "FK_Blocks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                    table.ForeignKey(
                        name: "FK_Blocks_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId");
                });

            migrationBuilder.CreateTable(
                name: "ServiceTrans",
                columns: table => new
                {
                    ServiceTransId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    LangCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ShortDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTrans", x => x.ServiceTransId);
                    table.ForeignKey(
                        name: "FK_ServiceTrans_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlockTrans",
                columns: table => new
                {
                    BlockTransId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BlockId = table.Column<int>(type: "integer", nullable: false),
                    LangCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockTrans", x => x.BlockTransId);
                    table.ForeignKey(
                        name: "FK_BlockTrans_Blocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Blocks",
                        principalColumn: "BlockId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_ProjectId",
                table: "Blocks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_ServiceId",
                table: "Blocks",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockTrans_BlockId",
                table: "BlockTrans",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_ProjectId",
                table: "Features",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_Uid",
                table: "Fields",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FieldTrans_FieldId",
                table: "FieldTrans",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_FieldId",
                table: "Projects",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTrans_ProjectId",
                table: "ProjectTrans",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_FieldId",
                table: "Services",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTrans_ServiceId",
                table: "ServiceTrans",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockTrans");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "FieldTrans");

            migrationBuilder.DropTable(
                name: "ProjectTrans");

            migrationBuilder.DropTable(
                name: "ServiceTrans");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Blocks");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Contacts",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "ContactId",
                table: "Contacts",
                newName: "ID");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Contacts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Contacts",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Contacts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
