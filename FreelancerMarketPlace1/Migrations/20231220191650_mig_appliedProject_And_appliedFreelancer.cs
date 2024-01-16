using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancerMarketPlace1.Migrations
{
    /// <inheritdoc />
    public partial class mig_appliedProject_And_appliedFreelancer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FreelancerProject",
                columns: table => new
                {
                    AppliedFreelancersFreelancerID = table.Column<int>(type: "int", nullable: false),
                    AppliedProjectsProjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelancerProject", x => new { x.AppliedFreelancersFreelancerID, x.AppliedProjectsProjectID });
                    table.ForeignKey(
                        name: "FK_FreelancerProject_Freelancers_AppliedFreelancersFreelancerID",
                        column: x => x.AppliedFreelancersFreelancerID,
                        principalTable: "Freelancers",
                        principalColumn: "FreelancerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FreelancerProject_Projects_AppliedProjectsProjectID",
                        column: x => x.AppliedProjectsProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FreelancerProject_AppliedProjectsProjectID",
                table: "FreelancerProject",
                column: "AppliedProjectsProjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FreelancerProject");
        }
    }
}
