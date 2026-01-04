using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RouteGymManagementDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class SomeEnhancement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "GymUserValidEmailCheck1",
                table: "Trainers");

            migrationBuilder.DropCheckConstraint(
                name: "GymUserValidEmailCheck",
                table: "Members");

            migrationBuilder.AddCheckConstraint(
                name: "GymUserValidEmailCheck1",
                table: "Trainers",
                sql: "Email like '_%@_%._%'");

            migrationBuilder.AddCheckConstraint(
                name: "GymUserValidEmailCheck",
                table: "Members",
                sql: "Email like '_%@_%._%'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "GymUserValidEmailCheck1",
                table: "Trainers");

            migrationBuilder.DropCheckConstraint(
                name: "GymUserValidEmailCheck",
                table: "Members");

            migrationBuilder.AddCheckConstraint(
                name: "GymUserValidEmailCheck1",
                table: "Trainers",
                sql: "Email Like '%_@_%._%'");

            migrationBuilder.AddCheckConstraint(
                name: "GymUserValidEmailCheck",
                table: "Members",
                sql: "Email Like '%_@_%._%'");
        }
    }
}
