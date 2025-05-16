using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademIQ.Migrations
{
    /// <inheritdoc />
    public partial class hg1223 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "Submissions");

            migrationBuilder.AddColumn<string>(
                name: "SubmissionsSubmissionID",
                table: "Attachments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_SubmissionsSubmissionID",
                table: "Attachments",
                column: "SubmissionsSubmissionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Submissions_SubmissionsSubmissionID",
                table: "Attachments",
                column: "SubmissionsSubmissionID",
                principalTable: "Submissions",
                principalColumn: "SubmissionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Submissions_SubmissionsSubmissionID",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_SubmissionsSubmissionID",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "SubmissionsSubmissionID",
                table: "Attachments");

            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "Submissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
