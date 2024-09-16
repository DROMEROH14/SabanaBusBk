using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SabanaBus.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bus",
                columns: table => new
                {
                    IdBus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicensePlate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Capacity = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bus", x => x.IdBus);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    IdPermission = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Permission = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.IdPermission);
                });

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    IdRoute = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EstimatedDuration = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.IdRoute);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    IdUserType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.IdUserType);
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    AssignmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkBusID = table.Column<int>(type: "int", nullable: false),
                    FkRouteID = table.Column<int>(type: "int", nullable: false),
                    AssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x.AssignmentID);
                    table.ForeignKey(
                        name: "FK_Assignment_Bus_FkBusID",
                        column: x => x.FkBusID,
                        principalTable: "Bus",
                        principalColumn: "IdBus");
                    table.ForeignKey(
                        name: "FK_Assignment_Route_FkRouteID",
                        column: x => x.FkRouteID,
                        principalTable: "Route",
                        principalColumn: "IdRoute");
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    IDSchedule = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkIDRoute = table.Column<int>(type: "int", nullable: false),
                    DepartureTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ArrivalTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.IDSchedule);
                    table.ForeignKey(
                        name: "FK_Schedule_Route_FkIDRoute",
                        column: x => x.FkIDRoute,
                        principalTable: "Route",
                        principalColumn: "IdRoute",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionsXUserTypes",
                columns: table => new
                {
                    IdPermissionsXUserTypes = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKIdPermission = table.Column<int>(type: "int", nullable: false),
                    FKIdUserType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionsXUserTypes", x => x.IdPermissionsXUserTypes);
                    table.ForeignKey(
                        name: "FK_PermissionsXUserTypes_Permissions_FKIdPermission",
                        column: x => x.FKIdPermission,
                        principalTable: "Permissions",
                        principalColumn: "IdPermission");
                    table.ForeignKey(
                        name: "FK_PermissionsXUserTypes_UserType_FKIdUserType",
                        column: x => x.FKIdUserType,
                        principalTable: "UserType",
                        principalColumn: "IdUserType");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKIdUserType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_Users_UserType_FKIdUserType",
                        column: x => x.FKIdUserType,
                        principalTable: "UserType",
                        principalColumn: "IdUserType");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    IDNotification = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkIdSchedule = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.IDNotification);
                    table.ForeignKey(
                        name: "FK_Notification_Schedule_FkIdSchedule",
                        column: x => x.FkIdSchedule,
                        principalTable: "Schedule",
                        principalColumn: "IDSchedule");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_FkBusID",
                table: "Assignment",
                column: "FkBusID");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_FkRouteID",
                table: "Assignment",
                column: "FkRouteID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_FkIdSchedule",
                table: "Notification",
                column: "FkIdSchedule");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionsXUserTypes_FKIdPermission",
                table: "PermissionsXUserTypes",
                column: "FKIdPermission");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionsXUserTypes_FKIdUserType",
                table: "PermissionsXUserTypes",
                column: "FKIdUserType");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_FkIDRoute",
                table: "Schedule",
                column: "FkIDRoute");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FKIdUserType",
                table: "Users",
                column: "FKIdUserType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "PermissionsXUserTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Bus");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "UserType");

            migrationBuilder.DropTable(
                name: "Route");
        }
    }
}
