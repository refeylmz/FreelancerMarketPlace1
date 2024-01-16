using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancerMarketPlace1.Migrations
{
    /// <inheritdoc />
    public partial class mig_company_new_props_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bagdet1",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Bagdet2",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Bagdet3",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Educaiton",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FeedBack",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Notification",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Overview",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "WorkModel",
                table: "Companies",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "UserImage",
                table: "Companies",
                newName: "CompanyName");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Companies",
                newName: "CompanyImage");

            migrationBuilder.RenameColumn(
                name: "TechSkill",
                table: "Companies",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "ExperienceDuraiton",
                table: "Companies",
                newName: "ExperienceDuration");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExperienceDuration",
                table: "Companies",
                newName: "ExperienceDuraiton");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Companies",
                newName: "WorkModel");

            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "Companies",
                newName: "UserImage");

            migrationBuilder.RenameColumn(
                name: "CompanyImage",
                table: "Companies",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Companies",
                newName: "TechSkill");

            migrationBuilder.AddColumn<string>(
                name: "Bagdet1",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bagdet2",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bagdet3",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Educaiton",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeedBack",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notification",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Overview",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Companies",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
