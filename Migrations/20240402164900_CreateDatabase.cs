using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Glimpse.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ResponsibleUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Users_ResponsibleUserId",
                        column: x => x.ResponsibleUserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ProjectUser",
                columns: table => new
                {
                    ProjectsProjectId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUser", x => new { x.ProjectsProjectId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_ProjectUser_Projects_ProjectsProjectId",
                        column: x => x.ProjectsProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkProjectsProjectIdProjectId = table.Column<int>(type: "int", nullable: false),
                    FkUsersUserIdUserId = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanRemoveMembers = table.Column<bool>(type: "bit", nullable: false),
                    CanCreateRole = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_Roles_Projects_FkProjectsProjectIdProjectId",
                        column: x => x.FkProjectsProjectIdProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Roles_Users_FkUsersUserIdUserId",
                        column: x => x.FkUsersUserIdUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    FkProjectsProjectId = table.Column<int>(type: "int", nullable: false),
                    FkUsersUserId = table.Column<int>(type: "int", nullable: false),
                    FkRolesRoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => new { x.FkUsersUserId, x.FkProjectsProjectId });
                    table.ForeignKey(
                        name: "FK_Teams_Projects_FkProjectsProjectId",
                        column: x => x.FkProjectsProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Roles_FkRolesRoleId",
                        column: x => x.FkRolesRoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                    table.ForeignKey(
                        name: "FK_Teams_Users_FkUsersUserId",
                        column: x => x.FkUsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ResponsibleUserId",
                table: "Projects",
                column: "ResponsibleUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUser_UsersUserId",
                table: "ProjectUser",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_FkProjectsProjectIdProjectId",
                table: "Roles",
                column: "FkProjectsProjectIdProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_FkUsersUserIdUserId",
                table: "Roles",
                column: "FkUsersUserIdUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_FkProjectsProjectId",
                table: "Teams",
                column: "FkProjectsProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_FkRolesRoleId",
                table: "Teams",
                column: "FkRolesRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectUser");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
