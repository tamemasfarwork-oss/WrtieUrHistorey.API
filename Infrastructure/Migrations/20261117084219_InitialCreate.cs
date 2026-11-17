using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Introduction = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    PlannedChaptersCount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FinalReflection = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    FinalSatisfaction = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.CheckConstraint("CK_Books_FinalSatisfaction", "[FinalSatisfaction] BETWEEN 0 AND 100");
                    table.CheckConstraint("CK_Books_PlannedChapters", "[PlannedChaptersCount] >= 1");
                    table.ForeignKey(
                        name: "FK_Books_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookGoals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsAchieved = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGoals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookGoals_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookVideos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Script = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FailureReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookVideos_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    VisibleText = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    SealedText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartsAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndsAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Achievements = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Satisfaction = table.Column<int>(type: "int", nullable: true),
                    ReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapters", x => x.Id);
                    table.CheckConstraint("CK_Chapters_Dates", "[EndsAt] > [StartsAt]");
                    table.CheckConstraint("CK_Chapters_Satisfaction", "[Satisfaction] BETWEEN 0 AND 100");
                    table.ForeignKey(
                        name: "FK_Chapters_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChapterGoals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChapterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsAchieved = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterGoals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChapterGoals_Chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarginNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChapterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarginNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarginNotes_Chapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "Chapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoteImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MarginNoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoragePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteImages", x => x.Id);
                    table.CheckConstraint("CK_NoteImages_Size", "[SizeInBytes] > 0");
                    table.ForeignKey(
                        name: "FK_NoteImages_MarginNotes_MarginNoteId",
                        column: x => x.MarginNoteId,
                        principalTable: "MarginNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookGoals_BookId_Order",
                table: "BookGoals",
                columns: new[] { "BookId", "Order" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserId",
                table: "Books",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookVideos_BookId_Status",
                table: "BookVideos",
                columns: new[] { "BookId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_ChapterGoals_ChapterId_Order",
                table: "ChapterGoals",
                columns: new[] { "ChapterId", "Order" });

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_BookId_Number",
                table: "Chapters",
                columns: new[] { "BookId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarginNotes_ChapterId_CreatedAt",
                table: "MarginNotes",
                columns: new[] { "ChapterId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_NoteImages_MarginNoteId_Order",
                table: "NoteImages",
                columns: new[] { "MarginNoteId", "Order" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookGoals");

            migrationBuilder.DropTable(
                name: "BookVideos");

            migrationBuilder.DropTable(
                name: "ChapterGoals");

            migrationBuilder.DropTable(
                name: "NoteImages");

            migrationBuilder.DropTable(
                name: "MarginNotes");

            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
