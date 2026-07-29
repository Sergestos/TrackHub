using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackHub.Persistence.Sql.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    type = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    photo_url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    registration_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    last_entrance_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    last_play_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    first_play_date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "login_sessions",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    session_id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    expires_at = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    user_id1 = table.Column<string>(type: "nvarchar(128)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login_sessions", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_login_sessions_users_user_id1",
                        column: x => x.user_id1,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_songs",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    position = table.Column<int>(type: "int", nullable: false),
                    song_name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(128)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_songs", x => new { x.user_id, x.position });
                    table.ForeignKey(
                        name: "FK_user_songs_users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_user_songs_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_login_sessions_user_id1",
                table: "login_sessions",
                column: "user_id1",
                unique: true,
                filter: "[user_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_user_songs_user_id",
                table: "user_songs",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_songs_UserId1",
                table: "user_songs",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "login_sessions");

            migrationBuilder.DropTable(
                name: "user_songs");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
