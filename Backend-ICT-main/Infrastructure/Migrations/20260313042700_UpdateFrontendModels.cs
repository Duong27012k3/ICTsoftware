using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFrontendModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProjectTrans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeatureType",
                table: "Features",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Features",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProjectTrans");

            migrationBuilder.DropColumn(
                name: "FeatureType",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "Features");
        }
    }
}
