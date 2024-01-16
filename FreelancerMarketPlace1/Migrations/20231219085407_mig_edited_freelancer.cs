using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancerMarketPlace1.Migrations
{
    /// <inheritdoc />
    public partial class mig_edited_freelancer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bagdet2",
                table: "Freelancers");

            migrationBuilder.DropColumn(
                name: "Bagdet3",
                table: "Freelancers");

            migrationBuilder.DropColumn(
                name: "Educaiton",
                table: "Freelancers");

            migrationBuilder.DropColumn(
                name: "FeedBack",
                table: "Freelancers");

            migrationBuilder.RenameColumn(
                name: "TechSkill",
                table: "Freelancers",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Overview",
                table: "Freelancers",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "ExperienceDuraiton",
                table: "Freelancers",
                newName: "ExperienceDuration");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExperienceDuration",
                table: "Freelancers",
                newName: "ExperienceDuraiton");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Freelancers",
                newName: "TechSkill");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Freelancers",
                newName: "Overview");

            migrationBuilder.AddColumn<string>(
                name: "Bagdet2",
                table: "Freelancers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bagdet3",
                table: "Freelancers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Educaiton",
                table: "Freelancers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeedBack",
                table: "Freelancers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
