using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TECsite.Migrations
{
    /// <inheritdoc />
    public partial class EventInfoUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventCategory",
                table: "EventsInfo");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "EventsInfo");

            migrationBuilder.AlterColumn<int>(
                name: "EventNumber",
                table: "EventsInfo",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "QuestCompatable",
                table: "EventsInfo",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventsInfo",
                table: "EventsInfo",
                column: "EventNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventsInfo",
                table: "EventsInfo");

            migrationBuilder.DropColumn(
                name: "QuestCompatable",
                table: "EventsInfo");

            migrationBuilder.AlterColumn<int>(
                name: "EventNumber",
                table: "EventsInfo",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "EventCategory",
                table: "EventsInfo",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EventId",
                table: "EventsInfo",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
