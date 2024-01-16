using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancerMarketPlace1.Migrations
{
    /// <inheritdoc />
    public partial class mig_add_bagdet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bagdets",
                columns: table => new
                {
                    BagdetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BagdetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bagdets", x => x.BagdetID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bagdets");
        }
    }
}
