using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SabanaBus.Migrations
{
    /// <inheritdoc />
    public partial class Migration_001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_Route_FkRouteID",
                table: "Assignment");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Route_FkIDRoute",
                table: "Schedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Route",
                table: "Route");

            migrationBuilder.RenameTable(
                name: "Route",
                newName: "Routes");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Schedule",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PermissionsXUserTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Permissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Notification",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Bus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Assignment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Routes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Routes",
                table: "Routes",
                column: "IdRoute");

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityName = table.Column<string>(type: "varchar(64)", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    ActionType = table.Column<string>(type: "varchar(64)", nullable: false),
                    OldValues = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    NewValues = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_Routes_FkRouteID",
                table: "Assignment",
                column: "FkRouteID",
                principalTable: "Routes",
                principalColumn: "IdRoute");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Routes_FkIDRoute",
                table: "Schedule",
                column: "FkIDRoute",
                principalTable: "Routes",
                principalColumn: "IdRoute");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_Routes_FkRouteID",
                table: "Assignment");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Routes_FkIDRoute",
                table: "Schedule");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Routes",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PermissionsXUserTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Assignment");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Routes");

            migrationBuilder.RenameTable(
                name: "Routes",
                newName: "Route");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Route",
                table: "Route",
                column: "IdRoute");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_Route_FkRouteID",
                table: "Assignment",
                column: "FkRouteID",
                principalTable: "Route",
                principalColumn: "IdRoute");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Route_FkIDRoute",
                table: "Schedule",
                column: "FkIDRoute",
                principalTable: "Route",
                principalColumn: "IdRoute",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
