using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace redditwebapi.Migrations
{
    public partial class Ændringafvotestildownogupvote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Vote",
                table: "Posts",
                newName: "Upvote");

            migrationBuilder.RenameColumn(
                name: "Vote",
                table: "Comments",
                newName: "Upvote");

            migrationBuilder.AddColumn<int>(
                name: "Downvote",
                table: "Posts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Downvote",
                table: "Comments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Downvote",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Downvote",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Upvote",
                table: "Posts",
                newName: "Vote");

            migrationBuilder.RenameColumn(
                name: "Upvote",
                table: "Comments",
                newName: "Vote");
        }
    }
}
