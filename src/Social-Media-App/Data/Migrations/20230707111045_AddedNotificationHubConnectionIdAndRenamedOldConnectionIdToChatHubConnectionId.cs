using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedNotificationHubConnectionIdAndRenamedOldConnectionIdToChatHubConnectionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConnectionId",
                table: "AspNetUsers",
                newName: "NotificationHubConnectionId");

            migrationBuilder.AddColumn<string>(
                name: "ChatHubConnectionId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatHubConnectionId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "NotificationHubConnectionId",
                table: "AspNetUsers",
                newName: "ConnectionId");
        }
    }
}
