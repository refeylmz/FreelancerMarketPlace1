using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancerMarketPlace1.Migrations
{
    /// <inheritdoc />
    public partial class mig_bagdetId_inSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BagdetId",
                table: "Skills",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BagdetId",
                table: "Skills");
        }
    }
}
